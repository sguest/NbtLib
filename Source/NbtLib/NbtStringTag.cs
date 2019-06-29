using System;

namespace NbtLib
{
    public class NbtStringTag : NbtTag, IEquatable<NbtStringTag>
    {
        public NbtStringTag(string payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.String;
        public string Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtStringTag stringTag)
            {
                return Equals(stringTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtStringTag other)
        {
            return other.Payload == Payload;
        }

        public override int GetHashCode() => Payload.GetHashCode();
    }
}
