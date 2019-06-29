using System;

namespace NbtLib
{
    public class NbtByteTag : NbtTag, IEquatable<NbtByteTag>
    {
        public NbtByteTag(sbyte payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Byte;
        public sbyte Payload { get; }

        public override bool Equals(object obj)
        {
            if (obj is NbtByteTag byteTag)
            {
                return Equals(byteTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtByteTag other)
        {
            return other.Payload == Payload;
        }

        public override int GetHashCode() => Payload;
    }
}
