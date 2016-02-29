using System;
using NUnit.Framework;
using Sequence;

namespace SortTest
{
    [TestFixture]
    public class TestRun
    {
        private readonly IComparable[] _nums;

        public TestRun()
        {
            _nums = new IComparable[]
            {
                4, 1, 5, 3, 2,6,8,0,13,11,-4,99,43,8
            };
        }

        private void Print()
        {
            foreach (var num in _nums)
            {
                Console.Write(num + "\t");
            }
        }

        [Test]
        public void TestBubbleSort()
        {
            Sort.BubbleSort(_nums);
            Print();
        }

        [Test]
        public void TestSelectionSort()
        {
            Sort.SelectionSort(_nums);
            Print();
        }

        [Test]
        public void TestInsertSort()
        {
            Sort.InsertSort(_nums);
            Print();
        }

        [Test]
        public void TestMergeSort()
        {
            Sort.MergeSort(_nums);
            Print();
        }

        [Test]
        public void TestQuickSort()
        {
            Sort.QuickSort(_nums);
            Print();
        }

        [Test]
        public void TestHeapSort()
        {
            Sort.HeapSort(_nums);
            Print();
        }

        [Test]
        public void TestShellSort()
        {
            Sort.ShellSort(_nums);
            Print();
        }
    }
}
