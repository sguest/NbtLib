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
                Assert.Equal(5, (parsed.ChildTags["Int 5"] as NbtIntTag).Payload);
                Assert.Equal("abcd", (parsed.ChildTags["String abcd"] as NbtStringTag).Payload);
            }
        }

        [Fact]
        public void ParseNbtStream_ShouldReadArrayTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\arrays.nbt"))
            {
                var parser = new NbtParser();
                var parsed = parser.ParseNbtStream(fileStream);

                Assert.Equal(new byte[] { 0, 2, 4, 6, 8, 10 }, (parsed.ChildTags["Byte Array"] as NbtByteArrayTag).Payload);
                Assert.Equal(new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 }, (parsed.ChildTags["Int Array"] as NbtIntArrayTag).Payload);
                Assert.Equal(new long[] { 1337, 147258369, 8675309 }, (parsed.ChildTags["Long Array"] as NbtLongArrayTag).Payload);
            }
        }
    }
}
