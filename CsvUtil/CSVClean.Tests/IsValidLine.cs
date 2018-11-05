using System;
using Xunit;
using csvclean;

namespace csvclean.tests
{
    public class UnitTest1
    {

        CleanFile _cf = new CleanFile();

        [Fact]
        public void ReturnFalseBadLn()
        {

            string badLine = "abc#123";
            bool result = _cf.LineIsValid(badLine);
            Assert.False(result, $"Should be false: {badLine}");

        }

        [Theory]
        [InlineData("äöüÄÖÜßß")]
        [InlineData("123")]
        [InlineData("do-re-mi")]
        public void ReturnTrueGoodLn(string ln)
        {
            bool result = _cf.LineIsValid(ln);
            Assert.True(result, $"Should be true: {ln}");
        }


    }
}
