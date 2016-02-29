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
        private IComparable[] nums;

        public RunKth()
        {
            nums = new IComparable[]
            {
                2,3,1,1,1,1,1,1,15,3,5,4,3,2,3,13,234,23412,4323,23143,
            };
        }

        [Test]
        public void TestMaj()
        {
            IComparable maj = 5;
            bool res=Sort.Majority(nums, ref maj);
           // Assert.AreEqual(maj, 1);
            Assert.AreEqual(false, res);
        }
    }
}
