using System;
using NUnit.Framework;
using Sequence;

namespace VectorTest
{
    [TestFixture]
    public class TestRun
    {
        private readonly IVector<int> _vector; 
        public TestRun()
        {
            _vector = Vector<int>.VectorFactory();
        }
        [Test]
        public void TestSizeEmpty()
        {
            Assert.AreEqual(_vector.Empty,true);
            Assert.AreEqual(_vector.Size,0);
        }

        [Test]
        public void TestObjectIndex()
        {
            InsertVector();
            Assert.AreEqual(_vector[0],4);
         
        }

        [Test]
        public void TestDisOrder()
        {
            InsertVector();
            Assert.AreEqual(_vector.DisOrdered(),0);
            _vector[0] = 6;
            _vector[1] = 4;
            _vector[2] = 3;
            Assert.AreEqual(_vector.DisOrdered(),3);
        }

        private void InsertVector()
        {
            _vector.Insert(4);
            _vector.Insert(5);
            _vector.Insert(6);
        }

        private void Cout(int i)
        {
            Console.Write(i+"\t");
        }

        [Test]
        public void TestTraverse()
        {
            InsertVector();
            _vector.Traverse(Cout);
        }

        [Test]
        public void TestDeduplicate()
        {
            _vector.Insert(4);
            _vector.Insert(3);
            _vector.Insert(4);
            _vector.Insert(7);
            _vector.Insert(7);
            _vector.Insert(8);
            _vector.Traverse(Cout);
            Console.WriteLine();
            _vector.Deduplicate();
            _vector.Traverse(Cout);
        }

        [Test]
        public void TestUniquify()
        {
            _vector.Insert(3);
            _vector.Insert(3);
            _vector.Insert(4);
            _vector.Insert(7);
            _vector.Insert(7);
            _vector.Insert(7);
            _vector.Insert(8);
            _vector.Traverse(Cout);
            Console.WriteLine();
            _vector.Uniquify();
            _vector.Traverse(Cout);
        }
    }

    [TestFixture]
    public class TestUdtRun
    {
        private readonly IVector<Coordinate> _vector;

        public TestUdtRun()
        {
            _vector = Vector<Coordinate>.VectorFactory();
        }
        [Test]
        public void TestFind()
        {          
            _vector.Insert(new Coordinate() {X = 0, Y = 0});
            Assert.AreEqual(_vector.Find(new Coordinate() {X = 0, Y = 0}),0);
        }
    }
}
