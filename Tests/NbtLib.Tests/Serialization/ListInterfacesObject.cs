using System.Collections.Generic;

namespace NbtLib.Tests.Serialization
{
    public class ListInterfacesObject
    {
        public IList<int> IntList { get; set; }
        public IEnumerable<string> StringList { get; set; }
        public IReadOnlyCollection<object> EndList { get; set; }
    }
}
