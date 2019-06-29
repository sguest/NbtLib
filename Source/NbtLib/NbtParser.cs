using System;
using System.IO;
using System.IO.Compression;

namespace NbtLib
{
    public class NbtParser
    {
        public NbtCompoundTag ParseNbtStream(Stream stream)
        {
            using (var decompressionStream = new GZipStream(stream, CompressionMode.Decompress))
            {
                return ParseUncompressedNbtStream(decompressionStream);
            }
        }

        public NbtCompoundTag ParseUncompressedNbtStream(Stream stream)
        {
            var rootTag = (NbtCompoundTag)ParseNamedTag(stream);
            return rootTag;
        }

        private byte[] ReadBytes(Stream stream, int length)
        {
            var bytes = new byte[length];
            stream.Read(bytes, 0, length);
            return bytes;
        }

        private short ReadShort(Stream stream)
        {
            var bytes = ReadBytes(stream, 2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToInt16(bytes, 0);
        }

        private int ReadInt(Stream stream)
        {
            var bytes = ReadBytes(stream, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt32(bytes, 0 );
        }

        private long ReadLong(Stream stream)
        {
            var bytes = ReadBytes(stream, 8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToInt64(bytes, 0);
        }

        private float ReadFloat(Stream stream)
        {
            var bytes = ReadBytes(stream, 4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToSingle(bytes, 0);
        }

        private double ReadDouble(Stream stream)
        {
            var bytes = ReadBytes(stream, 8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToDouble(bytes, 0);
        }

        private string ReadString(Stream stream)
        {
            var nameLength = ReadShort(stream);
            return System.Text.Encoding.UTF8.GetString(ReadBytes(stream, nameLength));
        }

        private NbtTag ParseNamedTag(Stream stream)
        {
            var tagType = (NbtTagType)stream.ReadByte();

            if (tagType == NbtTagType.End)
            {
                return new NbtEndTag();
            }

            var name = ReadString(stream);
            var tag = ParseTagPayload(stream, tagType);
            tag.Name = name;
            return tag;
        }

        private NbtTag ParseTagPayload(Stream stream, NbtTagType tagType)
        {
            switch (tagType) {
                case NbtTagType.End:
                    return new NbtEndTag();
                case NbtTagType.Compound:
                    return ParseCompoundTag(stream);
                case NbtTagType.Byte:
                    return new NbtByteTag() { Payload = (sbyte)stream.ReadByte() };
                case NbtTagType.Short:
                    return new NbtShortTag() { Payload = ReadShort(stream) };
                case NbtTagType.Int:
                    return new NbtIntTag() { Payload = ReadInt(stream) };
                case NbtTagType.Long:
                    return new NbtLongTag() { Payload = ReadLong(stream) };
                case NbtTagType.Float:
                    return new NbtFloatTag() { Payload = ReadFloat(stream) };
                case NbtTagType.Double:
                    return new NbtDoubleTag() { Payload = ReadDouble(stream) };
                case NbtTagType.String:
                    return new NbtStringTag() { Payload = ReadString(stream) };
                case NbtTagType.ByteArray:
                    var length = ReadInt(stream);
                    return new NbtByteArrayTag() { Payload = ReadBytes(stream, length) };
                case NbtTagType.IntArray:
                    return ParseIntArray(stream);
                case NbtTagType.LongArray:
                    return ParseLongArray(stream);
                case NbtTagType.List:
                    return ParseListTag(stream);
            }

            throw new InvalidDataException($"Unrecognized tag type {tagType}");
        }

        private NbtCompoundTag ParseCompoundTag(Stream stream)
        {
            var tag = new NbtCompoundTag();

            NbtTag childTag;
            while(true)
            {
                childTag = ParseNamedTag(stream);
                if(childTag is NbtEndTag)
                {
                    return tag;
                }
                tag.ChildTags.Add(childTag.Name, childTag);
            }
        }

        private NbtIntArrayTag ParseIntArray(Stream stream)
        {
            var length = ReadInt(stream);
            var payload = new int[length];

            for(var i = 0; i < length; i++)
            {
                payload[i] = ReadInt(stream);
            }
            return new NbtIntArrayTag() { Payload = payload };
        }

        private NbtLongArrayTag ParseLongArray(Stream stream)
        {
            var length = ReadInt(stream);
            var payload = new long[length];

            for (var i = 0; i < length; i++)
            {
                payload[i] = ReadLong(stream);
            }
            return new NbtLongArrayTag() { Payload = payload };
        }

        private NbtListTag ParseListTag(Stream stream)
        {
            var tag = new NbtListTag
            {
                ItemType = (NbtTagType)stream.ReadByte()
            };
            int length = ReadInt(stream);

            for(var i = 0; i < length; i++)
            {
                tag.Add(ParseTagPayload(stream, tag.ItemType));
            }

            return tag;
        }
    }
}
