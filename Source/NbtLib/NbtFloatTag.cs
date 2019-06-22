namespace NbtLib
{
    public class NbtFloatTag : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.Float;
        public float Payload { get; set; }
    }
}
