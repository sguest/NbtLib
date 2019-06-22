namespace NbtLib
{
    public class NbtLongTag : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Long;
        public long Payload { get; set; }
    }
}
