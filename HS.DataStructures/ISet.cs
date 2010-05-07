namespace HS.DataStructures
{
    public interface ISet<T>
    {
        void Add(T item);
        void Clear();
        bool Contains(T value);
        bool Remove(T item);
        int Count { get; }
        Set<T> Union(Set<T> other);
        Set<T> Intersection(Set<T> other);
        Set<T> Difference(Set<T> other);
    }
}