using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticTerrain
{
    public class Heap<T>
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
        //public T Peek();
        public T RemoveMax()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException();
            }
            _items[0] = _items[Count - 1];
            _count--;

            //now bubble up the childs
            int idx = 0;
            while (idx < Count
                && (
                    (_items[idx].CompareTo(_items[ChildLeft(idx)]) > 0)
                    ||
                    (_items[idx].CompareTo(_items[ChildRight(idx)]) > 0)
                    )
                )
            {
                int childIdx = (_items[idx].CompareTo(_items[ChildLeft(idx)]) > 0) ? ChildLeft(idx) : ChildRight(idx);

                Swap(idx, childIdx);
                idx = childIdx;
            }

            return default(T);
        }
        private int ChildLeft(int index)
        {
            return 2 * index + 1;
        }
        private int ChildRight(int index)
        {
            return 2 * index + 2;
        }

        public int Count { get; }

        //public void Clear();

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
