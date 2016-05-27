using System;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sequence;

namespace StackQueueTest
{
    [TestFixture]
    public class StackTest
    {
        private Stack<int> _stack;

        public StackTest()
        {
           
        }

        [Test]
        public void TestStack()
        {
            _stack = StackVectorImpl<int>.StackFactory();
            _stack.Push(1);
            _stack.Push(2);
            Assert.AreEqual(_stack.Top,2);
        }
    }
}
