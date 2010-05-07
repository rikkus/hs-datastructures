using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HS.DataStructures
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private readonly T[] data;

        private int back;
        private int front;
        private int size;

        public CircularBuffer(int capacity)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException
                    ("capacity", capacity, "Capacity must be greater than 0");
            }

            data = new T[capacity];
            Clear();
        }

        public int Capacity
        {
            get { return data.Length; }
        }

        public int Size
        {
            get { return size; }

            private set
            {
                size = value;

                if (size == 0)
                {
                    front = -1;
                }
            }
        }

        public bool Empty
        {
            get { return Size == 0; }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Size)
                {
                    throw new ArgumentOutOfRangeException
                        (
                        "index",
                        index,
                        "Index must be within the bounds of the collection"
                        );
                }

                return front >= back ? data[back + index] : data[(back + index) % Capacity];
            }
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new CircularBufferEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public T[] ToArray()
        {
            var ret = new T[Size];

            if (Size == 0)
            {
                return ret;
            }

            if (front >= back)
            {
                Array.Copy(data, back, ret, 0, front - back + 1);
            }
            else
            {
                Array.Copy(data, Capacity - 1, ret, 0, Capacity - back);
                Array.Copy(data, front, ret, front + 1, front + 1);
            }

            return ret;
        }

        public void Add(T item)
        {
            AdvanceCursor(ref front);

            if (Size == Capacity)
            {
                AdvanceCursor(ref back);
            }
            else
            {
                ++Size;
            }

            data[front] = item;
        }

        private void AdvanceCursor(ref int cursor)
        {
            if (cursor == Capacity - 1)
            {
                cursor = 0;
            }
            else
            {
                ++cursor;
            }
        }

        public T Remove()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Buffer empty");
            }

            T ret = data[back];

            AdvanceCursor(ref back);

            --Size;

            return ret;
        }

        public T First()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Buffer empty");
            }

            return data[back];
        }

        public void Clear()
        {
            Size = 0;
        }

        public override string ToString()
        {
            var builder = new StringBuilder("{");

            for (int index = 0; index < Math.Min(Size, 10); index++)
            {
                if (index > 0)
                {
                    builder.Append(", ");
                }
                else
                {
                    builder.Append(" ");
                }

                builder.Append(this[index].ToString());
            }

            if (Size > 10)
            {
                builder.Append(", [...]");
            }

            builder.Append(" }");

            return builder.ToString();
        }
    }
}