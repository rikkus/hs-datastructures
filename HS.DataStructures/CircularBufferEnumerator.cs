using System;
using System.Collections;
using System.Collections.Generic;

namespace HS.DataStructures
{
    public class CircularBufferEnumerator<T> : IEnumerator<T>
    {
        private readonly CircularBuffer<T> buffer;
        private int index;

        public CircularBufferEnumerator(CircularBuffer<T> buffer)
        {
            this.buffer = buffer;
        }

        #region IEnumerator<T> Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool MoveNext()
        {
            if (index == buffer.Size - 1)
            {
                return false;
            }

            ++index;
            return true;
        }

        public void Reset()
        {
            index = 0;
        }

        public T Current
        {
            get { return buffer[index]; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}