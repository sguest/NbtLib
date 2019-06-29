using System;
using Xunit;

namespace NbtLib.Tests
{
    public class NbtCompoundTagTests
    {
        [Fact]
        public void Equals_ShouldCompareChildContent()
        {
            var compoundOne = new NbtCompoundTag()
            {
                { "Int", new NbtIntTag(5) },
                { "String", new NbtStringTag("abc") }
            };

            var compoundTwo = new NbtCompoundTag()
            {
                { "Int", new NbtIntTag(5) },
                { "String", new NbtStringTag("abc") }
            };

            Assert.Equal(compoundOne, compoundTwo);
        }
    }
}
