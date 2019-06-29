using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NbtLib
{
    public class NbtCompoundTag : NbtTag, IDictionary<string, NbtTag>, IEquatable<NbtCompoundTag>
    {
        public NbtTag this[string key] { get => ChildTags[key]; set => Add(key, value); }

        public override NbtTagType TagType => NbtTagType.Compound;

        private IDictionary<string, NbtTag> ChildTags { get; set; } = new Dictionary<string, NbtTag>();

        public ICollection<string> Keys => ChildTags.Keys;

        public ICollection<NbtTag> Values => ChildTags.Values;

        public int Count => ChildTags.Count;

        public bool IsReadOnly => false;

        public void Add(string key, NbtTag value) => ChildTags.Add(key, value);

        public void Add(KeyValuePair<string, NbtTag> item) => Add(item.Key, item.Value);

        public void Clear() => ChildTags.Clear();

        public bool Contains(KeyValuePair<string, NbtTag> item) => ChildTags.Contains(item);

        public bool ContainsKey(string key) => ChildTags.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, NbtTag>[] array, int arrayIndex) => ChildTags.CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<string, NbtTag>> GetEnumerator() => ChildTags.GetEnumerator();

        public bool Remove(string key) => ChildTags.Remove(key);

        public bool Remove(KeyValuePair<string, NbtTag> item) => ChildTags.Remove(item);

        public bool TryGetValue(string key, out NbtTag value) => ChildTags.TryGetValue(key, out value);

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
            return -2086293992 + EqualityComparer<IDictionary<string, NbtTag>>.Default.GetHashCode(ChildTags);
        }
    }
}
