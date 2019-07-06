using Xunit;

namespace NbtLib.Tests
{
    public class NbtStringTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValue()
        {
            var string1 = new NbtStringTag("abc");
            var string2 = new NbtStringTag("abc");

            Assert.Equal(string1, string2);
        }

        [Fact]
        public void ToString_ShouldReturnPayload()
        {
            var s = new NbtStringTag("def");

            Assert.Equal("def", s.ToString());
        }

        [Fact]
        public void ToJsonString_ShouldIncludeQuotes()
        {
            var s = new NbtStringTag("def");

            Assert.Equal("\"def\"", s.ToJsonString());
        }
    }
}
