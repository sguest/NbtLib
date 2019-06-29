using System;

namespace NbtLib
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NbtPropertyAttribute : Attribute
    {
        public string PropertyName { get; set; }
    }
}
