using Labote.Core;
using Labote.Core.Constants;

using Hangfire;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labote.Api.BackgroundJobs
{
    public class HangFire : IHangfire
    {

        private readonly LaboteContext _context;


        public HangFire( LaboteContext context)
        {

            _context = context;
            
        }

        public bool ClearJobById(string jobId)
        {
            try
            {
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {

                        RecurringJob.RemoveIfExists(jobId);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool StartUpLive(IBackgroundJobClient cl)
        {
            //var dd = cl.Enqueue(() => _orderService.Test());
            //BackgroundJob.Delete(dd);
            return true;
        }
        public void CreateDb()
        {
           
        }
        public bool ClearAllJob()
        {
            try
            {
                using (var connection = JobStorage.Current.GetConnection())
                {
                    foreach (var recurringJob in connection.GetRecurringJobs())
                    {

                        RecurringJob.RemoveIfExists(recurringJob.Id);
                    }
                    
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [AutomaticRetry(Attempts = 0)]
        public bool StartGetOrderJobHepsiburada(Guid marketPlaceId)
        {
            ClearJobById(marketPlaceId.ToString());
          

            //string cronExpression = "";
            //int cs = 60;
            //if (data.JobScheduleTimeType == Enums.JobScheduleTimeType.Day)
            //{
            //    cs = 3;
            //}
            //else if (data.JobScheduleTimeType == Enums.JobScheduleTimeType.Hour)
            //{
            //    cs = 12;
            //}
            //for (int i = 1; i < cs; i = i + (int)data.JobScheduleTime)
            //{
            //    cronExpression += i.ToString() + ",";
            //}
            //cronExpression = cronExpression.Remove(cronExpression.Length - 1);
            //string cronEx = "";
            //if (data.JobScheduleTimeType == Enums.JobScheduleTimeType.Minute)
            //{
            //    cronEx = cronExpression + " * * * *";

            //}
            //else if (data.JobScheduleTimeType == Enums.JobScheduleTimeType.Hour)
            //{
            //    cronEx = "* " + cronExpression + " * * *";
            //}
            //else if (data.JobScheduleTimeType == Enums.JobScheduleTimeType.Day)
            //{
            //    cronEx = "* * " + cronExpression + " * *";
            //}

            //if (_context.AppInfo.Where(x => x.IsActive).Any())
            //{
            //    try
            //    {
            //        RecurringJob.AddOrUpdate(marketPlaceId.ToString(), () => _orderService.StartOrderSync(marketPlaceId), cronEx);
            //        return true;
            //    }
            //    catch (Exception e)
            //    {

            //        return false;
            //    }

            //}
            return false;
        }
    }
}
