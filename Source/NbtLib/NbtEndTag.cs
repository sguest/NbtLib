using System;

namespace NbtLib
{
    public class NbtEndTag : NbtTag, IEquatable<NbtEndTag>
    {
        public override NbtTagType TagType => NbtTagType.End;

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
    }
}
