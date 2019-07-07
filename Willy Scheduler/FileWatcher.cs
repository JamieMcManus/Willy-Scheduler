using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Willy_Scheduler.Services;

namespace Willy_Scheduler
{
    public class FileWather
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public void MonitorDirectory(string path, string file)

        {

            FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Path = path;

            fileSystemWatcher.Filter = file;

            fileSystemWatcher.Created += FileSystemWatcher_Created;

            fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;

            fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;

            fileSystemWatcher.Changed += FileSystemWatcher_Changed;

            fileSystemWatcher.EnableRaisingEvents = true;

        }

        public void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)

        {

            Console.WriteLine("File created: {0}", e.Name);

        }

        public void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)

        {

            Console.WriteLine("File renamed: {0}", e.Name);

        }

        public void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)

        {

            Console.WriteLine("File deleted: {0}", e.Name);

        }


        public void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)

        {

            Logger.Warn("File changed: {0}", e.Name);
            _Scheduler.RestartScheduler();

        }
    }
}
