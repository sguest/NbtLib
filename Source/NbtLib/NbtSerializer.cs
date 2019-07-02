using System;
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
            var targetType = obj.GetType();

            // unsigned versions of integer-based types are stored as the next size up in order to avoid overflows
            if (targetType == typeof(sbyte))
            {
                return new NbtByteTag((sbyte)obj);
            }
            else if(targetType == typeof(short) || targetType == typeof(byte))
            {
                return new NbtShortTag((short)obj);
            }
            else if (targetType == typeof(int) || targetType == typeof(ushort))
            {
                return new NbtIntTag((int)obj);
            }
            else if (targetType == typeof(long) || targetType == typeof(uint))
            {
                return new NbtLongTag((long)obj);
            }
            else if(targetType == typeof(float))
            {
                return new NbtFloatTag((float)obj);
            }
            else if(targetType == typeof(double))
            {
                return new NbtDoubleTag((double)obj);
            }
            else if(targetType == typeof(string))
            {
                return new NbtStringTag(obj.ToString());
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
                else
                {
                    return null;
                }
            }
            else
            {
                return SerializeCompoundTag(obj);
            }
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
