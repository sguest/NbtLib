using System.Collections.Generic;

namespace NbtLib.Tests.Serialization
{
    public class NestedObject
    {
        public CompoundChild CompoundChild { get; set; }
        public List<ListChild> ListChild { get; set; }
    }

    public class CompoundChild
    {
        public string StringTag { get; set; }
        public int IntTag { get; set; }
    }

    public class ListChild
    {
        public float Float { get; set; }
    }
}
