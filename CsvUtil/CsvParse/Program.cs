using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CsvParse
{
    class Program
    {

        public enum RetCodes {Success, InvalidParm, ConfigFileNotFound, CsvFileNotFound}
     
        static string fileToProcess= "";

        static void Main(string[] args)
        {
            
            string configName = "CsvParse.config";
            Console.WriteLine($"{configName}");

            if(! File.Exists(configName))
            {
                Console.WriteLine($"File does not exist: {configName}");
                Environment.Exit((int) RetCodes.ConfigFileNotFound);
            }

            IEnumerable<string> config = File.ReadAllLines(configName);
        
            Dictionary<String, ParseFile> cfKeys = GetParseFileKeys();

            string combinedBuff = string.Empty;

            string logName = "_log/runLog.txt";

            foreach(string cmdLn in config.Where(x => ! x.Trim().StartsWith(":")))
            {

                Console.WriteLine($"{cmdLn}");

                string[] cmdParts = cmdLn.Split(":");

                string cmd = cmdParts[0];
                string fileToParse = cmdParts[1];

                if(! File.Exists(fileToParse))
                {
                    Console.WriteLine($"File does not exist: {fileToParse}");
                    Environment.Exit((int) RetCodes.CsvFileNotFound);
                }

                ParseFile cf;

                if(! cfKeys.TryGetValue(cmd, out cf))
                {            
                    Console.WriteLine($"File key {cmd} not recognized, skipping...");
                    continue;
                }


                using(ILogger lg = new Logger())
                {
                    lg.LogFile = logName;
                    lg.Write(new string('-', 50));
                    lg.Write($"{DateTime.Now}");

                    cf.ILog = lg;
                    cf.FileName = fileToParse;

                    lg.Write(cf.FileRawHeader);

                    string buff = cf.GetJSON();

                    if(! string.IsNullOrEmpty(buff))
                    {

                        bool emptyCombBuff = string.IsNullOrEmpty(combinedBuff);

                        // remove closing "]" and opening "[" so this is full json 
                        if(! emptyCombBuff)
                        {
                         combinedBuff = combinedBuff.Substring(0,combinedBuff.Length-1);
                         buff = string.Concat(",",buff.Substring(1));
                        }

                        combinedBuff +=  buff;
                    }
                }

            }


              using(ILogger lg = new Logger())
                {
                    lg.LogFile = logName;
                    lg.Write(new string('-', 50));
                    string outFile = @"_out\" + "GermanEnglishDict.json";
                    lg.Write($"Outputting to: {outFile}");
                    File.WriteAllText(outFile, combinedBuff);
                    lg.Write("Done");
                }

            Environment.Exit((int) RetCodes.Success);
        }

        static Dictionary<String, ParseFile> GetParseFileKeys()
        {
            
            Dictionary<String, ParseFile> ret = new Dictionary<string, ParseFile>();
            ret.Add("FileNoun", new ParseFileNoun());
            ret.Add("FileAdj", new ParseFileAdj());
            ret.Add("FileAdv", new ParseFileAdv());
            ret.Add("FileVerb", new ParseFileVerb());

            return ret;

        }

    } // class Program
}
