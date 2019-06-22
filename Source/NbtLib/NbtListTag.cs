using System.Collections.Generic;

namespace NbtLib
{
    public class NbtListTag : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.List;
        public IList<NbtTag> ChildTags { get; set; } = new List<NbtTag>();
        public NbtTagType ItemType { get; set; }
    }
}
