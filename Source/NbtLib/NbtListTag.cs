using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NbtLib
{
    public class NbtListTag : INbtTag<IReadOnlyCollection<INbtTag>>, IList<INbtTag>, IEquatable<NbtListTag>
    {
        public NbtListTag(NbtTagType itemType)
        {
            ItemType = itemType;
        }

        public INbtTag this[int index] { get => ChildTags[index]; set => Insert(index, value); }
        public IReadOnlyCollection<INbtTag> Payload => ChildTags.AsReadOnly();

        public NbtTagType TagType => NbtTagType.List;
        public NbtTagType ItemType { get; }

        private List<INbtTag> ChildTags { get; set; } = new List<INbtTag>();

        public int Count => this.ChildTags.Count;

        public bool IsReadOnly => false;

        public void Add(INbtTag item) {
            if(item.TagType != ItemType)
            {
                throw new System.InvalidOperationException($"Unable to insert tag of type {item.TagType} into list of type {ItemType}");
            }
            ChildTags.Add(item);
        }

        public void Clear() => ChildTags.Clear();

        public bool Contains(INbtTag item) => ChildTags.Contains(item);

        public void CopyTo(INbtTag[] array, int arrayIndex) => ChildTags.CopyTo(array, arrayIndex);

        public IEnumerator<INbtTag> GetEnumerator() => ChildTags.GetEnumerator();

        public int IndexOf(INbtTag item) => ChildTags.IndexOf(item);

        public void Insert(int index, INbtTag item)
        {
            if (item.TagType != ItemType)
            {
                throw new System.InvalidOperationException($"Unable to insert tag of type {item.TagType} into list of type {ItemType}");
            }
            ChildTags.Insert(index, item);
        }

        public bool Remove(INbtTag item) => ChildTags.Remove(item);

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
            hashCode = hashCode * -1521134295 + EqualityComparer<IList<INbtTag>>.Default.GetHashCode(ChildTags);
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            return hashCode;
        }
    }
}
