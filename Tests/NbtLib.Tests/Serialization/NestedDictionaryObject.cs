using System.Collections.Generic;

namespace NbtLib.Tests.Serialization
{
    public class NestedDictionaryObject
    {
        public List<IDictionary<string, float>> ListChild { get; set; }
    }
}
