using CsvHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Willy_Scheduler.Services
{
    public static class CreateCSVs
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static void ReadFromDB(string sp_name, string fileName, string path)
        {

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionString"];

            try
            {
                DataTable ds = new DataTable("reports");
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    MySqlCommand sqlComm = new MySqlCommand(sp_name, conn);

                    sqlComm.CommandType = CommandType.StoredProcedure;

                    using (MySqlDataAdapter da = new MySqlDataAdapter())
                    {
                        da.SelectCommand = sqlComm;

                        da.Fill(ds);
                    }

                }


                List<dynamic> rows = ds.ToDynamic();

                System.IO.Directory.CreateDirectory("CSV_HUB");
                string fullPath;
                if (path == "default")
                {
                    fullPath = System.AppContext.BaseDirectory + "\\CSV_HUB\\";
                }
                else if (path.Contains(":"))
                {
                    fullPath = path;
                }
                else
                {
                    if (!Directory.Exists(System.AppContext.BaseDirectory + "\\CSV_HUB\\" + path))
                    {
                        Directory.CreateDirectory(System.AppContext.BaseDirectory + "\\CSV_HUB\\" + path);
                    }

                    fullPath = System.AppContext.BaseDirectory + "\\CSV_HUB\\" + path + "\\";
                }
                using (var writer = new StreamWriter(String.Format(fullPath + "{0}_{1}.csv", fileName, DateTime.Now.ToString("_MMddyyyy"))))
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(rows);
                    writer.Flush();
                }
                Logger.Info("Successfully CSV For {0} Created", fileName);
            }
            catch (Exception ex)
            {
                Logger.Error("WILLY Found a MYSQL PROBLEM Executing {0} Procedure", sp_name);
                Logger.Error(ex.Message);
            }




        }
    }
}
