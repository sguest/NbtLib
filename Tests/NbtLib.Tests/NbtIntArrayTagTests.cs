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
    }
}
