namespace NbtLib
{
    public class NbtStringTag : NbtTag
    {
        public NbtStringTag(string payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.String;
        public string Payload { get; }
    }
}
