namespace NbtLib
{
    /// <summary>
    /// Default naming strategy for property mapping. Leaves property names unchanged
    /// </summary>
    public class DefaultNamingStrategy : INamingStrategy
    {
        /// <summary>
        /// Maps tag name by returning the provided string unchanged
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>Provided name unchanged</returns>
        public string GetTagName(string name) => name;
    }
}
