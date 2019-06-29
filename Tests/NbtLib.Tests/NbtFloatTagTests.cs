using Xunit;

namespace NbtLib.Tests
{
    public class NbtFloatTagTests
    {
        [Fact]
        public void Equals_ShouldCompareValue()
        {
            var float1 = new NbtFloatTag(5);
            var float2 = new NbtFloatTag(5);

            Assert.Equal(float1, float2);
        }
    }
}
