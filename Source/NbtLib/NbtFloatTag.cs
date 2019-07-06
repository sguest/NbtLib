using System;

namespace NbtLib
{
    public struct NbtFloatTag : INbtTag<float>, IEquatable<NbtFloatTag>
    {
        public NbtFloatTag(float payload)
        {
            Payload = payload;
        }

        public NbtTagType TagType => NbtTagType.Float;
        public float Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtFloatTag floatTag)
            {
                return Equals(floatTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtFloatTag other)
        {
            return other.Payload == Payload;
        }

        public override int GetHashCode() => Payload.GetHashCode();

        public override string ToString() => Payload.ToString();

        public string ToJsonString() => ToString();
    }
}
