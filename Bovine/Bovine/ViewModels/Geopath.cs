using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Bovine.ViewModels
{
    public class Geopath : IList
    {
        private object[] _contents = new object[10];

        private int _count;

        public object this[int index]
        {
            get
            {
                return _contents[index];
            }
            set
            {
                _contents[index] = value;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return true;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public int Add(object value)
        {
            if (_count < _contents.Length)
            {
                _contents[_count] = value;
                _count++;

                return (_count - 1);
            }

            return -1;
        }

        public void Clear()
        {
            _count = 0;
        }

        public bool Contains(object value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_contents[i] == value)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(Array array, int index)
        {
            for (int i = 0; i < Count; i++)
            {
                array.SetValue(_contents[i], index++);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // Refer to the IEnumerator documentation for an example of
            // implementing an enumerator.
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public int IndexOf(object value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_contents[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, object value)
        {
            if ((_count + 1 <= _contents.Length) && (index < Count) && (index >= 0))
            {
                _count++;

                for (int i = Count - 1; i > index; i--)
                {
                    _contents[i] = _contents[i - 1];
                }
                _contents[index] = value;
            }
        }

        public void Remove(object value)
        {
            RemoveAt(IndexOf(value));
        }

        public void RemoveAt(int index)
        {
            if ((index >= 0) && (index < Count))
            {
                for (int i = index; i < Count - 1; i++)
                {
                    _contents[i] = _contents[i + 1];
                }
                _count--;
            }
        }

        public void PrintContents()
        {
            Console.WriteLine($"List has a capacity of {_contents.Length} and currently has {_count} elements.");
            Console.Write("List contents:");
            for (int i = 0; i < Count; i++)
            {
                Console.Write($" {_contents[i]}");
            }
            Console.WriteLine();
        }
    }
}
