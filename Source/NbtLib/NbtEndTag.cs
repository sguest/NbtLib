using System;

namespace NbtLib
{
    /// <summary>
    /// Marker tag to represent the end of a compound tag.
    /// Parsed structures will typically not contain tags of this type as they are a file marker only.
    /// </summary>
    public struct NbtEndTag : INbtTag, IEquatable<NbtEndTag>
    {
        public NbtTagType TagType => NbtTagType.End;

        public override bool Equals(object obj)
        {
            if (obj is NbtEndTag endTag)
            {
                return true;
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtEndTag other) => true;

        public override int GetHashCode() => 0;

        public string ToString() => string.Empty;

        public string ToJsonString() => string.Empty;
    }
}
