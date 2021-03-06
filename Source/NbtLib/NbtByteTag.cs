﻿using System;

namespace NbtLib
{
    /// <summary>
    /// Tag representing a signed byte value
    /// </summary>
    public struct NbtByteTag : INbtTag<sbyte>, IEquatable<NbtByteTag>
    {
        public NbtByteTag(sbyte payload)
        {
            Payload = payload;
        }

        public NbtTagType TagType => NbtTagType.Byte;
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

        public override string ToString() => Payload.ToString();

        public string ToJsonString() => ToString();
    }
}
