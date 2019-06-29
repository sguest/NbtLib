using System;

namespace NbtLib
{
    public struct NbtIntTag : INbtTag<int>, IEquatable<NbtIntTag>
    {
        public NbtIntTag(int payload)
        {
            Payload = payload;
        }

        public NbtTagType TagType => NbtTagType.Int;
        public int Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtIntTag intTag)
            {
                return Equals(intTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtIntTag other)
        {
            return other.Payload == Payload;
        }

        public override int GetHashCode() => Payload;
    }
}
