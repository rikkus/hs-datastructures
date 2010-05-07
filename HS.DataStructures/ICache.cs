using System.Collections.Generic;

namespace HS.DataStructures
{
    public interface ICache<TKey, TValue>
    {
        long Count { get; }
        bool Contains(TKey key);
        TValue Get(TKey key);
        void Put(TKey key, TValue value);
        void Delete(TKey key);
        void Clear();
        IEnumerable<KeyValuePair<TKey, TValue>> Items();
    }
}