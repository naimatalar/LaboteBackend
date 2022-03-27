using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Entities;
using Labote.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserManagerController : LaboteControllerBase
    {
        private readonly LaboteContext _context;
        private readonly UserManager<LaboteUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private const string pageName = "kullanici-islemleri";
        public UserManagerController(LaboteContext context, UserManager<LaboteUser> userManager, RoleManager<UserRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<dynamic>> GetAllUsers()
        {
            var userid = User.Identity.UserId();
            var result = _context.Users.Where(x => !x.IsDelete).Where(x => x.Id != userid).Select(x => new
            {
                x.Id,
                x.FirstName,
                x.Lastname,
                x.Email,
                x.UserName,
            });
            PageResponse.Data = result.ToList();
            return Ok(PageResponse);
        }
        [HttpGet("GetRoles")]
        public async Task<ActionResult<dynamic>> GetRoles()
        {
            var result = _context.Roles.Where(x => !x.IsDelete).Select(x => new
            {
                x.Id,
                x.Name,

            });
            PageResponse.Data = result.ToList();
            return Ok(PageResponse);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<dynamic>> GetUserById(Guid id)
        {

            var result = _context.Users;
            var roles = _context.UserRoles.Where(x => x.UserId == id).ToList();
            var labUser = _context.LaboratoryUsers.Where(x => x.LaboteUserId == id).ToList();
            List<string> rolelist = new List<string>();
            List<dynamic> laboratuvarList = new List<dynamic>();

            foreach (var item in roles)
            {
                var r = _context.Roles.Where(c => c.Id == item.RoleId && !c.IsDelete).FirstOrDefault();
                if (r != null)
                {
                    rolelist.Add(r.Name.ToString());
                }
            }
            foreach (var item in labUser)
            {
                var r = _context.LaboratoryUsers.Include(x=>x.Laboratory).Where(c => c.Id == item.Id && !c.IsDelete).FirstOrDefault();
                if (r != null)
                {
                    laboratuvarList.Add(r.LaboratoryId.ToString());
                }
            }

            PageResponse.Data = result.Where(x => x.Id == id).Select(x => new
            {
                x.Id,
                x.FirstName,
                x.Lastname,
                x.Email,
                x.UserName,
                Roles = rolelist.Select(x => new
                {
                    id = x,
                    text = x
                }).ToList(),
                LaboratuvarList = laboratuvarList
            }).FirstOrDefault();
            return Ok(PageResponse);
        }
        [HttpPost("EditUser")]
        [PermissionCheck(Action = pageName)]
        public async Task<ActionResult<dynamic>> EditUser(CreateUserRequestModel model)
        {
            var user = new LaboteUser();
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    user = _userManager.Users.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (user.NotDelete)
                    {
                        PageResponse.IsError = true;
                        PageResponse.Message = "Bu Kayıt Birincil Kayıttır Düzenlenemez";
                        return PageResponse;
                    }
                    var mailCheck = _userManager.Users.Where(x => x.Email == model.Email).Any();
                    if (!mailCheck)
                    {
                        user.Email = model.Email;
                    }


                    user.UserName = model.UserName;
                    user.FirstName = model.FirstName;
                    user.Lastname = model.Lastname;

                    try
                    {

                        context.SaveChanges();
                        var usr = _userManager.UpdateAsync(user).Result;

                        if (!string.IsNullOrEmpty(model.Password))
                        {
                            var passwordToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                            var dd = _userManager.ResetPasswordAsync(user, passwordToken, model.Password).Result;
                        }

                    }
                    catch (Exception e)
                    {

                        PageResponse.IsError = true;
                        return Ok(PageResponse);
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                };

                try
                {

                }
                catch (Exception)
                {

                    throw;
                }


            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var rl = context.UserRoles.Where(x => x.UserId == model.Id).ToList();
                    context.UserRoles.RemoveRange(rl);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var item in model.Roles)
                    {
                        var rl = context.Roles.Where(x => x.Name == item).FirstOrDefault();
                        context.UserRoles.Add(new IdentityUserRole<Guid> { RoleId = rl.Id, UserId = user.Id });
                        context.SaveChanges();
                    }        
                    transaction.Commit();

                }
            }
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var rl = context.LaboratoryUsers.Where(x => x.LaboteUserId == model.Id).ToList();
                    context.LaboratoryUsers.RemoveRange(rl);
                    context.SaveChanges();
                    transaction.Commit();
                }
            }

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var item in model.LaboratuvarList)
                    {
                        var rl = context.LaboratoryUsers.Where(x => x.Id == item).FirstOrDefault();
                        context.LaboratoryUsers.Add(new LaboratoryUser { LaboratoryId = item, LaboteUserId = user.Id });
                        context.SaveChanges();
                       
                    } 
                    transaction.Commit();
                }
            }


            PageResponse.Data = true;
            return Ok(PageResponse);
        }
        [HttpPost("CreateUser")]
        [PermissionCheck(Action = pageName)]
        public async Task<ActionResult<dynamic>> CreateUser(CreateUserRequestModel model)
        {
            var userId = User.Identity.UserId();
            var baseUser = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            LaboteUser user = new LaboteUser();
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {

                    user = new LaboteUser()
                    {
                        Email = model.Email,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = model.UserName,
                        FirstName = model.FirstName,
                        Lastname = model.Lastname,
                        UserTopicId = baseUser.UserTopicId
                    };

                    try
                    {
                        var usr = _userManager.CreateAsync(user, model.Password).Result;
                        foreach (var item in model.Roles)
                        {
                            var role = _userManager.AddToRoleAsync(user, item).Result;
                        }

                    }
                    catch (Exception e)
                    {

                        PageResponse.IsError = true;
                        return Ok(PageResponse);
                    }
                    transaction.Commit();
                };
            }

            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    foreach (var item in model.LaboratuvarList)
                    {
                        try
                        {
                            context.LaboratoryUsers.Add(new LaboratoryUser { LaboratoryId = item, LaboteUserId = user.Id });
                            context.SaveChanges();

                        }
                        catch (Exception e)
                        {

                        }

                    }
                    transaction.Commit();
                }
            }
            PageResponse.Data = true;
            return Ok(PageResponse);
        }
        [HttpGet("Delete/{id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<ActionResult<dynamic>> Delete(Guid id)
        {
            var user = new LaboteUser();
            using (LaboteContext context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
                    if (user.NotDelete)
                    {
                        PageResponse.IsError = true;
                        PageResponse.Message = "Bu Kayıt Silinemez";

                        return PageResponse;
                    }
                    user.IsDelete = true;

                    try
                    {
                        var usr = _userManager.UpdateAsync(user).Result;

                    }
                    catch (Exception e)
                    {

                        PageResponse.IsError = true;
                        return Ok(PageResponse);
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                };
            }


            PageResponse.Data = true;
            return Ok(PageResponse);
        }
    }
}
