using NbtLib.Tests.Serialization;
using System.Collections.Generic;
using Xunit;

namespace NbtLib.Tests
{
    public class NbtDeserializerTests
    {
        [Fact]
        public void DeserializeObject_ShouldParseSimpleObject()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\simple.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<SimpleObject>(fileStream);

                Assert.Equal(5, obj.Int5);
                Assert.Equal("abcd", obj.StringAbcd);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldParseToDictionary()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\simple.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<IDictionary<string, object>>(fileStream);

                Assert.Equal(5, obj["Int 5"]);
                Assert.Equal("abcd", obj["String abcd"]);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldParseToIDictionary()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\simple.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<Dictionary<string, object>>(fileStream);

                Assert.Equal(5, obj["Int 5"]);
                Assert.Equal("abcd", obj["String abcd"]);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldReadPrimitiveTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\primitives.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<PrimitivesObject>(fileStream);

                Assert.Equal(-2, obj.ByteTag);
                Assert.Equal(11234, obj.ShortTag);
                Assert.Equal(581248567, obj.IntTag);
                Assert.Equal(5816518613524685468, obj.LongTag);
                Assert.Equal(3.14159F, obj.FloatTag);
                Assert.Equal(66518181.2168181, obj.DoubleTag);
                Assert.Equal("It's a string", obj.StringTag);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldReadArrayTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\arrays.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<ArraysObject>(fileStream);

                Assert.Equal(new byte[] { 0, 2, 4, 6, 8, 10 }, obj.ByteArray);
                Assert.Equal(new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 }, obj.IntArray);
                Assert.Equal(new long[] { 1337, 147258369, 8675309 }, obj.LongArray);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldReadArraysToCollectionTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\arrays.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<ArrayCollectionsObject>(fileStream);

                Assert.Equal(new byte[] { 0, 2, 4, 6, 8, 10 }, obj.ByteArray);
                Assert.Equal(new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 }, obj.IntArray);
                Assert.Equal(new long[] { 1337, 147258369, 8675309 }, obj.LongArray);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldReadListTypes()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\lists.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<ListsObject>(fileStream);

                Assert.Equal(new string[] { "Alpha", "Beta", "Gamma", "Delta" }, obj.StringList);
                Assert.Equal(new int[] { 19, 5, 23, 9982 }, obj.IntList);
                Assert.Empty(obj.EndList);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldReadListTypesToInterfaces()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\lists.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<ListInterfacesObject>(fileStream);

                Assert.Equal(new string[] { "Alpha", "Beta", "Gamma", "Delta" }, obj.StringList);
                Assert.Equal(new int[] { 19, 5, 23, 9982 }, obj.IntList);
                Assert.Empty(obj.EndList);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldReadListTypesToArrays()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\lists.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<ListsAsArraysObject>(fileStream);

                Assert.Equal(new string[] { "Alpha", "Beta", "Gamma", "Delta" }, obj.StringList);
                Assert.Equal(new int[] { 19, 5, 23, 9982 }, obj.IntList);
                Assert.Empty(obj.EndList);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldReadNestedObjects()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\nested.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<NestedObject>(fileStream);

                Assert.Equal("String Content", obj.CompoundChild.StringTag);
                Assert.Equal(12345, obj.CompoundChild.IntTag);
                Assert.Equal(123, obj.ListChild[0].Float);
                Assert.Equal(456, obj.ListChild[1].Float);
            }
        }

        [Fact]
        public void DeserializeObject_ShouldReadNestedDictionary()
        {
            using (var fileStream = System.IO.File.OpenRead(@"TestData\nested.nbt"))
            {
                var deserializer = new NbtDeserializer();
                var obj = deserializer.DeserializeObject<NestedDictionaryObject>(fileStream);

                Assert.Equal(123, obj.ListChild[0]["Float"]);
                Assert.Equal(456, obj.ListChild[1]["Float"]);
            }
        }
    }
}
