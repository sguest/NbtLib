using System;
using System.Linq;

namespace NbtLib
{
    public class NbtIntArrayTag : NbtTag, IEquatable<NbtIntArrayTag>
    {
        public NbtIntArrayTag(int[] payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.IntArray;
        public int[] Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtIntArrayTag intArrayTag)
            {
                return Equals(intArrayTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtIntArrayTag other)
        {
            return Payload.SequenceEqual(other.Payload);
        }

        public override int GetHashCode() => Payload.GetHashCode();
    }
}
