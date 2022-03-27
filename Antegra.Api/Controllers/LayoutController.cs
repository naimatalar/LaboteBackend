using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.Entities;
using Labote.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class LayoutController : LaboteControllerBase
    {
        private readonly UserManager<LaboteUser> _userManager;
        private readonly RoleManager<UserRole> _userRole;
        private readonly LaboteContext _context;
        public LayoutController(UserManager<LaboteUser> userManager, RoleManager<UserRole> userRole, LaboteContext context)
        {
            _userManager = userManager;
            _userRole = userRole;
            _context = context;
        }

        [HttpGet("GetLayoutData")]
        public async Task<ActionResult> GetLayoutData()
        {
            try
            {
                var user = _userManager.Users.Where(x => x.Id == User.Identity.UserId()).FirstOrDefault();
                var roles = await _userManager.GetRolesAsync(user);

                var menuModeules = _context.UserMenuModules.Include(x=>x.UserRole).Where(x=>!x.UserRole.IsDelete ) ;
                var menuList = new List<UserMenuModule>();
                foreach (var item in roles)
                {
                    var menu = menuModeules.Include(x=>x.MenuModel).Where(x => x.UserRole.Name == item).ToList();
                    menuList.AddRange(menu);
                }
                var MenuList = (from b in menuList
                                                group b by b.MenuModel into g
                                                select g.First()).Select(x=>x.MenuModel).ToList();

                PageResponse.Data =new
                {
                    user.FirstName,
                    user.Lastname,
                    user.Email,
                    MenuList= MenuList.OrderBy(x=>x.OrderNumber).Where(x=>!x.IsHidden).Select(x=>new { 
                    x.Id,
                    x.OrderNumber,
                    x.PageName,
                    x.PageUrl,
                    x.ParentId,
                    x.IconName,
                    x.IsMainPage
                    }),
                };
            }
            catch (Exception e)
            {

                throw;
            }
            
            return Ok(PageResponse);
        }
    }
}
