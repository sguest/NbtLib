namespace NbtLib
{
    /// <summary>
    /// Representation of a NBT tag.
    /// Typically used for collections, individual tags will implement the generic version of this interface.
    /// </summary>
    public interface INbtTag
    {
        /// <summary>
        /// Tag type discriminator byte
        /// </summary>
        NbtTagType TagType { get; }
        /// <summary>
        /// Get a formatted JSON representation of this tag
        /// </summary>
        /// <returns>Tag JSON representation</returns>
        string ToJsonString();
    }

    /// <summary>
    /// Generic version of a NBT tag
    /// </summary>
    /// <typeparam name="T">The type of the tag's payload</typeparam>
    public interface INbtTag<T> : INbtTag
    {
        /// <summary>
        /// The tag's payload
        /// </summary>
        T Payload { get; }
    }
}
