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

            return DeserializeObject<T>(parsed);
        }

        public T DeserializeObject<T>(NbtCompoundTag compoundTag)
        {
            return (T)ParseNbtValue(compoundTag, typeof(T));
        }

        private object ParseNbtValue(INbtTag tag, Type targetType)
        {
            if (tag is NbtCompoundTag compoundTag)
            {
                return ParseCompoundTag(compoundTag, targetType);
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
                    Type itemType = ReflectionHelpers.GetEnumerableGenericType(targetType);

                    var items = listTag.Select(item => ParseNbtValue(item, itemType)).ToList().AsEnumerable();

                    return MapToCollection(items, itemType, targetType);
                }
            }

            return null;
        }

        private object ParseCompoundTag(NbtCompoundTag compoundTag, Type targetType)
        {
            var dictionaryTypes = GetDictionaryGenericTypes(targetType);
            if (dictionaryTypes == null)
            {
                return MapToObject(compoundTag, targetType);
            }
            else
            {
                return MapToDictionary(compoundTag, targetType, dictionaryTypes);
            }
        }

        private object MapToObject(NbtCompoundTag compoundTag, Type targetType)
        {
            if (targetType.IsInterface)
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
                    SetPropertyValue(info, obj, childTag.Value);
                }

                var props = targetType.GetProperties().Where(p => Attribute.IsDefined(p, typeof(NbtPropertyAttribute)));
                foreach (var prop in props)
                {
                    var attribute = prop.GetCustomAttribute<NbtPropertyAttribute>();
                    if(attribute.PropertyName.Equals(childTag.Key, StringComparison.InvariantCultureIgnoreCase))
                    {
                        SetPropertyValue(prop, obj, childTag.Value);
                    }
                }
            }

            return obj;
        }

        private void SetPropertyValue(PropertyInfo info, object parent, INbtTag childTag)
        {
            if (!Attribute.IsDefined(info, typeof(NbtIgnoreAttribute)))
            {
                var value = ParseNbtValue(childTag, info.PropertyType);
                if (info.PropertyType.IsAssignableFrom(value.GetType()))
                {
                    info.SetValue(parent, value);
                }
            }
        }

        private object MapToDictionary(NbtCompoundTag compoundTag, Type targetType, Type[] dictionaryTypes)
        {
            if (dictionaryTypes[0] != typeof(string))
            {
                return null;
            }

            object dictionary;
            if (targetType.IsInterface)
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
                var value = ParseNbtValue(childTag.Value, dictionaryTypes[1]);
                if (dictionaryTypes[1].IsAssignableFrom(value.GetType()))
                {
                    targetType.InvokeMember("Add", BindingFlags.InvokeMethod, null, dictionary, new[] { childTag.Key, ParseNbtValue(childTag.Value, dictionaryTypes[1]) });
                }
            }

            return dictionary;
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
