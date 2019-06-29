using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NbtLib
{
    public class NbtDeserializer
    {
        public T DeserializeObject<T>(Stream stream)
        {
            var parser = new NbtParser();
            var parsed = parser.ParseNbtStream(stream);

            return (T)ParseNbtValue(parsed, typeof(T));
        }

        private object ParseNbtValue(INbtTag tag, Type targetType)
        {
            if (tag is NbtCompoundTag compoundTag)
            {

                var dictionaryTypes = GetDictionaryGenericTypes(targetType);
                if (dictionaryTypes == null)
                {
                    if(targetType.IsInterface)
                    {
                        return null;
                    }

                    var obj = Activator.CreateInstance(targetType);

                    foreach (var childTag in compoundTag)
                    {
                        var propName = childTag.Key.Replace(" ", "");
                        var info = targetType.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (info != null)
                        {
                            info.SetValue(obj, ParseNbtValue(childTag.Value, info.PropertyType));
                        }
                    }

                    return obj;
                }
                else
                {
                    if (dictionaryTypes[0] != typeof(string))
                    {
                        return null;
                    }

                    object dictionary;
                    if(targetType.IsInterface)
                    {
                        var dictionaryType = typeof(Dictionary<,>).MakeGenericType(dictionaryTypes);
                        dictionary = Activator.CreateInstance(dictionaryType);
                    }
                    else
                    {
                        dictionary = Activator.CreateInstance(targetType);
                    }

                    foreach (var childTag in compoundTag)
                    {
                        targetType.InvokeMember("Add", BindingFlags.InvokeMethod, null, dictionary, new[] { childTag.Key, ParseNbtValue(childTag.Value, dictionaryTypes[1]) });
                    }

                    return dictionary;
                }
            }
            else if (tag is NbtByteTag byteTag)
            {
                return byteTag.Payload;
            }
            else if (tag is NbtShortTag shortTag)
            {
                return shortTag.Payload;
            }
            else if (tag is NbtIntTag intTag)
            {
                return intTag.Payload;
            }
            else if (tag is NbtLongTag longTag)
            {
                return longTag.Payload;
            }
            else if (tag is NbtFloatTag floatTag)
            {
                return floatTag.Payload;
            }
            else if (tag is NbtDoubleTag doubleTag)
            {
                return doubleTag.Payload;
            }
            else if (tag is NbtStringTag stringTag)
            {
                return stringTag.Payload;
            }
            else if (tag is NbtByteArrayTag byteArrayTag)
            {
                return MapToCollection(byteArrayTag.Payload, typeof(byte), targetType);
            }
            else if (tag is NbtIntArrayTag intArrayTag)
            {
                return MapToCollection(intArrayTag.Payload, typeof(int), targetType);
            }
            else if (tag is NbtLongArrayTag longArrayTag)
            {
                return MapToCollection(longArrayTag.Payload, typeof(long), targetType);
            }
            else if (tag is NbtListTag listTag)
            {
                foreach (Type interfaceType in targetType.GetInterfaces())
                {
                    Type itemType = GetEnumerableGenericType(targetType);

                    var items = listTag.Select(item => ParseNbtValue(item, itemType)).ToList().AsEnumerable();

                    return MapToCollection(items, itemType, targetType);
                }
            }

            return null;
        }

        private object MapToCollection(IEnumerable collection, Type genericType, Type targetType)
        {
            object list;
            Type collectionType;

            if(targetType.IsArray || targetType.IsInterface)
            {
                collectionType = typeof(List<>).MakeGenericType(genericType);
                list = Activator.CreateInstance(collectionType);
            }
            else
            {
                list = Activator.CreateInstance(targetType);
                collectionType = targetType;
            }

            foreach (var item in collection)
            {
                collectionType.InvokeMember("Add", BindingFlags.InvokeMethod, null, list, new object[] { item });
            }

            if(targetType.IsArray)
            {
                return collectionType.InvokeMember("ToArray", BindingFlags.InvokeMethod, null, list, new object[] { });
            }
            return list;
        }

        private Type GetEnumerableGenericType(Type collectionType)
        {
            foreach (Type interfaceType in collectionType.GetInterfaces().Union(new Type[] { collectionType }))
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition()
                    == typeof(IEnumerable<>))
                {
                    if(collectionType.IsArray)
                    {
                        return collectionType.GetElementType();
                    }

                    return collectionType.GetGenericArguments()[0];
                }
            }

            return null;
        }

        private Type[] GetDictionaryGenericTypes(Type dictionaryType)
        {
            foreach (Type interfaceType in dictionaryType.GetInterfaces().Union(new Type[] { dictionaryType }))
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition()
                    == typeof(IDictionary<,>))
                {
                    return dictionaryType.GetGenericArguments();
                }
            }

            return null;
        }
    }
}
