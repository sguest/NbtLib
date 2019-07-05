using Xunit;

namespace NbtLib.Tests
{
    public class NbtIntArrayTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValues()
        {
            var int1 = new NbtIntArrayTag(new int[] { 5, 6, 7 });
            var int2 = new NbtIntArrayTag(new int[] { 5, 6, 7 });

            Assert.Equal(int1, int2);
        }

        [Fact]
        public void ToString_ShouldReturnIntArray()
        {
            var array = new NbtIntArrayTag(new int[] { 8, 9, 10 });

            Assert.Equal("[8, 9, 10]", array.ToString());
        }
    }
}
