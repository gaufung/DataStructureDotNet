using System;
using NUnit.Framework;
using Sequence;

namespace ListTest
{
    [TestFixture]
    public class TestRun
    {
        private readonly IList<int> _list; 
        public TestRun()
        {
            _list = List<int>.ListFactory();
        }

        [Test]
        public void TestSize()
        {
            Assert.AreEqual(_list.Size,0);
            Assert.AreEqual(_list.Empty,true);
            _list.InsertAsFirst(4);
            _list.InsertAsLast(5);
            Assert.AreEqual(_list.Empty,false);
            Assert.AreEqual(_list.Size,2);
        }
        [Test]
        public void TestFristAndLast()
        {
            _list.InsertAsFirst(2);
            _list.InsertAsFirst(3);
            _list.InsertAsLast(6);
            Assert.AreEqual(_list.First.Data,3);
            Assert.AreEqual(_list.Last.Data, 6);
        }
        [Test]
        public void TestAsBeforeAndAfter()
        {
            _list.InsertAsFirst(3);
            _list.InsertAsAfter(_list.Last, 4);
            _list.InsertAsBefore(_list.First, 6);
            Assert.AreEqual(_list.Last.Data,4);
            Assert.AreEqual(_list.First.Data,6);
        }
        [Test]
        public void TestDisOrder()
        {
            _list.InsertAsLast(5);
            _list.InsertAsLast(4);
            _list.InsertAsLast(2);
            Assert.AreEqual(_list.DisOrder(),3);
        }

        [Test]
        public void TestFind()
        {
            _list.InsertAsLast(5);
            _list.InsertAsLast(4);
            _list.InsertAsLast(2);
            Assert.AreEqual(_list.Find(4).Data,4);
            Assert.AreEqual(_list.Find(6),null);
        }
        [Test]
        public void TestSearch()
        {
            _list.InsertAsLast(3);
            _list.InsertAsLast(4);
            _list.InsertAsLast(6);
            _list.InsertAsLast(7);
            _list.InsertAsLast(8);
            _list.InsertAsLast(12);
            Assert.AreEqual(_list.Search(5).Data,4);
            Assert.AreEqual(_list.Search(8).Data,8);
        }
        [Test]
        public void TestDeduplicate()
        {
            _list.InsertAsLast(5);
            _list.InsertAsLast(2);
            _list.InsertAsLast(2);
            _list.InsertAsLast(4);
            _list.InsertAsLast(5);
            _list.InsertAsLast(4);
            _list.InsertAsLast(5);
            _list.InsertAsLast(2);
            _list.Traverse(Cout);
            _list.Deduplicate();
            Console.WriteLine();
            _list.Traverse(Cout);
        }
        [Test]
        public void TestUniquify()
        {
            _list.InsertAsLast(3);
            _list.InsertAsLast(3);

            _list.InsertAsLast(4);
            _list.InsertAsLast(6);
            _list.InsertAsLast(7);
            _list.InsertAsLast(7);
            _list.InsertAsLast(7);
            _list.InsertAsLast(8);
            _list.InsertAsLast(8);
            _list.InsertAsLast(8);
            _list.InsertAsLast(12);
            _list.Traverse(Cout);
            _list.Uniquify();
            Console.WriteLine();
            _list.Traverse(Cout);
        }
        private void Cout(int value)
        {
            Console.Write(value+"\t");
        }
    }
}
