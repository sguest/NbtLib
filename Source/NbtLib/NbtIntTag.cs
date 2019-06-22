namespace NbtLib
{
    public class NbtIntTag : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Int;
        public int Payload { get; set; }
    }
}
