using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BackgroundJobs
{
    public interface IHangfire
    {
    
        bool ClearJobById(string jobId);
        bool ClearAllJob();
        bool StartUpLive(IBackgroundJobClient cl);
      
    }
}
