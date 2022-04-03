using Labote.Core;
using Labote.Core.Entities;
using Labote.Services.BindingModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Labote.Services
{
    public class PermissionCheck : Attribute, IActionFilter
    {
        public string Action { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.User.Identity.UserId();
            var mmoduls = new List<string>();
            using (LaboteContext dbcontext = new LaboteContext())
            {
                using (var transaction = dbcontext.Database.BeginTransaction())
                {
                    var roles = dbcontext.UserRoles.Where(x => x.UserId == userId).ToList();
                    foreach (var item in roles)
                    {
                        var menuModuls = dbcontext.UserMenuModules.Include(x=>x.UserRole).Where(x => x.UserRoleId == item.RoleId && x.MenuModel.IsHidden).Where(x=>!x.UserRole.IsDelete).Select(x => x.MenuModel).ToList();
                        foreach (var jitem in menuModuls)
                        {
                            mmoduls.Add(jitem.PageUrl);
                        }
                    }
                }
            }
            var permission = mmoduls.Where(x => x == Action).Any();
            if (!permission)
            {
     
                context.HttpContext.Response.StatusCode = 403;
                throw new MethodAccessException("Yetkisiz İşlem");
            }

        }
    }
}
