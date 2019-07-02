using System;
using System.IO;
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
            return (NbtCompoundTag)SerializeTag(obj);
        }

        private INbtTag SerializeTag(object obj)
        {
            var targetType = obj.GetType();

            if (targetType == typeof(int))
            {
                return new NbtIntTag((int)obj);
            }
            else if(targetType == typeof(string))
            {
                return new NbtStringTag(obj.ToString());
            }
            else
            {
                var tag = new NbtCompoundTag();

                foreach (var propInfo in targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if(propInfo.CanRead)
                    {
                        var name = propInfo.Name;
                        var value = SerializeTag(propInfo.GetValue(obj));
                        tag.Add(name, value);
                    }
                }

                return tag;
            }
        }
    }
}
