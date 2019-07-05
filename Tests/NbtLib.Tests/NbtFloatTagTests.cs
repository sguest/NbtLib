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

        [Fact]
        public void Equals_ShouldReturnPayload()
        {
            var flt = new NbtFloatTag(6.78f);

            Assert.Equal("6.78", flt.ToString());
        }
    }
}
