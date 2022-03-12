using Labote.Api.BindingModel.RequestModel;
using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Labote.Services;


namespace Labote.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleManagerController : LaboteControllerBase
    {
        private const string pageName = "grup-tanimlari";
        private readonly AntegraContext _context;
        private readonly UserManager<AntegraUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        public RoleManagerController(AntegraContext context, UserManager<AntegraUser> userManager, RoleManager<UserRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("GetAllRoles")]
        
        public async Task<ActionResult<dynamic>> GetAllUsers()
        {

            var roles = _context.Roles.Where(x => !x.IsDelete);
            var uRoles = _context.UserRoles;
            var result = (from r in roles
                          select new
                          {
                              r.Name,
                              r.Id,
                              userCount = uRoles.Where(x => x.RoleId == r.Id && _context.Users.Where(y => !y.IsDelete && y.Id == x.UserId).Any()).Count()
                          });
            PageResponse.Data = result.ToList();
            return Ok(PageResponse);
        }
        [HttpPost("CreateRole")]
        [PermissionCheck(Action = pageName)]
        public async Task<ActionResult<dynamic>> CreateRole(CreateRoleRequestModel model)
        {
            var ress = _roleManager.CreateAsync(new UserRole { Name = model.Name, NormalizedName = model.Name.ToUpper() }).Result;
            //_context.Roles.Add(new UserRole { Name = model.Name ,NormalizedName=model.Name.ToUpper()});
            //_context.SaveChanges();
            return Ok(PageResponse);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<dynamic>> GetById(Guid id)
        {

            PageResponse.Data = _context.Roles.Where(x => x.Id == id).Select(x => new
            {
                x.Id,
                x.Name,
            }).FirstOrDefault();
            return Ok(PageResponse);
        }
        [HttpPost("EditRole")]
        [PermissionCheck(Action = pageName)]
        public async Task<ActionResult<dynamic>> EditRole(CreateRoleRequestModel model)
        {
            var role = _context.Roles.Where(x => x.Id == model.Id).FirstOrDefault();
         
            if (role.NotDelete)
            {
                PageResponse.IsError = true;
                PageResponse.Message = "Bu Kayıt Birincil Kayıttır Değiştirilemez.";
                return PageResponse;
            }
            role.Name = model.Name;
            role.NormalizedName = model.Name.ToUpper();
            _context.Update(role);
            _context.SaveChanges();
            return Ok(PageResponse);
        }
        [HttpGet("Delete/{id}")]
        [PermissionCheck(Action = pageName)]
        public async Task<ActionResult<dynamic>> Delete(Guid id)
        {
            var role = _context.Roles.Where(x => x.Id == id).FirstOrDefault();
            if (role.NotDelete)
            {
                PageResponse.IsError = true;
                PageResponse.Message = "Bu Kayıt Silinemez";

                return PageResponse;
            }
            role.IsDelete = true;

            _context.Update(role);
            _context.SaveChanges();
            return Ok(PageResponse);
        }


    }
}
