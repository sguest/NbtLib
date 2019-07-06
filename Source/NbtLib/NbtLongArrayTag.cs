using System;
using System.Linq;

namespace NbtLib
{
    public struct NbtLongArrayTag : INbtTag<long[]>, IEquatable<NbtLongArrayTag>
    {
        public NbtLongArrayTag(long[] payload)
        {
            Payload = payload;
        }

        public NbtTagType TagType => NbtTagType.LongArray;
        public long[] Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtLongArrayTag longArrayTag)
            {
                return Equals(longArrayTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtLongArrayTag other)
        {
            return Payload.SequenceEqual(other.Payload);
        }

        public override int GetHashCode() => Payload.GetHashCode();

        public override string ToString() => "[" + string.Join(", ", Payload) + "]";

        public string ToJsonString() => ToString();
    }
}
