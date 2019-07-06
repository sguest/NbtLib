using System;
using Xunit;

namespace NbtLib.Tests
{
    public class NbtListTagTests
    {
        [Fact]
        public void Add_ShouldEnforceMatchingType()
        {
            var list = new NbtListTag(NbtTagType.Int);

            Assert.Throws<InvalidOperationException>(() => list.Add(new NbtStringTag("Invalid")));
        }

        [Fact]
        public void Insert_ShouldEnforceMatchingType()
        {
            var list = new NbtListTag(NbtTagType.Int);

            Assert.Throws<InvalidOperationException>(() => list.Insert(0, new NbtStringTag("Invalid")));
        }

        [Fact]
        public void Equals_ShouldCompareListContent()
        {
            var listOne = new NbtListTag(NbtTagType.Int)
            {
                new NbtIntTag(1),
                new NbtIntTag(2),
                new NbtIntTag(3)
            };

            var listTwo = new NbtListTag(NbtTagType.Int)
            {
                new NbtIntTag(1),
                new NbtIntTag(2),
                new NbtIntTag(3)
            };

            Assert.Equal(listOne, listTwo);
        }

        [Fact]
        public void ToString_ShouldReturnArrayRepresentation()
        {
            var list = new NbtListTag(NbtTagType.String)
            {
                new NbtStringTag("abc"),
                new NbtStringTag("def"),
                new NbtStringTag("ghi")
            };

            Assert.Equal("[abc, def, ghi]", list.ToString());
        }

        [Fact]
        public void ToJsonString_ShouldUseJsonForChildren()
        {
            var list = new NbtListTag(NbtTagType.String)
            {
                new NbtStringTag("abc"),
                new NbtStringTag("def"),
                new NbtStringTag("ghi")
            };

            Assert.Equal("[\"abc\", \"def\", \"ghi\"]", list.ToJsonString());
        }
    }
}
