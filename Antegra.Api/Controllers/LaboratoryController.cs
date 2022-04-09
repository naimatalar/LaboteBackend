using Labote.Api.BindingModel;
using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Entities;
using Labote.Services;
using Labote.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryController : LaboteControllerBase
    {
        private readonly LaboteContext _context;
        private readonly IUserRoleService _userRoleService;

        private const string pageName = "laboratuvar-tanimlama";

        public LaboratoryController(LaboteContext context, IUserRoleService userRoleService)
        {
            _context = context;
            _userRoleService = userRoleService;
        }

        [HttpGet("GetCurrentTopicLaboratory")]
        public async Task<dynamic> GetCurrentTopicLaboratory()
        {
            var userId = User.Identity.UserId();
           
            var topicId = _context.Users.Where(x => x.Id == userId).FirstOrDefault()?.UserTopicId;
            var laboratories = _context.Laboratories.Where(x => x.UserTopicId == topicId).Select(x => new
            {
                x.Name,
                x.Code,
                x.Description,
                x.Id,
                LaboratoryUserCount=x.LaboratoryUsers.Count(),
                DeviceCount=x.LaboratoryDevice.Count()
            }).ToList();
            PageResponse.Data = laboratories;
            return PageResponse;
        }
        [HttpGet("GetLaboratoryListByCurrentUser")]
        public async Task<dynamic> GetLaboratoryListByCurrentUser()
        {
            var userId = User.Identity.UserId();

           
            var laboratories = _context.LaboratoryUsers.Where(x => x.LaboteUserId == userId).Select(x => new
            {
                x.Laboratory.Name,
                x.Laboratory.Code,
                x.Laboratory.Description,
                x.Laboratory.Id,
             
            }).ToList();
            PageResponse.Data = laboratories;
            return PageResponse;
        }
        [HttpGet("GetAllCurrentLaboratory")]
        public async Task<dynamic> GetAllCurrentLaboratory()
        {
            var userId = User.Identity.UserId();
            var laboratoryUsers = _context.Users.Where(x => x.Id == userId).SelectMany(x => x.LaboratoryUsers).Select(x => new
            {
                x.Laboratory.Name,
                x.Laboratory.Id,
            });

            PageResponse.Data = laboratoryUsers;
            return PageResponse;
        }
        [HttpGet("GetById/{Id}")]
        public async Task<dynamic> GetById(Guid Id)
        {
            var userId = User.Identity.UserId();
            var laboratories = _context.Laboratories.Where(x => x.Id == Id).Select(x => new
            {
                x.Name,
                x.Code,
                x.Description,
                x.Id
            }).FirstOrDefault();

            PageResponse.Data = laboratories;
            return PageResponse;
        }
        [HttpPost("CreateLaboratory")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> CreateLaboratory(LaboratoryCreateRequestModel model)
        {
            var userId = User.Identity.UserId();
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            bool isFirstLab = false;
            var labb = new Laboratory();
            using (var context = new LaboteContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var laboratuvar = context.Laboratories.Count();
                    if (laboratuvar == 0)
                    {
                        isFirstLab = true;
                    }
                    if (model.Id == null)
                    {
                        labb = new Laboratory
                        {
                            Code = model.Code,
                            Description = model.Description,
                            Name = model.Name,
                            UserTopicId = user.UserTopicId,
                        };
                        context.Laboratories.Add(labb);
                        context.SaveChanges();
                    }
                    else
                    {
                        var data = context.Laboratories.Where(x => x.Id == model.Id).FirstOrDefault();
                        data.Code = model.Code;
                        data.Description = model.Description;
                        data.Name = model.Name;
                        data.UserTopicId = user.UserTopicId;
                        context.Update(data);
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
                PageResponse.Data = new
                {
                    Refres = false
                };
            }
            if (isFirstLab)
            {
                using (var context = new LaboteContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        context.LaboratoryUsers.Add(new LaboratoryUser
                        {
                            LaboratoryId = labb.Id,
                            LaboteUserId = userId
                        });
                        context.SaveChanges();
                        transaction.Commit();
                    }


                }
                PageResponse.Data = new
                {
                    Refres = true
                };
            }

            return PageResponse;
        }

        [HttpGet("GetUserByLaboratory/{Id}")]
        public async Task<dynamic> GetUserByLaboratory(Guid Id)
        {
            try
            {
                var laboteUser = _context.LaboratoryUsers.Where(x => x.LaboratoryId == Id).Select(x => x.LaboteUser)
                               .Select(x => new
                               {
                                   x.Id,
                                   x.FirstName,
                                   x.Lastname,
                                   x.Email,
                                   Role = new List<string>()
                               }).ToList();
                var listdata = new List<dynamic>();
                foreach (var item in laboteUser)
                {

                    var roles = await _userRoleService.GetUserRoleNamesByUserId(item.Id);
                    /*tem.Role=roles;*/
                    listdata.Add(new
                    {
                        item.Id,
                        item.FirstName,
                        item.Lastname,
                        item.Email,
                        Role = roles
                    });
                }
                PageResponse.Data = listdata;
            }
            catch (Exception e)
            {

                throw;
            }

            return PageResponse;
        }
        [HttpPost("GetUserByLaboratoryUserName")]
        public async Task<dynamic> GetUserByLaboratoryUserName(GetUserByLaboratoryUserNameModel model)
        {
            var userId = User.Identity.UserId();
            var currentUser = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (currentUser==null)
            {
                PageResponse.IsError = true;
                PageResponse.Message = "Kullanıcı bulunamadı";
                return PageResponse;
            };
            try
            {
                var laboteUser = _context.Users.Where(x => x.UserTopicId == currentUser.UserTopicId).Where(x => x.FirstName.ToLower().Contains(model.Query.ToLower()) || x.Lastname.ToLower().Contains(model.Query.ToLower()))
                               .Select(x => new
                               {
                                   x.Id,
                                   x.FirstName,
                                   x.Lastname,
                                   x.Email,
                                   Role = new List<string>()
                               }).ToList();
                var listdata = new List<dynamic>();
                foreach (var item in laboteUser)
                {

                    var roles = await _userRoleService.GetUserRoleNamesByUserId(item.Id);
                    /*tem.Role=roles;*/
                    listdata.Add(new
                    {
                        item.Id,
                        item.FirstName,
                        item.Lastname,
                        item.Email,
                        Role = roles
                    });
                }
                PageResponse.Data = listdata;
            }
            catch (Exception e)
            {

                throw;
            }

            return PageResponse;
        }

        [HttpPost("SetOrRemoveLaboratoryOnUser")]
        [PermissionCheck(Action = pageName)]
        public async Task<dynamic> SetOrLaboratoryOnUSer(SetOrLaboratoryOnUserRequestModel model)
        {
            if (model.Action == 1)
            {
               
                if (_context.LaboratoryUsers.Any(x => x.LaboteUserId == model.UserId && x.LaboratoryId==model.LaboratoryId))
                {
                    PageResponse.IsError = true;
                    PageResponse.Message = "Bu kullanıcı bu laboratuvara zaten atanmış";
                    return PageResponse;
                }
                _context.LaboratoryUsers.Add(new LaboratoryUser
                {
                    LaboratoryId = model.LaboratoryId,
                    LaboteUserId = model.UserId,
                });
                _context.SaveChanges();
            }
            else
            {
                var data = _context.LaboratoryUsers.Where(x => x.LaboratoryId == model.LaboratoryId && x.LaboteUserId == model.UserId);
                _context.LaboratoryUsers.RemoveRange(data);
                _context.SaveChanges();
            }

            return true;
        }

    }
}
