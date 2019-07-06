namespace NbtLib
{
    /// <summary>
    /// Settings to customize NBT Serialization
    /// </summary>
    public class NbtSerializerSettings
    {
        /// <summary>
        /// Will be called for each serialized property to determine the tag name
        /// </summary>
        public INamingStrategy NamingStrategy { get; set; } = new DefaultNamingStrategy();
        /// <summary>
        /// If true, property will be serialized to an appropriate array tag type if available.
        /// If false, a List tag will always be used.
        /// </summary>
        public bool UseArrayTypes { get; set; } = true;
        /// <summary>
        /// If true, an empty collection serialized to a List will specify an item type of End Tag
        /// </summary>
        public bool EmptyListAsEnd { get; set; } = false;
    }
}
