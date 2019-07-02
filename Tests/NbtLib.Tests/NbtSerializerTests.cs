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
    }
}
