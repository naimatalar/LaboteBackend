using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Services.Interfaces
{
   public interface IUserService
    {
        Task<Guid> GetUserTopicId(Guid userId);
    }
}
