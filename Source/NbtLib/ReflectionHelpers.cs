using System;
using System.Collections.Generic;
using System.Linq;

namespace NbtLib
{
    internal static class ReflectionHelpers
    {
        public static Type GetEnumerableGenericType(Type collectionType)
        {
            foreach (Type interfaceType in collectionType.GetInterfaces().Union(new Type[] { collectionType }))
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition()
                    == typeof(IEnumerable<>))
                {
                    if (collectionType.IsArray)
                    {
                        return collectionType.GetElementType();
                    }

                    return collectionType.GetGenericArguments()[0];
                }
            }

            return null;
        }
    }
}
