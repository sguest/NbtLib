namespace NbtLib
{
    public class NbtIntArrayTag : NbtTag
    {
        public override NbtTagType TagType => NbtTagType.IntArray;
        public int[] Payload { get; set; }
    }
}
