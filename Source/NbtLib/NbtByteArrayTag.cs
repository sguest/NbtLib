namespace NbtLib
{
    public class NbtByteArrayTag : NbtTag
    {
        public NbtByteArrayTag(byte[] payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.ByteArray;
        public byte[] Payload { get; }
    }
}
