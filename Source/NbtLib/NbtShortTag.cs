using System;

namespace NbtLib
{
    public class NbtShortTag : NbtTag, IEquatable<NbtShortTag>
    {
        public NbtShortTag(short payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Short;
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
    }
}
