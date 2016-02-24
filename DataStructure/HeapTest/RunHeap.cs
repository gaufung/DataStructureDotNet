using NUnit.Framework;
using Sequence;

namespace HeapTest
{
    [TestFixture]
    public class RunHeap
    {
        private IPriorityQueue<int> _heap;

        public RunHeap()
        {
            _heap=new ComplHeap<int>();
            InitHeap();
        }

        private void InitHeap()
        {
            _heap.Insert(5);
            _heap.Insert(8);
            _heap.Insert(3);
        }

        [Test]
        public void TestInsert()
        {
            _heap.Insert(10);
            Assert.AreEqual(10,_heap.GetMax());
        }

        [Test]
        public void TestDelMax()
        {
            _heap.Insert(7);
            Assert.AreEqual(8,_heap.GetMax());
            _heap.DelMax();
            Assert.AreEqual(7,_heap.GetMax());
        }

        [Test]
        public void TestHeapify()
        {
            int[] data=new int[]
            {
                4,8,2,3,9,8,10
            };
            _heap=new ComplHeap<int>(data);
            Assert.AreEqual(10,_heap.GetMax());
        }
    }
}
