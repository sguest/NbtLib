using System.Linq;
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

                Assert.Equal(5, ((NbtIntTag)parsed["Int 5"]).Payload);
                Assert.Equal("abcd", ((NbtStringTag)parsed["String abcd"]).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldReadPrimitiveTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\primitives.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);

                Assert.Equal(-2, ((NbtByteTag)parsed["Byte Tag"]).Payload);
                Assert.Equal(11234, ((NbtShortTag)parsed["Short Tag"]).Payload);
                Assert.Equal(581248567, ((NbtIntTag)parsed["Int Tag"]).Payload);
                Assert.Equal(5816518613524685468, ((NbtLongTag)parsed["Long Tag"]).Payload);
                Assert.Equal(3.14159F, ((NbtFloatTag)parsed["Float Tag"]).Payload);
                Assert.Equal(66518181.2168181, ((NbtDoubleTag)parsed["Double Tag"]).Payload);
                Assert.Equal("It's a string", ((NbtStringTag)parsed["String Tag"]).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldReadArrayTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\arrays.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);

                Assert.Equal(new byte[] { 0, 2, 4, 6, 8, 10 }, ((NbtByteArrayTag)parsed["Byte Array"]).Payload);
                Assert.Equal(new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 }, ((NbtIntArrayTag)parsed["Int Array"]).Payload);
                Assert.Equal(new long[] { 1337, 147258369, 8675309 }, ((NbtLongArrayTag)parsed["Long Array"]).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldReadListTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\lists.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);
                var stringList = (parsed["String List"] as NbtListTag).Select(t => ((NbtStringTag)t).Payload);
                var intList = (parsed["Int List"] as NbtListTag).Select(t => ((NbtIntTag)t).Payload);

                Assert.Equal(new string[] { "Alpha", "Beta", "Gamma", "Delta" }, stringList);
                Assert.Equal(new int[] { 19, 5, 23, 9982 }, intList);
                Assert.Empty((parsed["End List"] as NbtListTag));
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldReadNestedObjects()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\nested.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);

                var compoundChild = parsed["Compound Child"] as NbtCompoundTag;
                var listChild = parsed["List Child"] as NbtListTag;

                var itemOne = listChild[0] as NbtCompoundTag;
                var itemTwo = listChild[1] as NbtCompoundTag;

                Assert.Equal("String Content", ((NbtStringTag)compoundChild["String Tag"]).Payload);
                Assert.Equal(12345, ((NbtIntTag)compoundChild["Int Tag"]).Payload);

                Assert.Equal(123, ((NbtFloatTag)itemOne["Float"]).Payload);
                Assert.Equal(456, ((NbtFloatTag)itemTwo["Float"]).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldHandleUncompressedFile()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\uncompressed.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);

                Assert.Equal(5, ((NbtIntTag)parsed["Int 5"]).Payload);
                Assert.Equal("abcd", ((NbtStringTag)parsed["String abcd"]).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_WhenInvalidHeaderBytes_ShouldThrow()
        {
            using (var memoryStream = new System.IO.MemoryStream(new byte[] { 4, 5 }))
            {
                var parser = new NbtParser();
                Assert.Throws<System.IO.InvalidDataException>(() => parser.ParseNbtStream(memoryStream));
            }
        }
    }
}
