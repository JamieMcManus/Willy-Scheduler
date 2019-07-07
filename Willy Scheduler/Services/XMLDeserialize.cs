using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Willy_Scheduler.Services
{

    public class XMLDeserialize
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public List<ReportItem> GetList()
        {

            ReportList rpt;
            try
            {
                XmlSerializer myDeserilizer = new XmlSerializer(typeof(ReportList));
                FileStream myfilestream = new FileStream(System.AppContext.BaseDirectory + "\\ScheduleConfiguration.xml", FileMode.Open);
                var loadedData = myDeserilizer.Deserialize(myfilestream);
                myfilestream.Close();
                rpt = (ReportList)loadedData;
                return rpt.ReportItem;

            }
            catch
            {
                Logger.Info("Willie hears ya, Willie don't care");
                Logger.Error("Error Encountered in XML Deserializing");
                return null;
            }



        }
    }
}
