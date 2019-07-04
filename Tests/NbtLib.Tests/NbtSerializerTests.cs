﻿using NbtLib.Tests.Serialization;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NbtLib.Tests
{
    public class NbtSerializerTests
    {
        [Fact]
        public void SerializeObjectToTag_ShouldSerializeSimpleObject()
        {
            var obj = new SimpleObject
            {
                Int5 = 5,
                StringAbcd = "abcd"
            };

            var serializer = new NbtSerializer();
            var tag = serializer.SerializeObjectToTag(obj);

            Assert.Equal(5, ((NbtIntTag)tag["Int5"]).Payload);
            Assert.Equal("abcd", ((NbtStringTag)tag["StringAbcd"]).Payload);
        }

        [Fact]
        public void SerializeObjectToTag_ShouldSerializeDictionary()
        {
            IDictionary<string, object> obj = new Dictionary<string, object>
            {
                { "Int 5", 5 },
                { "String Abcd", "abcd" }
            };

            var serializer = new NbtSerializer();
            var tag = serializer.SerializeObjectToTag(obj);

            Assert.Equal(5, ((NbtIntTag)tag["Int 5"]).Payload);
            Assert.Equal("abcd", ((NbtStringTag)tag["String Abcd"]).Payload);
        }

        [Fact]
        public void SerializeObjectToTag_ShouldSerializePrimitiveTypes()
        {
            var obj = new PrimitivesObject
            {
                ByteTag = -2,
                ShortTag = 11234,
                IntTag = 581248567,
                LongTag = 5816518613524685468,
                FloatTag = 3.14159F,
                DoubleTag = 66518181.2168181,
                StringTag = "It's a string"
            };

            var serializer = new NbtSerializer();
            var tag = serializer.SerializeObjectToTag(obj);

            Assert.Equal(-2, ((NbtByteTag)tag["ByteTag"]).Payload);
            Assert.Equal(11234, ((NbtShortTag)tag["ShortTag"]).Payload);
            Assert.Equal(581248567, ((NbtIntTag)tag["IntTag"]).Payload);
            Assert.Equal(5816518613524685468, ((NbtLongTag)tag["LongTag"]).Payload);
            Assert.Equal(3.14159F, ((NbtFloatTag)tag["FloatTag"]).Payload);
            Assert.Equal(66518181.2168181, ((NbtDoubleTag)tag["DoubleTag"]).Payload);
            Assert.Equal("It's a string", ((NbtStringTag)tag["StringTag"]).Payload);
        }

        [Fact]
        public void SerializeObjectToTag_ShouldSerializeArrayTypes()
        {
            var obj = new ArraysObject
            {
                ByteArray = new byte[] { 0, 2, 4, 6, 8, 10 },
                IntArray = new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 },
                LongArray = new long[] { 1337, 147258369, 8675309 }
            };

            var serializer = new NbtSerializer();
            var tag = serializer.SerializeObjectToTag(obj);

            Assert.Equal(obj.ByteArray, ((NbtByteArrayTag)tag["ByteArray"]).Payload);
            Assert.Equal(obj.IntArray, ((NbtIntArrayTag)tag["IntArray"]).Payload);
            Assert.Equal(obj.LongArray, ((NbtLongArrayTag)tag["LongArray"]).Payload);
        }


        [Fact]
        public void SerializeObjectToTag_ShouldSerializeCollectionTypesToArrays()
        {
            var obj = new ArrayCollectionsObject
            {
                ByteArray = new List<byte>(new byte[] { 0, 2, 4, 6, 8, 10 }),
                IntArray = new List<int>(new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 }).AsReadOnly(),
                LongArray = new List<long>(new long[] { 1337, 147258369, 8675309 })
            };

            var serializer = new NbtSerializer();
            var tag = serializer.SerializeObjectToTag(obj);

            Assert.Equal(obj.ByteArray, ((NbtByteArrayTag)tag["ByteArray"]).Payload);
            Assert.Equal(obj.IntArray, ((NbtIntArrayTag)tag["IntArray"]).Payload);
            Assert.Equal(obj.LongArray, ((NbtLongArrayTag)tag["LongArray"]).Payload);
        }

        [Fact]
        public void SerializeObjectToTag_ShouldSerializeListTypes()
        {
            var obj = new ListsObject
            {
                StringList = new List<string>(new string[] { "Alpha", "Beta", "Gamma", "Delta" }),
                EndList = new List<object>()
            };

            var serializer = new NbtSerializer();
            var tag = serializer.SerializeObjectToTag(obj);

            var stringList = (NbtListTag)tag["StringList"];
            var emptyList = (NbtListTag)tag["EndList"];

            Assert.Null(tag["IntList"]);
            Assert.Equal(NbtTagType.String, stringList.ItemType);
            Assert.Equal(obj.StringList, stringList.Payload.Select(t => ((NbtStringTag)t).Payload));
            Assert.Equal(NbtTagType.Compound, emptyList.ItemType);
            Assert.Empty(emptyList.Payload);
        }


        [Fact]
        public void SerializeObjectToTag_ShouldSerializeNestedObjects()
        {
            var obj = new NestedObject
            {
                CompoundChild = new CompoundChild
                {
                    StringTag = "String Content",
                    IntTag = 12345
                },
                ListChild = new List<ListChild>()
                {
                    new ListChild { Float = 123 },
                    new ListChild { Float = 456 }
                }
            };

            var serializer = new NbtSerializer();
            var tag = serializer.SerializeObjectToTag(obj);

            var compoundChild = tag["CompoundChild"] as NbtCompoundTag;
            var listChild = tag["ListChild"] as NbtListTag;

            var listItems = listChild.Payload.Select(i => (NbtCompoundTag)i).ToList();

            Assert.Equal(obj.CompoundChild.StringTag, ((NbtStringTag)compoundChild["StringTag"]).Payload);
            Assert.Equal(obj.CompoundChild.IntTag, ((NbtIntTag)compoundChild["IntTag"]).Payload);
            Assert.Equal(obj.ListChild[0].Float, ((NbtFloatTag)listItems[0]["Float"]).Payload);
            Assert.Equal(obj.ListChild[1].Float, ((NbtFloatTag)listItems[1]["Float"]).Payload);
        }
    }
}
