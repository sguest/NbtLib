namespace NbtLib
{
    public interface INbtTag
    {
        NbtTagType TagType { get; }
    }

    public interface INbtTag<T> : INbtTag
    {
        T Payload { get; }
    }
}
