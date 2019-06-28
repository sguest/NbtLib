namespace NbtLib
{
    public class NbtDoubleTag : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Double;
        public double Payload { get; set; }
    }
}
