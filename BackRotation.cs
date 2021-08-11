using System;
using System.Buffers;
using System.Collections;
using System.Linq;

namespace SpecificCollections
{
    public class BackRotation<T>
    {
        int position = -1;

        private T[] _values;
        int _size;
        int _pos;

        public T this[int index]
        {
            get { return _values[index]; }
            set { _values[index] = value; }
        }

        public int Count { get { return _values.Length; } }

        public BackRotation(int size)
        {
            _size = size;
            _values = new T[size];
        }

        public void Add(T item)
        {
            if (_pos == 0)
            {
                _values[_pos] = item;
                _pos++;
            }
            else if (_pos < _size)
            {
                var regrouped = new T[_size];
                Array.ConstrainedCopy(_values, 0, regrouped, 1, _size - 1);
                regrouped[0] = item;
                _values = regrouped;
                _pos++;
            }
            else if (_pos == _size)
            {
                var regrouped = new T[_size];
                Array.ConstrainedCopy(_values, 0, regrouped, 1, _size - 1);
                regrouped[0] = item;
                _values = regrouped;
            }
        }

        public IEnumerator GetEnumerator()
        {
            while (true)
            {
                if (position < _values.Length - 1)
                {
                    position++;
                    yield return _values[position];
                }
                else
                {
                    Reset();
                    yield break;
                }
            }
        }

        private void Reset()
        {
            position = -1;
        }
    }
}
