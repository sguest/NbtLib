namespace NbtLib
{
    /// <summary>
    /// Byte representations used to differntiate tags in an NBT stream
    /// </summary>
    public enum NbtTagType : byte
    {
        End = 0,
        Byte = 1,
        Short = 2,
        Int = 3,
        Long = 4,
        Float = 5,
        Double = 6,
        ByteArray = 7,
        String = 8,
        List = 9,
        Compound = 10,
        IntArray = 11,
        LongArray = 12
    }
}
