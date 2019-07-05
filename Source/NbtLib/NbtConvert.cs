using System.IO;

namespace NbtLib
{
    public static class NbtConvert
    {
        public static NbtCompoundTag ParseNbtStream(Stream stream)
        {
            var parser = new NbtParser();
            return parser.ParseNbtStream(stream);
        }

        public static Stream CreateNbtStream(NbtCompoundTag rootTag)
        {
            var writer = new NbtWriter();
            return writer.CreateNbtStream(rootTag);
        }

        public static Stream CreateNbtStream(NbtCompoundTag rootTag, string rootTagName)
        {
            var writer = new NbtWriter();
            return writer.CreateNbtStream(rootTag, rootTagName);
        }

        public static Stream CreateUncompressedNbtStream(NbtCompoundTag rootTag)
        {
            var writer = new NbtWriter();
            return writer.CreateUncompressedNbtStream(rootTag);
        }

        public static Stream CreateUncompressedNbtStream(NbtCompoundTag rootTag, string rootTagName)
        {
            var writer = new NbtWriter();
            return writer.CreateUncompressedNbtStream(rootTag, rootTagName);
        }

        public static T DeserializeObject<T>(Stream stream)
        {
            var deserializer = new NbtDeserializer();
            return deserializer.DeserializeObject<T>(stream);
        }

        public static T DeserializeObject<T>(NbtCompoundTag compoundTag)
        {
            var deserializer = new NbtDeserializer();
            return deserializer.DeserializeObject<T>(compoundTag);
        }

        public static NbtCompoundTag SerializeObjectToTag(object obj)
        {
            var serializer = new NbtSerializer();
            return serializer.SerializeObjectToTag(obj);
        }

        public static NbtCompoundTag SerializeObjectToTag(object obj, NbtSerializerSettings settings)
        {
            var serializer = new NbtSerializer(settings);
            return serializer.SerializeObjectToTag(obj);
        }

        public static Stream SerializeObject(object obj)
        {
            var serializer = new NbtSerializer();
            return serializer.SerializeObject(obj);
        }

        public static Stream SerializeObject(object obj, NbtSerializerSettings settings)
        {
            var serializer = new NbtSerializer(settings);
            return serializer.SerializeObject(obj);
        }
    }
}
