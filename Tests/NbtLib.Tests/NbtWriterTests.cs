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
        public void CreateNbtStream_ShouldWritePrimitiveTypes()
        {
            var testData = new NbtCompoundTag()
            {
                Name = "Root Tag",
            };

            testData.ChildTags.Add("Byte Tag", new NbtByteTag { Name = "Byte Tag", Payload = -2 });
            testData.ChildTags.Add("Short Tag", new NbtShortTag { Name = "Short Tag", Payload = 11234 });
            testData.ChildTags.Add("Int Tag", new NbtIntTag { Name = "Int Tag", Payload = 581248567 });
            testData.ChildTags.Add("Long Tag", new NbtLongTag { Name = "Long Tag", Payload = 5816518613524685468 });
            testData.ChildTags.Add("Float Tag", new NbtFloatTag { Name = "Float Tag", Payload = 3.14159F });
            testData.ChildTags.Add("Double Tag", new NbtDoubleTag { Name = "Double Tag", Payload = 66518181.2168181 });
            testData.ChildTags.Add("String Tag", new NbtStringTag { Name = "String Tag", Payload = "It's a string" });
            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData))
            {
                using (var fileStream = System.IO.File.OpenRead(@"TestData\primitives.nbt"))
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
