using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsvClean
{
    class Program
    {

        public enum RetCodes {Success, InvalidParm, ConfigFileNotFound, CsvFileNotFound}

        static void Main(string[] args)
        {
            
            string configName = "CsvClean.config";
            Console.WriteLine($"{configName}");

            if(! File.Exists(configName))
            {
                Console.WriteLine($"File does not exist: {configName}");
                Environment.Exit((int) RetCodes.ConfigFileNotFound);
            }

            IEnumerable<string> config = File.ReadAllLines(configName);
            
            Dictionary<String, CleanFile> cfKeys = GetCleanFileKeys();

            foreach(string cmdLn in config.Where(x => ! x.Trim().StartsWith("::")))
            {

                Console.WriteLine($"{cmdLn}");

                string[] cmdParts = cmdLn.Split(":");

                string cmd = cmdParts[0];
                string fileToClean = cmdParts[1];

                if(! File.Exists(fileToClean))
                {
                    Console.WriteLine($"File does not exist: {fileToClean}");
                    continue;
                }

                CleanFile cf;

                if(! cfKeys.TryGetValue(cmd, out cf))
                {            
                    Console.WriteLine($"File key {cmd} not recognized, skipping...");
                    continue;
                }

                using(ILogger lg = new Logger())
                {
                    lg.LogFile = "_log/runLog.txt";
                    cf.ILog = lg;
                    cf.fileName = fileToClean;
                    cf.CleanTheFile();
                }
            }

            Environment.Exit((int) RetCodes.Success);
        }

        static Dictionary<String, CleanFile> GetCleanFileKeys()
        {
            
            Dictionary<String, CleanFile> ret = new Dictionary<string, CleanFile>();
            ret.Add("FileNoun", new CleanFileNoun());
            ret.Add("FileAdj", new CleanFileAdj());
            ret.Add("FileAdv", new CleanFileAdv());
            ret.Add("FileVerb", new CleanFileVerb());

            return ret;

        }

    } // class Program
}
