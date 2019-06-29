using System.Collections.Generic;

namespace NbtLib.Tests.Serialization
{
    public class ArrayCollectionsObject
    {
        public IEnumerable<byte> ByteArray { get; set; }
        public IReadOnlyCollection<int> IntArray { get; set; }
        public List<long> LongArray { get; set; }
    }
}
