using Xunit;

namespace NbtLib.Tests
{
    public class NbtLongTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValue()
        {
            var long1 = new NbtLongTag(5);
            var long2 = new NbtLongTag(5);

            Assert.Equal(long1, long2);
        }
    }
}
