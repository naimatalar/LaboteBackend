using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labote.Core.Entities
{
    public class JobScheduleTime:BaseEntity
    {
        public Constants.Enums.JobScheduleTimeType JobScheduleTimeType { get; set; }
        public int Time { get; set; }
  
}
}
