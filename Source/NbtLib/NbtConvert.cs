using System.IO;

namespace NbtLib
{
    /// <summary>
    /// Static helper class for conveniently working with NBT data
    /// </summary>
    public static class NbtConvert
    {
        /// <summary>
        /// Parse a NBT stream to a collection of tag objects
        /// </summary>
        /// <param name="stream">Stream of NBT data, can be GZipped or not</param>
        /// <returns>NbtCompound tag, possibly with child tags</returns>
        public static NbtCompoundTag ParseNbtStream(Stream stream)
        {
            var parser = new NbtParser();
            return parser.ParseNbtStream(stream);
        }

        /// <summary>
        /// Creates a GZipped stream of NBT data from a collection of tags. The root tag will have an empty name.
        /// </summary>
        /// <param name="rootTag">NBT compound tag to use as the root of the NBT datae</param>
        /// <returns>GZipped NBT stream</returns>
        public static Stream CreateNbtStream(NbtCompoundTag rootTag)
        {
            var writer = new NbtWriter();
            return writer.CreateNbtStream(rootTag);
        }

        /// <summary>
        /// Creates a GZipped stream of NBT data from a collection of tags, with a specified name for the root tag.
        /// </summary>
        /// <param name="rootTag">NBT compound tag to use as the root of the NBT data</param>
        /// <param name="rootTagName">Name of the root tag</param>
        /// <returns>GZipped NBT stream</returns>
        public static Stream CreateNbtStream(NbtCompoundTag rootTag, string rootTagName)
        {
            var writer = new NbtWriter();
            return writer.CreateNbtStream(rootTag, rootTagName);
        }

        /// <summary>
        /// Creates an uncompressed (nonstandard) stream of NBT data from a collection of tags. The root tag will have an empty name.
        /// </summary>
        /// <param name="rootTag">NBT compound tag to use as the root of the NBT datae</param>
        /// <returns>Uncompressed NBT stream</returns>
        public static Stream CreateUncompressedNbtStream(NbtCompoundTag rootTag)
        {
            var writer = new NbtWriter();
            return writer.CreateUncompressedNbtStream(rootTag);
        }

        /// <summary>
        /// Creates an uncompressed (nonstandard) stream of NBT data from a collection of tags, with a specified name for the root tag.
        /// </summary>
        /// <param name="rootTag">NBT compound tag to use as the root of the NBT data</param>
        /// <param name="rootTagName">Name of the root tag</param>
        /// <returns>Uncompressed NBT stream</returns>
        public static Stream CreateUncompressedNbtStream(NbtCompoundTag rootTag, string rootTagName)
        {
            var writer = new NbtWriter();
            return writer.CreateUncompressedNbtStream(rootTag, rootTagName);
        }

        /// <summary>
        /// Deserialize a stream of NBT data into an object based on the object's property names. Stream can be GZipped or not.
        /// </summary>
        /// <typeparam name="T">Type of object for deserialization target</typeparam>
        /// <param name="stream">Stream of NBT data, can be GZipped or not</param>
        /// <returns>Object representation of NBT data</returns>
        public static T DeserializeObject<T>(Stream stream)
        {
            var deserializer = new NbtDeserializer();
            return deserializer.DeserializeObject<T>(stream);
        }

        /// <summary>
        /// Deserialize a collection of NBT tags into an object based on the object's property names.
        /// </summary>
        /// <typeparam name="T">Type of object for deserialization target</typeparam>
        /// <param name="compoundTag">Root tag containing NBT data</param>
        /// <returns>Object representation of NBT tags</returns>
        public static T DeserializeObject<T>(NbtCompoundTag compoundTag)
        {
            var deserializer = new NbtDeserializer();
            return deserializer.DeserializeObject<T>(compoundTag);
        }

        /// <summary>
        /// Serialize an object to a representative set of NBT tags
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Nbt tag collection</returns>
        public static NbtCompoundTag SerializeObjectToTag(object obj)
        {
            var serializer = new NbtSerializer();
            return serializer.SerializeObjectToTag(obj);
        }

        /// <summary>
        /// Serialize an object to a representative set of NBT tags
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="settings">Serialization settings</param>
        /// <returns>Nbt tag collection</returns>
        public static NbtCompoundTag SerializeObjectToTag(object obj, NbtSerializerSettings settings)
        {
            var serializer = new NbtSerializer(settings);
            return serializer.SerializeObjectToTag(obj);
        }

        /// <summary>
        /// Serialize an object to a representative GZipped NBT stream
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>GZipped NBT stream</returns>
        public static Stream SerializeObject(object obj)
        {
            var serializer = new NbtSerializer();
            return serializer.SerializeObject(obj);
        }

        /// <summary>
        /// Serialize an object to a representative GZipped NBT stream
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="settings">Serialization settings</param>
        /// <returns>GZipped NBT stream</returns>
        public static Stream SerializeObject(object obj, NbtSerializerSettings settings)
        {
            var serializer = new NbtSerializer(settings);
            return serializer.SerializeObject(obj);
        }

        /// <summary>
        /// Serialize an object to a representative uncompressed (nonstandard) NBT stream
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <returns>Uncompressed NBT stream</returns>
        public static Stream SerializeObjectUncompressed(object obj)
        {
            var serializer = new NbtSerializer();
            return serializer.SerializeObjectUncompressed(obj);
        }

        /// <summary>
        /// Serialize an object to a representative uncompressed (nonstandard) NBT stream
        /// </summary>
        /// <param name="obj">Object to serialize</param>
        /// <param name="settings">Serialization settings</param>
        /// <returns>Uncompressed NBT stream</returns>
        public static Stream SerializeObjectUncompressed(object obj, NbtSerializerSettings settings)
        {
            var serializer = new NbtSerializer(settings);
            return serializer.SerializeObjectUncompressed(obj);
        }
    }
}
