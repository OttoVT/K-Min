using System;
using System.Collections.Generic;
using System.Text;

namespace Kmin
{
    public class MaxHeap
    {
        private int _count;
        private readonly int[] _array;

        public MaxHeap(int count)
        {
            _array = new int[count];
        }

        private int GetLeftChildIndex(int elementIndex) => 2 * elementIndex + 1;
        private int GetRightChildIndex(int elementIndex) => 2 * elementIndex + 2;
        private int GetParentIndex(int elementIndex) => (elementIndex - 1) / 2;

        private int GetLeftChild(int elementIndex) => _array[GetLeftChildIndex(elementIndex)];
        private int GetRightChild(int elementIndex) => _array[GetRightChildIndex(elementIndex)];
        private int GetParent(int elementIndex) => _array[GetParentIndex(elementIndex)];

        private bool HasLeftChild(int elementIndex) => GetLeftChildIndex(elementIndex) < _count;
        private bool HasRightChild(int elementIndex) => GetRightChildIndex(elementIndex) < _count;
        private bool IsRoot(int elementIndex) => elementIndex == 0;

        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _array[firstIndex];
            _array[firstIndex] = _array[secondIndex];
            _array[secondIndex] = temp;
        }

        public int[] GetArray()
        {
            return _array;
        }

        public int Peek()
        {
            if (_count == 0)
                throw new IndexOutOfRangeException();

            return _array[0];
        }

        public int Pop()
        {
            if (_count == 0)
                throw new IndexOutOfRangeException();

            var result = _array[0];
            _array[0] = _array[_count - 1];
            _count--;

            ReCalculateDown();

            return result;
        }

        public void Add(int element)
        {
            if (_count == _array.Length)
                throw new IndexOutOfRangeException();

            _array[_count] = element;
            _count++;

            ReCalculateUp();
        }

        private void ReCalculateUp()
        {
            var index = _count - 1;
            while (!IsRoot(index) && _array[index] > GetParent(index))
            {
                var parentIndex = GetParentIndex(index);
                Swap(parentIndex, index);
                index = parentIndex;
            }
        }

        private void ReCalculateDown()
        {
            int index = 0;
            while (HasLeftChild(index))
            {
                var biggerIndex = GetLeftChildIndex(index);
                if (HasRightChild(index) && GetRightChild(index) > GetLeftChild(index))
                {
                    biggerIndex = GetRightChildIndex(index);
                }

                if (_array[biggerIndex] < _array[index])
                {
                    break;
                }

                Swap(biggerIndex, index);
                index = biggerIndex;
            }
        }
    }
}
