namespace NbtLib
{
    public class NbtIntTag : NbtTag
    {
        public NbtIntTag(int payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Int;
        public int Payload { get; }
    }
}
