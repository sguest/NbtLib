namespace NbtLib
{
    public class NbtShortTag : NbtTag
    {
        public NbtShortTag(short payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Short;
        public short Payload { get; }
    }
}
