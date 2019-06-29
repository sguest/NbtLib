using Xunit;

namespace NbtLib.Tests
{
    public class NbtLongArrayTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValues()
        {
            var long1 = new NbtLongArrayTag(new long[] { 5, 6, 7 });
            var long2 = new NbtLongArrayTag(new long[] { 5, 6, 7 });

            Assert.Equal(long1, long2);
        }
    }
}
