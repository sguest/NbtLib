using System;

namespace NbtLib
{
    /// <summary>
    /// Tag representing a signed 16-bit integer
    /// </summary>
    public struct NbtShortTag : INbtTag<short>, IEquatable<NbtShortTag>
    {
        public NbtShortTag(short payload)
        {
            Payload = payload;
        }

        public NbtTagType TagType => NbtTagType.Short;
        public short Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtShortTag shortTag)
            {
                return Equals(shortTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtShortTag other)
        {
            return other.Payload == Payload;
        }

        public override int GetHashCode() => Payload;

        public override string ToString() => Payload.ToString();

        public string ToJsonString() => ToString();
    }
}
