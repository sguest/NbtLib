﻿using System.Linq;
using Xunit;

namespace NbtLib.Tests
{
    public class NbtParserTests
    {
        [Fact]
        public void ParseNbtStream_ShouldReadSimpleFile()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\simple.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);

                Assert.Equal("Root Tag", parsed.Name);
                Assert.Equal(5, (parsed["Int 5"] as NbtIntTag).Payload);
                Assert.Equal("abcd", (parsed["String abcd"] as NbtStringTag).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldReadPrimitiveTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\primitives.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);

                Assert.Equal(-2, (parsed["Byte Tag"] as NbtByteTag).Payload);
                Assert.Equal(11234, (parsed["Short Tag"] as NbtShortTag).Payload);
                Assert.Equal(581248567, (parsed["Int Tag"] as NbtIntTag).Payload);
                Assert.Equal(5816518613524685468, (parsed["Long Tag"] as NbtLongTag).Payload);
                Assert.Equal(3.14159F, (parsed["Float Tag"] as NbtFloatTag).Payload);
                Assert.Equal(66518181.2168181, (parsed["Double Tag"] as NbtDoubleTag).Payload);
                Assert.Equal("It's a string", (parsed["String Tag"] as NbtStringTag).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldReadArrayTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\arrays.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);

                Assert.Equal(new byte[] { 0, 2, 4, 6, 8, 10 }, (parsed["Byte Array"] as NbtByteArrayTag).Payload);
                Assert.Equal(new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 }, (parsed["Int Array"] as NbtIntArrayTag).Payload);
                Assert.Equal(new long[] { 1337, 147258369, 8675309 }, (parsed["Long Array"] as NbtLongArrayTag).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldReadListTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\lists.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);
                var stringList = (parsed["String List"] as NbtListTag).Select(t => (t as NbtStringTag).Payload);
                var intList = (parsed["Int List"] as NbtListTag).Select(t => (t as NbtIntTag).Payload);

                Assert.Equal(new string[] { "Alpha", "Beta", "Gamma", "Delta" }, stringList);
                Assert.Equal(new int[] { 19, 5, 23, 9982 }, intList);
                Assert.Empty((parsed["End List"] as NbtListTag));
            }
        }
    }
}