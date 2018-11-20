using System;
using Xunit;
using System.IO;

namespace CsvParse.Tests
{
    public class VadlidHeader
    {

        [Fact]
        public void AssertRawFileHeaders()
        {

            HeadAssert ha = new HeadAssert{Fname = "GermanEnglishDict_nouns.csv"
                , pFile = new ParseFileNoun()
                , FType = "** NOUN **"};

            string fRow = ha.FirstLine();
            Assert.True(fRow == ha.pFile.FileRawHeader, $"Invalid raw {ha.FType} \nHeader: {fRow}\nExpected: {ha.ExpectedHeader()}");

            ha.Fname = "GermanEnglishDict_adjs.csv";
            ha.FType = " ** ADJ ** ";
            ha.pFile = new ParseFileAdj();
            fRow = ha.FirstLine();
            Assert.True(fRow == ha.pFile.FileRawHeader, $"Invalid raw {ha.FType} \nHeader: {fRow}\nExpected: {ha.ExpectedHeader()}");

        }

        public class HeadAssert{
            public string Fname = string.Empty;
            public ParseFile pFile ;
            public string FType = string.Empty;
            public string relPath = @"../../../../../";

            public string ExpectedHeader()
            {
                return pFile.FileRawHeader;
            }

            public string FirstLine()
            {
                return GetFirstLine(relPath + Fname);

            }

            string GetFirstLine(string fName)
            {

                string absName = Path.GetFullPath(fName);
            
                string ret = "";
                using (StreamReader reader = new StreamReader(absName)) {
                    ret = reader.ReadLine();
            }

                return ret;

            }

        }
      

      
        
    }
      
}
