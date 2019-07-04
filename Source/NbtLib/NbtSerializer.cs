﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NbtLib
{
    public class NbtSerializer
    {
        public Stream SerializeObject(object obj)
        {
            var tag = SerializeObjectToTag(obj);

            var writer = new NbtWriter();
            return writer.CreateNbtStream(tag);
        }

        public NbtCompoundTag SerializeObjectToTag(object obj)
        {
            return SerializeCompoundTag(obj);
        }

        private INbtTag SerializeTag(object obj)
        {
            if(obj == null)
            {
                return null;
            }

            var targetType = obj.GetType();

            var primitiveTypes = GetPrimitiveTagType(targetType);
            if(primitiveTypes != null)
            {
                return (INbtTag)Activator.CreateInstance(primitiveTypes.Item1, obj);
            }
            else if (obj is IEnumerable enumerable)
            {
                var enumerableType = ReflectionHelpers.GetEnumerableGenericType(targetType) ?? typeof(object);

                if (enumerableType == typeof(byte))
                {
                    return new NbtByteArrayTag((obj as IEnumerable<byte>).ToArray());
                }
                else if (enumerableType == typeof(int))
                {
                    return new NbtIntArrayTag((obj as IEnumerable<int>).ToArray());
                }
                else if (enumerableType == typeof(long))
                {
                    return new NbtLongArrayTag((obj as IEnumerable<long>).ToArray());
                }

                var enumerableTagTypes = GetPrimitiveTagType(enumerableType);
                if (enumerableTagTypes == null)
                {
                    enumerableTagTypes = new Tuple<Type, NbtTagType>(typeof(NbtCompoundTag), NbtTagType.Compound);
                }

                var tag = new NbtListTag(enumerableTagTypes.Item2);
                foreach (var item in enumerable)
                {
                    tag.Add(SerializeTag(item));
                }
                return tag;
            }
            else
            {
                return SerializeCompoundTag(obj);
            }
        }

        private Tuple<Type, NbtTagType>GetPrimitiveTagType(Type targetType)
        {
            // unsigned versions of integer-based types are stored as the next size up in order to avoid overflows
            if (targetType == typeof(sbyte))
            {
                return new Tuple<Type, NbtTagType>(typeof(NbtByteTag), NbtTagType.Byte);
            }
            else if (targetType == typeof(short) || targetType == typeof(byte))
            {
                return new Tuple<Type, NbtTagType>(typeof(NbtShortTag), NbtTagType.Short);
            }
            else if (targetType == typeof(int) || targetType == typeof(ushort))
            {
                return new Tuple<Type, NbtTagType>(typeof(NbtIntTag), NbtTagType.Int);
            }
            else if (targetType == typeof(long) || targetType == typeof(uint))
            {
                return new Tuple<Type, NbtTagType>(typeof(NbtLongTag), NbtTagType.Long);
            }
            else if (targetType == typeof(float))
            {
                return new Tuple<Type, NbtTagType>(typeof(NbtFloatTag), NbtTagType.Float);
            }
            else if (targetType == typeof(double))
            {
                return new Tuple<Type, NbtTagType>(typeof(NbtDoubleTag), NbtTagType.Double);
            }
            else if (targetType == typeof(string))
            {
                return new Tuple<Type, NbtTagType>(typeof(NbtStringTag), NbtTagType.String);
            }

            return null;
        }

        private NbtCompoundTag SerializeCompoundTag(object obj)
        {
            var targetType = obj.GetType();
            var tag = new NbtCompoundTag();

            if (obj is IDictionary dictionary)
            {
                foreach (var key in dictionary.Keys)
                {
                    tag.Add(key.ToString(), SerializeTag(dictionary[key]));
                }
            }
            else
            {
                foreach (var propInfo in targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (propInfo.CanRead)
                    {
                        var name = propInfo.Name;
                        var value = SerializeTag(propInfo.GetValue(obj));
                        tag.Add(name, value);
                    }
                }

            }
            return tag;
        }
    }
}
