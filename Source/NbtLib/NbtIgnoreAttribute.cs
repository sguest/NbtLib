using System;

namespace NbtLib
{
    /// <summary>
    /// Specifies that a property should be ignored when serializing or deserializing NBT data
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NbtIgnoreAttribute : Attribute
    {
    }
}
