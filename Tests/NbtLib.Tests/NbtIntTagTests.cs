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
    }
}
