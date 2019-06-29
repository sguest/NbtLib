using System;

namespace NbtLib
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NbtIgnoreAttribute : Attribute
    {
    }
}
