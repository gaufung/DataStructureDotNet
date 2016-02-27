using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sequence;

namespace SortTest
{
    [TestFixture]
    public class RunKth
    {
        private int[] nums;

        public RunKth()
        {
            nums=new int[]
            {
                2,3,1,1,1,1,1,1,15,3,5,4,3,2,3,13,234,23412,4323,23143,
            };
        }

        [Test]
        public void TestMaj()
        {
            int maj = 5;
            bool res=Sort<int>.Majority(nums, ref maj);
           // Assert.AreEqual(maj, 1);
            Assert.AreEqual(false, res);
        }
    }
}
