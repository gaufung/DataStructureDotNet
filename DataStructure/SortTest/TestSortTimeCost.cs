using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sequence;

namespace SortTest
{
    [TestFixture]
    public class TestSortTimeCost
    {
        private int[] _nums;

        public TestSortTimeCost()
        {
            _nums = new int[100000];
            Random random=new Random();
            for (int i = 0; i < 100000; i++)
            {
                _nums[i] = random.Next();
            }
        }

        [Test]
        public void TestQuickSort()
        {
            Stopwatch watch=new Stopwatch();
            watch.Start();
            Sort<int>.QuickSort(_nums);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        [Test]
        public void TestBubbleSort()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Sort<int>.BubbleSort(_nums);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
    }
}
