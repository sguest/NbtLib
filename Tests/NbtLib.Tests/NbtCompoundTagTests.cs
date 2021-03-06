﻿using System.Text;
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

        [Fact]
        public void ToString_ShouldReturnStringRepresentation()
        {
            var compound = new NbtCompoundTag
            {
                { "Int", new NbtIntTag(5) },
                { "Compound", new NbtCompoundTag
                {
                    { "String", new NbtStringTag("abc") }
                } }
            };

            Assert.Equal("{Int=5, Compound={String=abc}}", compound.ToString());
        }

        [Fact]
        public void ToJsonString_ShouldFormatResult()
        {
            var compound = new NbtCompoundTag
            {
                { "Int", new NbtIntTag(5) },
                { "Compound", new NbtCompoundTag
                {
                    { "String", new NbtStringTag("abc") }
                } }
            };

            var expected = new StringBuilder();
            expected.AppendLine("{")
                .AppendLine("  \"Int\": 5,")
                .AppendLine("  \"Compound\": {")
                .AppendLine("    \"String\": \"abc\"")
                .AppendLine("  }")
                .Append("}");

            Assert.Equal(expected.ToString(), compound.ToJsonString());
        }
    }
}
