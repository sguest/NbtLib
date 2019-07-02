using NbtLib.Tests.Serialization;
using System.Collections.Generic;
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
    }
}
