namespace NbtLib
{
    public class NbtIntArrayTag : NbtTag
    {
        public NbtIntArrayTag(int[] payload)
        {
            Payload = payload;
        }

        public override NbtTagType TagType => NbtTagType.IntArray;
        public int[] Payload { get; }
    }
}
