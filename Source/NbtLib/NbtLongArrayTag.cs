using System;
using System.Linq;

namespace NbtLib
{
    public class NbtLongArrayTag : NbtTag, IEquatable<NbtLongArrayTag>
    {
        public NbtLongArrayTag(long[] payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.LongArray;
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
    }
}
