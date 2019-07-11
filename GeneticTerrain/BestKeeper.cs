using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticTerrain
{
    public class BestKeeper<T>: IEnumerable
    {
        readonly T[] _items;
        readonly IComparer<T> _comparer;
        int _count;

        class ComparerAdapter : IComparer<T>
        {
            readonly Func<T, T, int> _comparator;

            public ComparerAdapter(Func<T, T, int> comparator)
            {
                _comparator = comparator;
            }

            public int Compare(T x, T y)
            {
                return _comparator(x, y);
            }
        }

        public BestKeeper(int maxCount, Func<T, T, int> comparator = null)
        {
            if (maxCount <= 0) throw new ArgumentException("The max count must be greater than 0.", nameof(maxCount));
            if (comparator == null) _comparer = Comparer<T>.Default;
            else _comparer = new ComparerAdapter(comparator);
            _items = new T[maxCount];
        }

        public bool Add(T candidate)
        {
            if (IsFull)
            {
                if (_comparer.Compare(candidate, _items[0]) < 0) return false;
                AddFromTop(candidate);
                return true;
            }

            AddFromBottom(candidate);
            return true;
        }

        public int Count => _count;

        public IEnumerator<T> GetEnumerator() => _items.Take(_count).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool IsFull => _count == _items.Length;

        void AddFromBottom(T item)
        {
            _items[_count] = item;
            int idx = _count;

            while (idx > 0)
            {
                int fatherIdx = (idx - 1) / 2;
                if (_comparer.Compare( _items[fatherIdx],item) > 0) break;
                Swap(idx, fatherIdx);
                idx = fatherIdx;
            }

            _count++;
        }

        void AddFromTop(T candidate)
        {
            int idx = 0;
            _items[0] = candidate;

            while (true)
            {
                int leftIdx = idx * 2 + 1;
                int rightIdx = idx * 2 + 2;

                int smallestIdx;
                if (leftIdx < _count && _comparer.Compare(_items[leftIdx], candidate) < 0) smallestIdx = leftIdx;
                else smallestIdx = idx;
                if (rightIdx < _count && _comparer.Compare(_items[rightIdx], _items[smallestIdx]) < 0) smallestIdx = rightIdx;

                if (smallestIdx == idx) return;

                Swap(smallestIdx, idx);
                idx = smallestIdx;
            }
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
                if (_comparer.Compare(_items[index], _items[maxChildIndex]) > 0)
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
                if (_comparer.Compare(_items[left], _items[right]) > 0)
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

        void Swap(int idx1, int idx2)
        {
            T item = _items[idx1];
            _items[idx1] = _items[idx2];
            _items[idx2] = item;
        }
        
    }
}
