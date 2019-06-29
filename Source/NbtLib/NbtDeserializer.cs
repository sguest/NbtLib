using System;
using System.Collections.Generic;
using System.IO;
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
                var obj = Activator.CreateInstance(targetType);

                var dictionaryTypes = GetDictionaryGenericTypes(targetType);
                if (dictionaryTypes == null)
                {
                    foreach (var childTag in compoundTag)
                    {
                        var propName = childTag.Key.Replace(" ", "");
                        var info = targetType.GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (info != null)
                        {
                            info.SetValue(obj, ParseNbtValue(childTag.Value, info.PropertyType));
                        }
                    }
                }
                else
                {
                    if (dictionaryTypes[0] != typeof(string))
                    {
                        throw new Exception("Can only deserialize objects with string keys");
                    }
                    foreach (var childTag in compoundTag)
                    {
                        targetType.InvokeMember("Add", BindingFlags.InvokeMethod, null, obj, new[] { childTag.Key, ParseNbtValue(childTag.Value, dictionaryTypes[1]) });
                    }
                }

            return obj;
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
                return byteArrayTag.Payload;
            }
            else if (tag is NbtIntArrayTag intArrayTag)
            {
                return intArrayTag.Payload;
            }
            else if (tag is NbtLongArrayTag longArrayTag)
            {
                return longArrayTag.Payload;
            }
            else if (tag is NbtListTag listTag)
            {
                if(listTag.ItemType == NbtTagType.End)
                {
                    return new List<object>();
                }

                foreach (Type interfaceType in targetType.GetInterfaces())
                {
                    Type itemType = GetCollectionGenericType(targetType);
                    var list = Activator.CreateInstance(targetType);

                    foreach (var childTag in listTag)
                    {
                        targetType.InvokeMember("Add", BindingFlags.InvokeMethod, null, list, new[] { ParseNbtValue(childTag, itemType) });
                    }
                    return list;
                }
            }

            throw new Exception($"Unable to deserialize tag of type {tag.TagType} to type {targetType}");
        }

        private Type GetCollectionGenericType(Type collectionType)
        {
            foreach (Type interfaceType in collectionType.GetInterfaces())
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition()
                    == typeof(ICollection<>))
                {
                    return collectionType.GetGenericArguments()[0];
                }
            }

            return null;
        }

        private Type[] GetDictionaryGenericTypes(Type dictionaryType)
        {
            foreach (Type interfaceType in dictionaryType.GetInterfaces())
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
