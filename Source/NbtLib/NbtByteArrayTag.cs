using System;
using System.Linq;

namespace NbtLib
{
    public struct NbtByteArrayTag : INbtTag<byte[]>, IEquatable<NbtByteArrayTag>
    {
        public NbtByteArrayTag(byte[] payload)
        {
            Payload = payload;
        }

        public NbtTagType TagType => NbtTagType.ByteArray;
        public byte[] Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtByteArrayTag byteArrayTag)
            {
                return Equals(byteArrayTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtByteArrayTag other)
        {
            return Payload.SequenceEqual(other.Payload);
        }

        public override int GetHashCode() => Payload.GetHashCode();

        public override string ToString() => "[" + string.Join(", ", Payload) + "]";

        public string ToJsonString() => ToString();
    }
}
