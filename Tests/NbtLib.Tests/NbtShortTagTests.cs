using Xunit;

namespace NbtLib.Tests
{
    public class NbtShortTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValue()
        {
            var short1 = new NbtShortTag(5);
            var short2 = new NbtShortTag(5);

            Assert.Equal(short1, short2);
        }

        [Fact]
        public void ToString_ShouldReturnPayload()
        {
            var s = new NbtShortTag(6);

            Assert.Equal("6", s.ToString());
        }
    }
}
