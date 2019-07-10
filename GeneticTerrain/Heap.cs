using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticTerrain
{
    public class Heap<T> : IEnumerable
        where T : IComparable<T>
    {
        T[] _items;
        int _count;
        const int DEFAULT_LENGTH = 100;
        public Heap()
        : this(DEFAULT_LENGTH)
        {
        }
        public Heap(int length)
        {
            _items = new T[length];
            _count = 0;
        }
        public void Add(T value)
        {
            //TODO: rescale if too small
            if (_count >= _items.Length)
            {
                GrowBackingArray();
            }
            _items[_count] = value;
            int idx = _count;

            while (idx > 0 && _items[idx].CompareTo(_items[Parent(idx)]) > 0)
            {
                Swap(idx, Parent(idx));
                idx = Parent(idx);
            }

            _count++;
        }
        private void GrowBackingArray()
        {
            T[] newItems = new T[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                newItems[i] = _items[i];
            }
            _items = newItems;
        }

        public T Peek()
        {
            if (Count > 0)
            {
                return _items[0];
            }
            throw new InvalidOperationException();
        }

        public T RemoveMax()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException();
            }
            T max = _items[0];
            _items[0] = _items[_count - 1];
            _count--;
            int index = 0;
            while (index < _count)
            {
                int left = (2 * index) + 1;
                int right = (2 * index) + 2;
                if (left >= _count)
                {
                    break;
                }
                     int maxChildIndex = IndexOfMaxChild(left, right);
                if (_items[index].CompareTo(_items[maxChildIndex]) > 0)
                {
                    break;
                }
                Swap(index, maxChildIndex);
                index = maxChildIndex;
            }
            return max;
        }

        private int IndexOfMaxChild(int left, int right)
        {
            int maxChildIndex = -1;
            if (right >= _count)
            {
                maxChildIndex = left;
            }
            else
            {
                if (_items[left].CompareTo(_items[right]) > 0)
                {
                    maxChildIndex = left;
                }
                else
                {
                    maxChildIndex = right;
                }
            }
            return maxChildIndex;
        }


        public int Count { get { return _count; } }

        public IEnumerator<T> GetEnumerator() => _items.Take(_count).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        private int ChildLeft(int index)
        {
            return 2 * index + 1;
        }
        private int ChildRight(int index)
        {
            return 2 * index + 2;
        }


        public void Clear()
        {
            _count = 0;
            _items = new T[DEFAULT_LENGTH];
        }

        private int Parent(int index)
        {
            return (index - 1) / 2;
        }
        private void Swap(int left, int right)
        {
            T temp = _items[left];
            _items[left] = _items[right];
            _items[right] = temp;
        }

    }
}
