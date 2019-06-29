namespace NbtLib
{
    public class NbtByteTag : NbtTag
    {
        public NbtByteTag(sbyte payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.Byte;
        public sbyte Payload { get; }
    }
}
