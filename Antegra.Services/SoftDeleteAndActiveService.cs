using Labote.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Services
{
   public static class SoftDeleteAndActiveService
    {
        public static IQueryable<T> GetNoneDelete<T>(this IQueryable<T> entity) where T: BaseEntity
        {
           return entity.Where(x => x.IsDelete != true);
        }
    }
}
