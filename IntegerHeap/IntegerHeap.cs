using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    /// <summary>
    /// Integer Heap by Steve Stermer. 
    /// Integer heap is held as an array. Left child is indexed as 2i + 1, right child is indexed as 2i + 2.
    /// </summary>
    public class IntegerHeap
    {
        public enum SortType
        {
            MIN,
            MAX
        }

        private SortType m_ActiveSort;
        private int[] m_Heap = new int[100];
        public int m_Size;

        public IntegerHeap(SortType activeSort)
        {
            m_ActiveSort = activeSort;
        }

        private void ResizeHeap()
        {
            if (m_Size * 2 + 2 >= m_Heap.Length)
            {
                int[] newHeap = new int[m_Heap.Length * 2];
                Array.Copy(m_Heap, newHeap, m_Size);
                m_Heap = newHeap;
            }
        }

        public void Insert(int number)
        {
            ResizeHeap();
            m_Heap[m_Size] = number;
            BubbleUp(m_Size);
            m_Size++;
        }

        private void SwapAToB(int indexA, int indexB)
        {
            int tempVal = m_Heap[indexA];
            m_Heap[indexA] = m_Heap[indexB];
            m_Heap[indexB] = tempVal;
        }

        //When inserting a new number, we 'bubble up' to find its right place.
        private void BubbleUp(int index)
        {
            if (index == 0) return;

            int parentOffset = index % 2 == 0 ? parentOffset = 2 : 1; //Was a 'right' node it was even, else, it was a left.
            bool swapIndices = false;

            switch (m_ActiveSort)
            {
                case SortType.MAX:
                    if (m_Heap[index] > m_Heap[(index - parentOffset) / 2]) swapIndices = true; //If greater than its parent, setup swap.
                    break;
                case SortType.MIN:
                    if (m_Heap[index] < m_Heap[(index - parentOffset) / 2]) swapIndices = true; //If less than its parent, setup swap.
                    break;
            }

            if (swapIndices)
            {
                SwapAToB((index - parentOffset) / 2, index);
                BubbleUp((index - parentOffset) / 2);
            }
        }

        //If we pop the root, we place the last added number in the root, and 'bubble down' to find its right place
        private void BubbleDown(int index)
        {
            if (index >= m_Size - 1) return; //No where to bubble.

            bool swapLeft = false;
            bool swapRight = false;

            switch (m_ActiveSort)
            {
                case SortType.MAX:
                    if (2 * index + 1 < m_Size && m_Heap[index] < m_Heap[2 * index + 1]) //if less than left child, swap. Check if child is out of bounds.
                    {
                        SwapAToB(index, 2 * index + 1);
                        swapLeft = true;
                    }
                    if (2 * index + 2 < m_Size && m_Heap[index] < m_Heap[2 * index + 2]) //if less than right child, swap. Chick if child is out of bounds.
                    {
                        SwapAToB(index, 2 * index + 2);
                        swapRight = true;
                    }
                    break;
                case SortType.MIN:
                    if (2 * index + 1 < m_Size && m_Heap[index] > m_Heap[2 * index + 1]) //if greater than left child, swap. Check if child is out of bounds.
                    {
                        SwapAToB(index, 2 * index + 1);
                        swapLeft = true;
                    }
                    if (2 * index + 2 < m_Size && m_Heap[index] > m_Heap[2 * index + 2]) //if greater than right child, swap. Check if child is out of bounds.
                    {
                        SwapAToB(index, 2 * index + 2);
                        swapRight = true;
                    }
                    break;
            }

            if (swapRight) BubbleDown(2 * index + 2); //If a swap was performed, continue the bubbling down.
            if (swapLeft) BubbleDown(2 * index + 1); //Since we could have swapped both sides above, we most now perform on both sides.
        }

        public int Peek()
        {
            if (m_Size != 0) return m_Heap[0];
            return int.MinValue;
        }

        public int Pop()
        {
            if (m_Size != 0)
            {
                int rootValue = m_Heap[0];
                m_Heap[0] = m_Heap[m_Size - 1];
                m_Size--;

                BubbleDown(0);
                return rootValue;

            }

            return int.MinValue;
        }
    } //end Integer Heap
}
