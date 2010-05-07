using System;
using System.Collections.Generic;

namespace HS.DataStructures
{
    public class LruCache<TKey, TValue> : ICache<TKey, TValue>
    {
        private class Node
        {
            public Node Previous { get; set; }
            public KeyValuePair<TKey, TValue> Data { get; private set; }
            public Node Next { get; set; }

            public Node(Node previous, KeyValuePair<TKey, TValue> data)
            {
                Previous = previous;
                Data = data;
                Next = null;
            }
        }

        private readonly Dictionary<TKey, Node> dictionary;

        private Node first;
        private Node last;
        public long Capacity { get; private set; }

        public LruCache(long capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException
                    ("capacity", capacity, "Capacity must be greater than 0");
            }

            Capacity = capacity;

            dictionary = new Dictionary<TKey, Node>();
        }

        public TValue Get(TKey key)
        {
            var data = dictionary[key].Data;
            Put(data.Key, data.Value);
            return data.Value;
        }

        public void Put(TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
                dictionary.Remove(key);

            var node = new Node(last, new KeyValuePair<TKey, TValue>(key, value));

            if (first == null)
                first = node;

            if (last != null)
                last.Next = node;

            last = node;
            dictionary[key] = node;

            if (dictionary.Count <= Capacity)
            {
                return;
            }

            if (first == last)
            {
                first = last = null;
                return;
            }

            var condemned = first;
            condemned.Next.Previous = null;
            first = condemned.Next;
            condemned.Next = null;
            dictionary.Remove(condemned.Data.Key);
        }

        public bool Contains(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public void Delete(TKey key)
        {
            var node = dictionary[key];

            if (node.Previous != null)
            {
                node.Previous.Next = node.Next;
            }
            else
            {
                first = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Previous = node.Previous;
            }
            else
            {
                last = node.Previous;
            }

            dictionary.Remove(key);
        }

        public void Clear()
        {
            dictionary.Clear();
            first = last = null;
            Capacity = 0;
        }

        public long Count
        {
            get { return dictionary.Count; }
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> Items()
        {
            foreach (var item in dictionary)
            {
                yield return item.Value.Data;
            }
        }
    }
}
