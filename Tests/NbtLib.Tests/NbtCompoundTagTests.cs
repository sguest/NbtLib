using System;
using Xunit;

namespace NbtLib.Tests
{
    public class NbtCompoundTagTests
    {
        [Fact]
        public void Add_ShouldAssignNameIfBlank()
        {
            var stringTag = new NbtStringTag { Payload = "Some value" };
            var compoundTag = new NbtCompoundTag
            {
                { "Key name", stringTag }
            };

            Assert.Equal("Key name", stringTag.Name);
        }

        [Fact]
        public void Add_ShouldThrowIfNameConflict()
        {
            var stringTag = new NbtStringTag { Name = "Some name", Payload = "Some value" };
            var compoundTag = new NbtCompoundTag();

            Assert.Throws<InvalidOperationException>(() => compoundTag.Add("Other name", stringTag));
        }
    }
}
