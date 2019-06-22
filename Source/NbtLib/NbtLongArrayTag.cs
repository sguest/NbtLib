namespace NbtLib
{
    public class NbtLongArrayTag : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.LongArray;
        public long[] Payload { get; set; }
    }
}
