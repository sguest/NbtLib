namespace NbtLib
{
    public class NbtDoubleTag : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Long;
        public double Payload { get; set; }
    }
}
