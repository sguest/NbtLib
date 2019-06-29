using Xunit;

namespace NbtLib.Tests
{
    public class NbtEndTagTests
    {
        [Fact]
        public void Equals_ShouldReturnTrue()
        {
            var end1 = new NbtEndTag();
            var end2 = new NbtEndTag();

            Assert.Equal(end1, end2);
        }
    }
}
