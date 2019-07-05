using Xunit;

namespace NbtLib.Tests
{
    public class NbtDoubleTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValue()
        {
            var double1 = new NbtDoubleTag(5);
            var double2 = new NbtDoubleTag(5);

            Assert.Equal(double1, double2);
        }

        [Fact]
        public void ToString_ShouldReturnPayload()
        {
            var dbl = new NbtDoubleTag(1.23);

            Assert.Equal("1.23", dbl.ToString());
        }
    }
}
