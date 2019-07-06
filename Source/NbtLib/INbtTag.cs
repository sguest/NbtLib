namespace NbtLib
{
    public interface INbtTag
    {
        NbtTagType TagType { get; }
        string ToJsonString();
    }

    public interface INbtTag<T> : INbtTag
    {
        T Payload { get; }
    }
}
