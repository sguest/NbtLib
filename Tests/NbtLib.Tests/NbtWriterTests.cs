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

            testData.Add("Int 5", new NbtIntTag(5));
            testData.Add("String abcd", new NbtStringTag("abcd"));

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

            testData.Add("Byte Tag", new NbtByteTag(-2));
            testData.Add("Short Tag", new NbtShortTag(11234));
            testData.Add("Int Tag", new NbtIntTag(581248567));
            testData.Add("Long Tag", new NbtLongTag(5816518613524685468));
            testData.Add("Float Tag", new NbtFloatTag(3.14159F));
            testData.Add("Double Tag", new NbtDoubleTag(66518181.2168181));
            testData.Add("String Tag", new NbtStringTag("It's a string"));
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

            testData.Add("Byte Array", new NbtByteArrayTag(new byte[] { 0, 2, 4, 6, 8, 10 }));
            testData.Add("Int Array", new NbtIntArrayTag(new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 }));
            testData.Add("Long Array", new NbtLongArrayTag(new long[] { 1337, 147258369, 8675309 }));
            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData))
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
            var testData = new NbtCompoundTag()
            {
                Name = "Root Tag",
            };

            var stringList = new NbtListTag(NbtTagType.String) { Name = "String List" };
            stringList.Add(new NbtStringTag("Alpha"));
            stringList.Add(new NbtStringTag("Beta"));
            stringList.Add(new NbtStringTag("Gamma"));
            stringList.Add(new NbtStringTag("Delta"));

            var intList = new NbtListTag(NbtTagType.Int) { Name = "Int List" };
            intList.Add(new NbtIntTag(19));
            intList.Add(new NbtIntTag(5));
            intList.Add(new NbtIntTag(23));
            intList.Add(new NbtIntTag(9982));

            var endList = new NbtListTag(NbtTagType.End) { Name = "End List" };

            testData.Add("String List", stringList);
            testData.Add("Int List", intList);
            testData.Add("End List", endList);
            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData))
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
            var testData = new NbtCompoundTag() { Name = "Root Tag" };

            var compoundChild = new NbtCompoundTag
            {
                { "String Tag", new NbtStringTag("String Content") },
                { "Int Tag", new NbtIntTag(12345) }
            };

            testData.Add("Compound Child", compoundChild);

            var listChild = new NbtListTag(NbtTagType.Compound);

            testData.Add("List Child", listChild);

            var listItemOne = new NbtCompoundTag();
            listItemOne.Add("Float", new NbtFloatTag(123));
            listChild.Add(listItemOne);

            var listItemTwo = new NbtCompoundTag();
            listItemTwo.Add("Float", new NbtFloatTag(456));
            listChild.Add(listItemTwo);

            var writer = new NbtWriter();

            using (var outputStream = writer.CreateNbtStream(testData))
            {
                using (var fileStream = System.IO.File.OpenRead(@"TestData\nested.nbt"))
                {
                    Assert.True(TestHelpers.StreamsEqual(outputStream, fileStream));
                }
            }
        }
    }
}
