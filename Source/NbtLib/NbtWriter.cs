using System;
using System.IO;
using System.IO.Compression;

namespace NbtLib
{
    /// <summary>
    /// Turns NBT tag collections into data streams
    /// </summary>
    public class NbtWriter
    {
        /// <summary>
        /// Creates a GZipped stream of NBT data from a collection of tags. The root tag will have an empty name.
        /// </summary>
        /// <param name="rootTag">NBT compound tag to use as the root of the NBT datae</param>
        /// <returns>GZipped NBT stream</returns>
        public Stream CreateNbtStream(NbtCompoundTag rootTag)
        {
            return CreateNbtStream(rootTag, string.Empty);
        }

        /// <summary>
        /// Creates a GZipped stream of NBT data from a collection of tags, with a specified name for the root tag.
        /// </summary>
        /// <param name="rootTag">NBT compound tag to use as the root of the NBT data</param>
        /// <param name="rootTagName">Name of the root tag</param>
        /// <returns>GZipped NBT stream</returns>
        public Stream CreateNbtStream(NbtCompoundTag rootTag, string rootTagName)
        {
            using (var inputStream = CreateUncompressedNbtStream(rootTag, rootTagName))
            {
                var outputStream = new MemoryStream();

                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress, true))
                {
                    inputStream.CopyTo(gZipStream);
                }

                outputStream.Seek(0, SeekOrigin.Begin);
                return outputStream;
            }
        }

        /// <summary>
        /// Creates an uncompressed (nonstandard) stream of NBT data from a collection of tags. The root tag will have an empty name.
        /// </summary>
        /// <param name="rootTag">NBT compound tag to use as the root of the NBT datae</param>
        /// <returns>Uncompressed NBT stream</returns>
        public Stream CreateUncompressedNbtStream(NbtCompoundTag rootTag)
        {
            return CreateUncompressedNbtStream(rootTag, string.Empty);
        }

        /// <summary>
        /// Creates an uncompressed (nonstandard) stream of NBT data from a collection of tags, with a specified name for the root tag.
        /// </summary>
        /// <param name="rootTag">NBT compound tag to use as the root of the NBT data</param>
        /// <param name="rootTagName">Name of the root tag</param>
        /// <returns>Uncompressed NBT stream</returns>
        public Stream CreateUncompressedNbtStream(NbtCompoundTag rootTag, string rootTagName)
        {
            var stream = new MemoryStream();
            WriteNamedTag(stream, rootTag, rootTagName);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private void WriteString(Stream stream, string value)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            if(bytes.Length > short.MaxValue)
            {
                throw new InvalidDataException($"Could not write string {value} to stream as it is longer than {short.MaxValue} bytes");
            }
            WriteShort(stream, (short)bytes.Length);
            stream.Write(bytes, 0, bytes.Length);
        }

        private void WriteShort(Stream stream, short value)
        {
            var bytes = BitConverter.GetBytes(value);
            if(BitConverter.IsLittleEndian) {
                Array.Reverse(bytes);
            }
            stream.Write(bytes, 0, 2);
        }

        private void WriteInt(Stream stream, int value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            stream.Write(bytes, 0, 4);
        }

        private void WriteLong(Stream stream, long value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            stream.Write(bytes, 0, 8);
        }

        private void WriteFloat(Stream stream, float value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            stream.Write(bytes, 0, 4);
        }

        private void WriteDouble(Stream stream, double value)
        {
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            stream.Write(bytes, 0, 8);
        }

        private void WriteNamedTag(Stream stream, INbtTag tag, string name)
        {
            stream.WriteByte((byte)tag.TagType);

            if(tag.TagType != NbtTagType.End)
            {
                WriteString(stream, name);
            }

            WriteTagPayload(stream, tag);
        }

        private void WriteTagPayload(Stream stream, INbtTag tag)
        {
            if (tag is NbtByteTag byteTag)
            {
                stream.WriteByte((byte)byteTag.Payload);
            }
            else if (tag is NbtShortTag shortTag)
            {
                WriteShort(stream, shortTag.Payload);
            }
            else if (tag is NbtIntTag intTag)
            {
                WriteInt(stream, intTag.Payload);
            }
            else if (tag is NbtLongTag longTag)
            {
                WriteLong(stream, longTag.Payload);
            }
            else if (tag is NbtFloatTag floatTag)
            {
                WriteFloat(stream, floatTag.Payload);
            }
            else if (tag is NbtDoubleTag doubleTag)
            {
                WriteDouble(stream, doubleTag.Payload);
            }
            else if(tag is NbtStringTag stringTag)
            {
                WriteString(stream, stringTag.Payload);
            }
            else if(tag is NbtByteArrayTag byteArrayTag)
            {
                WriteInt(stream, byteArrayTag.Payload.Length);
                stream.Write(byteArrayTag.Payload, 0, byteArrayTag.Payload.Length);
            }
            else if(tag is NbtIntArrayTag intArrayTag)
            {
                WriteInt(stream, intArrayTag.Payload.Length);
                foreach (var item in intArrayTag.Payload)
                {
                    WriteInt(stream, item);
                }
            }
            else if(tag is NbtLongArrayTag longArrayTag)
            {
                WriteInt(stream, longArrayTag.Payload.Length);
                foreach (var item in longArrayTag.Payload)
                {
                    WriteLong(stream, item);
                }
            }
            else if(tag is NbtListTag listTag)
            {
                WriteListTag(stream, listTag);
            }
            else if(tag is NbtCompoundTag compoundTag)
            {
                WriteCompoundTag(stream, compoundTag);
            }
            else
            {
                throw new InvalidDataException($"Unrecognized tag type {tag.GetType()}");
            }
        }

        private void WriteListTag(Stream stream, NbtListTag tag)
        {
            stream.WriteByte((byte)tag.ItemType);
            WriteInt(stream, tag.Count);
            foreach (var childTag in tag)
            {
                WriteTagPayload(stream, childTag);
            }
        }

        private void WriteCompoundTag(Stream stream, NbtCompoundTag tag)
        {
            foreach (var childTag in tag)
            {
                WriteNamedTag(stream, childTag.Value, childTag.Key);
            }

            stream.WriteByte((byte)NbtTagType.End);
        }
    }
}
