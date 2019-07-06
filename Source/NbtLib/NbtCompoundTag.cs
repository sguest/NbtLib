using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NbtLib
{
    public class NbtCompoundTag : INbtTag<IReadOnlyDictionary<string, INbtTag>>, IDictionary<string, INbtTag>, IEquatable<NbtCompoundTag>
    {
        public INbtTag this[string key] { get => ChildTags[key]; set => Add(key, value); }

        public NbtTagType TagType => NbtTagType.Compound;

        public IReadOnlyDictionary<string, INbtTag> Payload => new ReadOnlyDictionary<string, INbtTag>(ChildTags);

        private IDictionary<string, INbtTag> ChildTags { get; set; } = new Dictionary<string, INbtTag>();

        public ICollection<string> Keys => ChildTags.Keys;

        public ICollection<INbtTag> Values => ChildTags.Values;

        public int Count => ChildTags.Count;

        public bool IsReadOnly => false;

        public void Add(string key, INbtTag value) => ChildTags.Add(key, value);

        public void Add(KeyValuePair<string, INbtTag> item) => Add(item.Key, item.Value);

        public void Clear() => ChildTags.Clear();

        public bool Contains(KeyValuePair<string, INbtTag> item) => ChildTags.Contains(item);

        public bool ContainsKey(string key) => ChildTags.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, INbtTag>[] array, int arrayIndex) => ChildTags.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<string, INbtTag>> GetEnumerator() => ChildTags.GetEnumerator();

        public bool Remove(string key) => ChildTags.Remove(key);

        public bool Remove(KeyValuePair<string, INbtTag> item) => ChildTags.Remove(item);

        public bool TryGetValue(string key, out INbtTag value) => ChildTags.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object obj)
        {
            if (obj is NbtCompoundTag compoundTag)
            {
                return Equals(compoundTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtCompoundTag other) => this.SequenceEqual(other);

        public override int GetHashCode()
        {
            return -2086293992 + EqualityComparer<IDictionary<string, INbtTag>>.Default.GetHashCode(ChildTags);
        }

        public override string ToString() => "{" + string.Join(", ", ChildTags.Select(kvp => kvp.Key + "=" + kvp.Value.ToString())) + "}";

        public string ToJsonString()
        {
            return ToJsonString(0);
        }

        private string ToJsonString(int indentLevel)
        {
            var linePrefix = new string(' ', indentLevel + 2);
            var builder = new StringBuilder();
            builder.AppendLine("{");

            var count = 1;
            foreach (var tag in ChildTags)
            {
                builder.Append(linePrefix).Append($"\"{tag.Key}\": ");

                if(tag.Value is NbtCompoundTag compoundTag)
                {
                    builder.Append(compoundTag.ToJsonString(indentLevel + 2));
                }
                else
                {
                    builder.Append(tag.Value.ToJsonString());
                }

                if(count < ChildTags.Count)
                {
                    builder.Append(",");
                    count++;
                }

                builder.AppendLine();
            }

            builder.Append(new string(' ', indentLevel)).Append("}");
            return builder.ToString();
        }
    }
}
