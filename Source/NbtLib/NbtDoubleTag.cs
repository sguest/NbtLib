using System;

namespace NbtLib
{
    public struct NbtDoubleTag : INbtTag<double>, IEquatable<NbtDoubleTag>
    {
        public NbtDoubleTag(double payload)
        {
            Payload = payload;
        }

        public NbtTagType TagType => NbtTagType.Double;
        public double Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtDoubleTag doubleTag)
            {
                return Equals(doubleTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtDoubleTag other)
        {
            return other.Payload == Payload;
        }

        public override int GetHashCode() => Payload.GetHashCode();
    }
}
