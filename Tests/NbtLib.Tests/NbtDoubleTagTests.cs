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
    }
}
