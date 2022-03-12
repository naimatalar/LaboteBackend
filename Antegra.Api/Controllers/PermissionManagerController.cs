using Labote.Api.Controllers.LaboteController;
using Labote.Core;
using Labote.Core.BindingModels;
using Labote.Core.Entities;
using Labote.Services;
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
    public class PermissionManagerController : LaboteControllerBase
    {
        private const string pageName = "yetki-tanimlari";
        private readonly AntegraContext _context;
        private readonly UserManager<AntegraUser> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        public PermissionManagerController(AntegraContext context, UserManager<AntegraUser> userManager, RoleManager<UserRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }



        [HttpGet("GetRoleWithMenuModuleByRoleId/{id}")]
        public async Task<ActionResult<dynamic>> GetRoleWithMenuModule(Guid id)
        {
            var Role = _context.Roles.Include(x => x.UserMenuModules).Where(x => x.Id == id).FirstOrDefault();
            var MenuModules = _context.MenuModules.OrderBy(x => x.OrderNumber).ToList();
            PageResponse.Data = new
            {
                Role = new
                {
                    Role.Name,
                    Role.Id,
                    UserMenuModules = Role.UserMenuModules.Select(x => new
                    {
                        x.MenuModelId,
                        x.Id
                    }).ToList()
                },
                MenuModules = MenuModules.Select(x => new
                {
                    x.Id,
                    x.PageUrl,
                    x.PageName,
                    x.IsMainPage,
                    x.ParentId,
                    x.IconName
                })
            };

            return Ok(PageResponse);
        }


        [HttpPost("MultiMenuActivateToggle")]
        [PermissionCheck(Action = pageName)]
        public async Task<ActionResult<dynamic>> MultiMenuActivateToggle(MenuActivateToggle model)
        {
            try
            {
                var userRolemenu = _context.UserMenuModules.Include(x => x.UserRole).Where(x => x.MenuModelId == model.MenuId && x.UserRole.Id == model.RoleId).FirstOrDefault();
                var menu = _context.MenuModules.Where(x => x.Id == model.MenuId || x.ParentId == model.MenuId).ToList();
                if (userRolemenu != null && userRolemenu.UserRole.NotDelete)
                {
                    PageResponse.IsError = true;
                    PageResponse.Message = "Bu Kayıt Birincil Kayıttır Değiştirilemez.";
                    return PageResponse;
                }

                if (userRolemenu == null)
                {
                    var sbMenu = menu.Where(x => x.ParentId != null).ToList();
                    if (sbMenu != null)
                    {
                        foreach (var item in sbMenu)
                        {
                            var submenuList = _context.MenuModules.Where(x => x.ParentId == item.Id).ToList();
                            foreach (var jitem in submenuList)
                            {
                                menu.Add(jitem);
                            }
                        }
                    }
                    foreach (var item in menu)
                    {

                        _context.UserMenuModules.Add(new UserMenuModule
                        {
                            MenuModelId = item.Id,
                            UserRoleId = model.RoleId
                        });
                    }
                    _context.SaveChanges();
                }
                else
                {
                    var sbMenu = menu.Where(x => x.ParentId != null).ToList();
                    if (sbMenu != null)
                    {
                        foreach (var item in sbMenu)
                        {
                            var submenuList = _context.MenuModules.Where(x => x.ParentId == item.Id).ToList();
                            foreach (var jitem in submenuList)
                            {
                                var rls = _context.UserMenuModules.Include(x=>x.UserRole).Include(x => x.UserRole).Where(x => x.MenuModelId == jitem.Id && x.UserRole.Id==model.RoleId);
                                _context.UserMenuModules.RemoveRange(rls);
                            }
                        }
                    }
                    foreach (var item in menu)
                    {
                        
                        var rls = _context.UserMenuModules.Include(x=>x.UserRole).Where(x => x.MenuModelId == item.Id || x.MenuModelId == item.ParentId).Where(x=>x.UserRole.Id == model.RoleId).ToList();
                        _context.UserMenuModules.RemoveRange(rls);
                    }
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {


            }

            return Ok(PageResponse);
        }

      
        [HttpGet("FrontEndPermissionCheck/{pagename}")]
        public async Task<ActionResult<dynamic>> FrontEndPermissionCheck(string pagename)
        {
            var userId = User.Identity.UserId();
            var mmoduls = new List<string>();
            using (AntegraContext dbcontext = new AntegraContext())
            {
                using (var transaction = dbcontext.Database.BeginTransaction())
                {
                    var roles = dbcontext.UserRoles.Where(x => x.UserId == userId).ToList();
                    foreach (var item in roles)
                    {
                        var menuModuls = dbcontext.UserMenuModules.Include(x=>x.UserRole).Where(x => x.UserRoleId == item.RoleId && !x.MenuModel.IsHidden).Where(x=>!x.UserRole.IsDelete).Select(x => x.MenuModel).ToList();
                        foreach (var jitem in menuModuls)
                        {
                            mmoduls.Add(jitem.PageUrl);
                        }

                    }

                }

            }
            var permission = mmoduls.Where(x => x == pagename).Any();
            if (!permission)
            {
                PageResponse.IsError = true;
                return Ok(PageResponse);
            }

            return Ok(PageResponse);
        }


        [HttpPost("SingleMenuActivateToggle")]
        [PermissionCheck(Action = pageName)]
        public async Task<ActionResult<dynamic>> SingleMenuActivateToggle(MenuActivateToggle model)
        {
            try
            {
                var userRolemenu = _context.UserMenuModules.Include(x => x.UserRole).Where(x => x.MenuModelId == model.MenuId && x.UserRoleId == model.RoleId).FirstOrDefault();
                if (userRolemenu != null && userRolemenu.UserRole.NotDelete)
                {
                    PageResponse.IsError = true;
                    PageResponse.Message = "Bu Kayıt Birincil Kayıttır Değiştirilemez.";
                    return PageResponse;
                }
                var menu = _context.MenuModules.Where(x => x.Id == model.MenuId || x.ParentId == model.MenuId).ToList();
                if (userRolemenu == null)
                {

                    foreach (var item in menu)
                    {
                        _context.UserMenuModules.Add(new UserMenuModule
                        {
                            MenuModelId = item.Id,
                            UserRoleId = model.RoleId
                        });

                    }


                    _context.SaveChanges();

                }
                else
                {
                    var sbMenu = menu.Where(x => x.ParentId != null).ToList();
                    if (sbMenu.Count > 0)
                    {
                        foreach (var item in sbMenu)
                        {

                            var rlss = _context.UserMenuModules.Include(x => x.UserRole).Where(x => x.MenuModelId == item.Id && x.UserRoleId == model.RoleId);
                            _context.UserMenuModules.RemoveRange(rlss);

                        }
                    }
                    //var rls = _context.UserMenuModules.Where(x => x.MenuModelId == menu.Id).FirstOrDefault();
                    //_context.UserMenuModules.Remove(rls);

                    _context.SaveChanges();
                }

            }
            catch (Exception e)
            {


            }

            return Ok(PageResponse);
        }



    }
}
