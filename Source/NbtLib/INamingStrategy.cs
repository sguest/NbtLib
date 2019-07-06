namespace NbtLib
{
    /// <summary>
    /// Provides implementation to serializer on how to map property names to tag names in the output
    /// </summary>
    public interface INamingStrategy
    {
        /// <summary>
        /// Will be called for each mapped property, the returned value will be used as the tag name
        /// </summary>
        /// <param name="name">The name of the property being mapped</param>
        /// <returns>The tag name to be used</returns>
        string GetTagName(string name);
    }
}
