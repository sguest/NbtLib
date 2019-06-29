using System;
using Xunit;

namespace NbtLib.Tests
{
    public class NbtListTests
    {
        [Fact]
        public void Add_ShouldEnforceMatchingType()
        {
            var list = new NbtListTag(NbtTagType.Int);

            Assert.Throws<InvalidOperationException>(() => list.Add(new NbtStringTag()));
        }

        [Fact]
        public void Insert_ShouldEnforceMatchingType()
        {
            var list = new NbtListTag(NbtTagType.Int);

            Assert.Throws<InvalidOperationException>(() => list.Insert(0, new NbtStringTag()));
        }
    }
}
