using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NbtLib
{
    public class NbtListTag : NbtTag, IList<NbtTag>, IEquatable<NbtListTag>
    {
        public NbtListTag(NbtTagType itemType)
        {
            ItemType = itemType;
        }

        public NbtTag this[int index] { get => ChildTags[index]; set => Insert(index, value); }

        public override NbtTagType TagType => NbtTagType.List;
        public NbtTagType ItemType { get; }

        private IList<NbtTag> ChildTags { get; set; } = new List<NbtTag>();

        public int Count => this.ChildTags.Count;

        public bool IsReadOnly => false;

        public void Add(NbtTag item) {
            if(item.TagType != ItemType)
            {
                throw new System.InvalidOperationException($"Unable to insert tag of type {item.TagType} into list of type {ItemType}");
            }
            ChildTags.Add(item);
        }

        public void Clear() => ChildTags.Clear();

        public bool Contains(NbtTag item) => ChildTags.Contains(item);

        public void CopyTo(NbtTag[] array, int arrayIndex) => ChildTags.CopyTo(array, arrayIndex);

        public IEnumerator<NbtTag> GetEnumerator() => ChildTags.GetEnumerator();

        public int IndexOf(NbtTag item) => ChildTags.IndexOf(item);

        public void Insert(int index, NbtTag item)
        {
            if (item.TagType != ItemType)
            {
                throw new System.InvalidOperationException($"Unable to insert tag of type {item.TagType} into list of type {ItemType}");
            }
            ChildTags.Insert(index, item);
        }

        public bool Remove(NbtTag item) => ChildTags.Remove(item);

        public void RemoveAt(int index) => ChildTags.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object obj)
        {
            if(obj is NbtListTag listTag)
            {
                return Equals(listTag);
            }

            return base.Equals(obj);
        }

        public bool Equals(NbtListTag other) => this.SequenceEqual(other);

        public override int GetHashCode()
        {
            var hashCode = 562106404;
            hashCode = hashCode * -1521134295 + ItemType.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<NbtTag>>.Default.GetHashCode(ChildTags);
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            return hashCode;
        }
    }
}
