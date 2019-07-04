using System;

namespace NbtLib
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NbtPropertyAttribute : Attribute
    {
        public string PropertyName { get; set; }

        private bool? useArrayTypeImpl = null;
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
        public bool IsUseArrayTypeSpecified
        {
            get
            {
                return useArrayTypeImpl.HasValue;
            }
        }
    }
}
