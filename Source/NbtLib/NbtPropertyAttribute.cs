using System;

namespace NbtLib
{
    /// <summary>
    /// Provides customization for handing of NBT tags when serializing or deserializing
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NbtPropertyAttribute : Attribute
    {
        /// <summary>
        /// When (de)serializing, this name will be used instead of the property's reflected name
        /// </summary>
        public string PropertyName { get; set; }

        private bool? useArrayTypeImpl = null;
        /// <summary>
        /// If true, property will be serialized to an appropriate array tag type if available.
        /// If false, a List tag will always be used.
        /// </summary>
        public bool UseArrayType {
            get
            {
                return useArrayTypeImpl.GetValueOrDefault(false);
            }
            set
            {
                useArrayTypeImpl = value;
            }
        }
        internal bool IsUseArrayTypeSpecified
        {
            get
            {
                return useArrayTypeImpl.HasValue;
            }
        }

        private bool? emptyListAsEndImpl = null;
        /// <summary>
        /// If true, an empty collection serialized to a List will specify an item type of End Tag
        /// </summary>
        public bool EmptyListAsEnd
        {
            get
            {
                return emptyListAsEndImpl.GetValueOrDefault(false);
            }
            set
            {
                emptyListAsEndImpl = value;
            }
        }
        internal bool IsEmptyListAsEndSpecified
        {
            get
            {
                return emptyListAsEndImpl.HasValue;
            }
        }
    }
}
