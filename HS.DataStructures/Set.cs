using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HS.DataStructures
{
    public class Set<T> :
        ICollection<T>,
        ISerializable,
        IDeserializationCallback,
        ISet<T>,
        IEquatable<Set<T>>
    {
        private readonly Dictionary<T, bool> dict = new Dictionary<T, bool>();

        public void Add(T item)
        {
            if (!dict.ContainsKey(item))
                dict[item] = true;
        }

        public void Clear()
        {
            dict.Clear();
        }

        public bool Contains(T value)
        {
            return dict.ContainsKey(value);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            dict.Keys.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (!dict.ContainsKey(item))
                return false;

            dict.Remove(item);
            return true;
        }

        public int Count
        {
            get { return dict.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((IDictionary<T, bool>)dict).IsReadOnly; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return dict.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            dict.GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            dict.OnDeserialization(sender);
        }

        public Set<T> Union(Set<T> other)
        {
            var ret = new Set<T>();

            foreach (var item in this)
            {
                ret.Add(item);
            }

            foreach (var item in other)
            {
                ret.Add(item);
            }

            return ret;
        }

        public Set<T> Intersection(Set<T> other)
        {
            var ret = new Set<T>();

            foreach (var item in this)
            {
                if (other.Contains(item))
                    ret.Add(item);
            }

            return ret;
        }

        public Set<T> Difference(Set<T> other)
        {
            var ret = new Set<T>();

            foreach (var item in this)
            {
                if (!other.Contains(item))
                    ret.Add(item);
            }

            return ret;
        }

        public bool Equals(Set<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other.dict.Count != dict.Count)
                return false;

            foreach (var member in this)
            {
                if (!other.Contains(member))
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder("{");

            bool first = true;

            foreach (var member in this)
            {
                if (!first)
                {
                    builder.Append(", ");
                }

                first = false;

                builder.Append(member.ToString());
            }

            builder.Append("}");

            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Set<T>)) return false;
            return Equals((Set<T>) obj);
        }

        public override int GetHashCode()
        {
            return dict.GetHashCode();
        }

        public static bool operator ==(Set<T> left, Set<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Set<T> left, Set<T> right)
        {
            return !Equals(left, right);
        }
    }
}
