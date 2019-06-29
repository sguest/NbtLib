using Xunit;

namespace NbtLib.Tests
{
    public class NbtWriterTests
    {
        [Fact]
        public void CreateNbtStream_ShouldWriteSimpleFile()
        {
            var testData = new NbtCompoundTag
            {
                { "Int 5", new NbtIntTag(5) },
                { "String abcd", new NbtStringTag("abcd") }
            };

            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData, "Root Tag"))
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
            var testData = new NbtCompoundTag
            {
                { "Byte Tag", new NbtByteTag(-2) },
                { "Short Tag", new NbtShortTag(11234) },
                { "Int Tag", new NbtIntTag(581248567) },
                { "Long Tag", new NbtLongTag(5816518613524685468) },
                { "Float Tag", new NbtFloatTag(3.14159F) },
                { "Double Tag", new NbtDoubleTag(66518181.2168181) },
                { "String Tag", new NbtStringTag("It's a string") }
            };

            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData, "Root Tag"))
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
            var testData = new NbtCompoundTag
            {
                { "Byte Array", new NbtByteArrayTag(new byte[] { 0, 2, 4, 6, 8, 10 }) },
                { "Int Array", new NbtIntArrayTag(new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 }) },
                { "Long Array", new NbtLongArrayTag(new long[] { 1337, 147258369, 8675309 }) }
            };

            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData, "Root Tag"))
            {
                using (var fileStream = System.IO.File.OpenRead(@"TestData\arrays.nbt"))
                {
                    Assert.True(TestHelpers.StreamsEqual(outputStream, fileStream));
                }
            }
        }

        [Fact]
        public void CreateNbtStream_ShouldWriteListTypes()
        {
            var stringList = new NbtListTag(NbtTagType.String)
            {
                new NbtStringTag("Alpha"),
                new NbtStringTag("Beta"),
                new NbtStringTag("Gamma"),
                new NbtStringTag("Delta")
            };

            var intList = new NbtListTag(NbtTagType.Int)
            {
                new NbtIntTag(19),
                new NbtIntTag(5),
                new NbtIntTag(23),
                new NbtIntTag(9982)
            };

            var endList = new NbtListTag(NbtTagType.End);

            var testData = new NbtCompoundTag
            {
                { "String List", stringList },
                { "Int List", intList },
                { "End List", endList }
            };

            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData, "Root Tag"))
            {
                using (var fileStream = System.IO.File.OpenRead(@"TestData\lists.nbt"))
                {
                    Assert.True(TestHelpers.StreamsEqual(outputStream, fileStream));
                }
            }
        }

        [Fact]
        public void CreateNbtStream_ShouldWriteNestedObjects()
        {
            var testData = new NbtCompoundTag();

            var compoundChild = new NbtCompoundTag
            {
                { "String Tag", new NbtStringTag("String Content") },
                { "Int Tag", new NbtIntTag(12345) }
            };

            testData.Add("Compound Child", compoundChild);

            var listChild = new NbtListTag(NbtTagType.Compound);

            testData.Add("List Child", listChild);

            var listItemOne = new NbtCompoundTag
            {
                { "Float", new NbtFloatTag(123) }
            };
            listChild.Add(listItemOne);

            var listItemTwo = new NbtCompoundTag
            {
                { "Float", new NbtFloatTag(456) }
            };
            listChild.Add(listItemTwo);

            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData, "Root Tag"))
            {
                using (var fileStream = System.IO.File.OpenRead(@"TestData\nested.nbt"))
                {
                    Assert.True(TestHelpers.StreamsEqual(outputStream, fileStream));
                }
            }
        }
    }
}
