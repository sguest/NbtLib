using System;

namespace NbtLib
{
    public class NbtLongTag : NbtTag, IEquatable<NbtLongTag>
    {
        public NbtLongTag(long payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Long;
        public long Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtLongTag longTag)
            {
                return Equals(longTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtLongTag other)
        {
            return other.Payload == Payload;
        }

        public override int GetHashCode() => Payload.GetHashCode();
    }
}
