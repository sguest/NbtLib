namespace NbtLib
{
    public class NbtFloatTag : NbtTag
    {
        public NbtFloatTag(float payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Float;
        public float Payload { get; }
    }
}
