using Xunit;

namespace NbtLib.Tests
{
    public class NbtByteTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValue()
        {
            var byte1 = new NbtByteTag(5);
            var byte2 = new NbtByteTag(5);

            Assert.Equal(byte1, byte2);
        }
    }
}
