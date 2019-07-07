using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NLog;
using Quartz;
using Quartz.Impl;

using Willy_Scheduler;
using Willy_Scheduler.Services;


namespace Willy_Scheduler
{
    public class Program
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static void Main(string[] args)
        {
            
            Logger.Info("Willy Scheduler started");
            

            _Scheduler.InitializeOrRefreshScheduler(null);

            string path = System.AppContext.BaseDirectory;
            FileWather watcher = new FileWather();
            string watchfile = "ScheduleConfiguration.xml";
            watcher.MonitorDirectory(path, watchfile);
            Logger.Info("Watching File: {0}", path);
            _Scheduler.StartScheduler();

            // Thread.Sleep(TimeSpan.FromSeconds(160));

            Console.WriteLine("Press To Close");
            Console.ReadKey();

        }
    }



    [XmlRoot(ElementName = "ReportItem")]
    public class ReportItem : IJob
    {

        [XmlAttribute(AttributeName = "TimeToRun")]
        public string TimeToRun { get; set; }

        [XmlAttribute(AttributeName = "Procedure")]
        public string Procedure { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Path")]
        public string Path { get; set; }
        [XmlAttribute(AttributeName = "Frequency")]
        public string Frequency { get; set; }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            ReportItem r = (ReportItem)dataMap["report"];

            Logger.Info("Executing Job Task for  " + r.Name);
            CreateCSVs.ReadFromDB(r.Procedure, r.Name, r.Path);
            //Print();
            return Task.CompletedTask;
        }


    }

}

[XmlRoot(ElementName = "ReportList")]
public class ReportList
{
    [XmlElement(ElementName = "ReportItem")]
    public List<ReportItem> ReportItem { get; set; }
}