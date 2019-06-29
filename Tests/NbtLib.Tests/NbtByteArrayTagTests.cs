using Xunit;

namespace NbtLib.Tests
{
    public class NbtByteArrayTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValues()
        {
            var byte1 = new NbtByteArrayTag(new byte[] { 5, 6, 7 });
            var byte2 = new NbtByteArrayTag(new byte[] { 5, 6, 7 });

            Assert.Equal(byte1, byte2);
        }
    }
}
