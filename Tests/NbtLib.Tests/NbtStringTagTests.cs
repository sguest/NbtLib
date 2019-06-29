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
    }
}
