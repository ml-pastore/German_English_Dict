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
        public enum Args {ConfigFileName, CsvFolder}
        static string fileToProcess= "";

        static void Main(string[] args)
        {
            
            if(args.Length !=2){
                Console.WriteLine("arg[0]: name of config file");
                Environment.Exit((int) RetCodes.InvalidParm);
            }
            
            string configName = args[(int) Args.ConfigFileName];
            Console.WriteLine($"{configName}");

            if(! File.Exists(configName))
            {
                Console.WriteLine($"File does not exist: {configName}");
                Environment.Exit((int) RetCodes.ConfigFileNotFound);
            }

            IEnumerable<string> config = File.ReadAllLines(configName);
            string CsvFolder = args[(int) Args.CsvFolder];

            Dictionary<String, ParseFIle> cfKeys = GetParseFIleKeys();

            foreach(string cmdLn in config.Where(x => ! x.Trim().StartsWith(":")))
            {

                Console.WriteLine($"{cmdLn}");

                string[] cmdParts = cmdLn.Split(":");

                string cmd = cmdParts[0];
                string fileToClean = cmdParts[1].Replace("[workspaceFolder]", CsvFolder);

                if(! File.Exists(fileToClean))
                {
                    Console.WriteLine($"File does not exist: {fileToClean}");
                    Environment.Exit((int) RetCodes.CsvFileNotFound);
                }

                ParseFIle cf;

                if(! cfKeys.TryGetValue(cmd, out cf))
                {            
                    Console.WriteLine($"File key {cmd} not recognized, skipping...");
                    continue;
                }

                cf.fileName = fileToClean;
                cf.CleanTheFile();
            
            }

            Environment.Exit((int) RetCodes.Success);
        }

        static Dictionary<String, ParseFIle> GetParseFIleKeys()
        {
            
            Dictionary<String, ParseFIle> ret = new Dictionary<string, ParseFIle>();
            ret.Add("FileNoun", new ParseFIleNoun());
            ret.Add("FileAdj", new ParseFIleAdj());
            ret.Add("FileAdv", new ParseFIleAdv());
            ret.Add("FileVerb", new ParseFIleVerb());

            return ret;

        }

    } // class Program
}
