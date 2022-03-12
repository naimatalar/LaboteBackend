using Labote.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Services.Interfaces
{
   public interface IUserRoleService
    {
        List<UserRole> GetUserRoleById(Guid Id);
    }
}
