using Xunit;

namespace NbtLib.Tests
{
    public class NbtIntTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValue()
        {
            var int1 = new NbtIntTag(5);
            var int2 = new NbtIntTag(5);

            Assert.Equal(int1, int2);
        }

        [Fact]
        public void ToString_ShouldReturnPayload()
        {
            var i = new NbtIntTag(6);

            Assert.Equal("6", i.ToString());
        }
    }
}
