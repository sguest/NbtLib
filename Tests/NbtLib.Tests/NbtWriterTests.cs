using Xunit;

namespace NbtLib.Tests
{
    public class NbtWriterTests
    {
        [Fact]
        public void CreateNbtStream_ShouldWriteSimpleFile()
        {
            var testData = new NbtCompoundTag()
            {
                Name = "Root Tag",
            };

            testData.ChildTags.Add("List", new NbtIntTag { Name = "Int 5", Payload = 5 });
            testData.ChildTags.Add("String abcd", new NbtStringTag { Name = "String abcd", Payload = "abcd" });

            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData))
            {
                using (var fileStream = System.IO.File.OpenRead(@"TestData\simple.nbt"))
                {
                    Assert.True(TestHelpers.StreamsEqual(outputStream, fileStream));
                }
            }
        }

        [Fact]
        public void CreateNbtStream_ShouldWriteArrayTypes()
        {
            var testData = new NbtCompoundTag()
            {
                Name = "Root Tag",
            };

            testData.ChildTags.Add("Byte Array", new NbtByteArrayTag { Name = "Byte Array", Payload = new byte[] { 0, 2, 4, 6, 8, 10 } });
            testData.ChildTags.Add("Int Array", new NbtIntArrayTag { Name = "Int Array", Payload = new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 } });
            testData.ChildTags.Add("Long Array", new NbtLongArrayTag { Name = "Long Array", Payload = new long[] { 1337, 147258369, 8675309 } });
            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData))
            {
                using (var fileStream = System.IO.File.OpenRead(@"TestData\arrays.nbt"))
                {
                    Assert.True(TestHelpers.StreamsEqual(outputStream, fileStream));
                }
            }
        }
    }
}
