using System.Collections.Generic;

namespace NbtLib.Tests.Serialization
{
    public class CollectionsAttributesObject
    {
        [NbtProperty(UseArrayType = false)]
        public List<int> IntList { get; set; }

        [NbtProperty(EmptyListAsEnd = true)]
        public List<object> EmptyList { get; set; }
    }
}
