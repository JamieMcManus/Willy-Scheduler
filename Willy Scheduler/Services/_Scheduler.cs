using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Willy_Scheduler.Services
{
    public static class _Scheduler
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static StdSchedulerFactory SchedulerFactory;
        public static IScheduler scheduler;
        private static XMLDeserialize _XMLDeserialize;


        public static void RestartScheduler()
        {
            InitializeOrRefreshScheduler(scheduler);

            StartScheduler();
        }

        public static IScheduler InitializeOrRefreshScheduler(IScheduler existingScheduler)
        {
            IScheduler returnSchedule = null;

            if (null != existingScheduler)
            {
                existingScheduler.Shutdown(true);
            }
            else
            {
                /* the existingScheduler is null, so it would be null on "first startup" */
            }
            _XMLDeserialize = new XMLDeserialize();
            SchedulerFactory = new StdSchedulerFactory();
            returnSchedule = SchedulerFactory.GetScheduler().Result;
            returnSchedule.Start();

            return returnSchedule;
        }


        public static void StartScheduler()
        {
            scheduler = InitializeOrRefreshScheduler(scheduler);

            List<ReportItem> source = _XMLDeserialize.GetList();

            try
            {
                // and start it off
                scheduler.Start();

                foreach (ReportItem o in source)
                {


                    var jobDataMap = new JobDataMap();
                    jobDataMap.Add("report", o);

                    // define the job and tie it to our class
                    IJobDetail job = JobBuilder.Create<ReportItem>()
                        .WithIdentity(o.Name, o.Name)        // this needs to be unique
                        .SetJobData(jobDataMap)
                        .Build();

                    // Trigger the job to run now
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity(o.Name, o.Name)
                        .StartNow()
                        .WithCronSchedule(o.TimeToRun)
                        .Build();

                    // Tell quartz to schedule the job using our trigger
                    scheduler.ScheduleJob(job, trigger);


                }

            }
            catch (SchedulerException se)
            {
                Logger.Error(se);
            }
            finally
            {
                Logger.Info("I'm a maniac, maniac, that's for sure...");
                Logger.Info("Schedule List Created");
            }


        }

    }
}
