namespace NbtLib
{
    public class NbtLongArrayTag : NbtTag
    {
        public NbtLongArrayTag(long[] payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.LongArray;
        public long[] Payload { get; }
    }
}
