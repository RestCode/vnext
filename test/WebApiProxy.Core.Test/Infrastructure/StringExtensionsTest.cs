using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using WebApiProxy.Core.Infrastructure;

namespace WebApiProxy.Core.Test.Infrastructure
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Default_String_Value_If_Empty(string input)
        {
            var expected = "default";

            var actual = StringExtensions.DefaultIfEmpty(input, expected);

            Assert.Equal("default", actual);
        }

        [Theory]
        [InlineData("some text")]
        public void Original_String_Value_If_Not_Empty(string input)
        {
            var actual = StringExtensions.DefaultIfEmpty(input, "default");

            Assert.Equal(input, actual);
        }

        [Fact]
        public void String_To_Title_Case()
        {
            var expected = "Test";
            var actual = StringExtensions.ToTitle("test");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void String_To_Camel_Case()
        {
            var expected = "testString";
            var actual = StringExtensions.ToCamelCasing("TestString");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void String_To_XML_Summary_With_Triple_Slash()
        {
            var expected = "\n\t\t/// testing";
            var actual = StringExtensions.ToSummary("\ntesting");

            Assert.Equal(expected, actual);
        }
    }
}
