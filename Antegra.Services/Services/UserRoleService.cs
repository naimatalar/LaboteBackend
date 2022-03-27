using Labote.Core;
using Labote.Core.Entities;
using Labote.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace Labote.Services.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly LaboteContext _context;

        public UserRoleService(LaboteContext context)
        {
            _context = context;
        }

        public async Task<List<UserRole>> GetUserRoleById(Guid Id)
        {
            var roles = _context.Roles.Where(x=>x.IsActive);
            var uRoles = _context.UserRoles;
            var listRoles = (from r in roles
                             from u in uRoles.Where(x => x.RoleId == r.Id && x.UserId==Id)
                             select new
                             {
                                 r
                             }).Select(x=>x.r).ToList();
            return listRoles;
        }

        public async Task<List<string>> GetUserRoleNamesByUserId(Guid Id)
        {
            var roles = _context.Roles.Where(x => x.IsActive);
            var uRoles = _context.UserRoles;
            var listRoles = (from r in roles
                             from u in uRoles.Where(x => x.RoleId == r.Id && x.UserId == Id)
                             select new
                             {
                                 r.Name
                             }).Select(x => x.Name).ToList();
            return listRoles;
        }
    }
}
