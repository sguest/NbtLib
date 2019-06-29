namespace NbtLib
{
    public class NbtDoubleTag : NbtTag
    {
        public NbtDoubleTag(double payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Double;
        public double Payload { get; }
    }
}
