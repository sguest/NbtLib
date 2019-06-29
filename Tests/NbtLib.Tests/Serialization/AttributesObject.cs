namespace NbtLib.Tests.Serialization
{
    public class AttributesObject
    {
        [NbtIgnore]
        public int Int5 { get; set; }
        [NbtProperty(PropertyName = "String Abcd")]
        public string SomeString { get; set; }
    }
}
