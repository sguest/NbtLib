namespace NbtLib
{
    public class NbtLongTag : NbtTag
    {
        public NbtLongTag(long payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Long;
        public long Payload { get; }
    }
}
