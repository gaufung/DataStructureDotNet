using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Sequence;

namespace VectorTests
{
    [TestFixture]
    public class VectorIntTests
    {
        private IVector<int> vector;

        [SetUp]
        public void Init()
        {
            this.vector = VectorFactory<int>.Create();
        }

        [Test]
        public void TestEmpty()
        {
            Assert.AreEqual(0, this.vector.Size);
            Assert.AreEqual(true, this.vector.Empty);
        }

        [Test]
        public void TestInsert()
        {
            int[] values = new[] { 0, 4, 9, -1, int.MaxValue, int.MinValue };
            InsertToVector(values);
            Assert.AreEqual(6, this.vector.Size);
            for(int i=0; i < values.Length; i++)
            {
                Assert.AreEqual(values[i], this.vector[i]);
            }
            this.vector.Insert(4, 8);
            Assert.AreEqual(7, this.vector.Size);
            Assert.AreEqual(8, this.vector[4]);

        }

        private void InsertToVector(params int[] args)
        {
            foreach (var arg in args)
            {
                this.vector.Insert(arg);
            }
        }

        [Test]
        public void TestFind()
        {
            int[] values = new[] { 0, 4, 9, -1, int.MaxValue, int.MinValue, 9 };
            InsertToVector(values);
            Assert.AreEqual(1, this.vector.Find(4));
            Assert.AreEqual(-1, this.vector.Find(100));
            Assert.AreEqual(6, this.vector.Find(9));
            Assert.AreEqual(-1, this.vector.Find(int.MaxValue, 0, 3));
            Assert.AreEqual(2, this.vector.Find(9, 0, 4));
        }

        [Test]
        public void TestSearch()
        {
            int[] values = new[] { int.MinValue, -100, 10, 0, 99, 4901, int.MaxValue };
            InsertToVector(values);
            Assert.AreEqual(0, this.vector.Search(int.MinValue));
            Assert.AreEqual(4, this.vector.Search(99));
            Assert.AreEqual(-1, this.vector.Search(-92));
        }

        [Test]
        public void TestRemove()
        {
            int[] values = new[] { 0, 4, 9, -1, int.MaxValue, int.MinValue, 9 };
            InsertToVector(values);
            Assert.AreEqual(7, this.vector.Size);
            Assert.AreEqual(4, this.vector.Remove(1));
            Assert.AreEqual(6, this.vector.Size);
            Assert.AreEqual(2, this.vector.Remove(0, 2));
            Assert.AreEqual(4, this.vector.Size);
        }

        [Test]
        public void TestDisorder()
        {
            int[] values = new[] { 0, 4, 9, -1, int.MaxValue, int.MinValue, 9 };
            InsertToVector(values);
            Assert.AreEqual(9, this.vector.DisOrdered());
        }

        [Test]
        public void TestDuplicate()
        {
            int[] values = new[] { 10, 9, 9, int.MaxValue, -1, 8 };
            InsertToVector(values);
            Assert.AreEqual(1, this.vector.Deduplicate());
            Assert.AreEqual(int.MaxValue, this.vector[2]);
        }

        [Test]
        public void TestUniquey()
        {
            int[] values = new[] { -10, 9, 9, 10 };
            InsertToVector(values);
            Assert.AreEqual(1, this.vector.Deduplicate());
            Assert.AreEqual(10, this.vector[2]);
        }

        [Test]
        public void TestCompareTo()
        {
            IVector<int> smaller = VectorFactory<int>.Create(new[] { 10 }, 1);
            int[] values = new[] { -10, 9, 9, 10 };
            InsertToVector(values);
            Assert.AreEqual(1, this.vector.CompareTo(smaller));
            IVector<int> equal = VectorFactory<int>.Create(new[] { 1, 2, 3, 4 }, 4);
            Assert.AreEqual(0, this.vector.CompareTo(equal));
            IVector<int> bigger = VectorFactory<int>.Create(new[] { 1, 2, 3, 4, 5 }, 5);
            Assert.AreEqual(-1, this.vector.CompareTo(bigger));
        }

        [Test]
        public void TestAny()
        {
            int[] values = new[] { 10, 9, 9, int.MaxValue, -1, 8 };
            InsertToVector(values);
            Assert.IsTrue(this.vector.Any(i => i % 2 == 0));
            Assert.IsFalse(this.vector.Any(i => i < -5));
        }

        [Test]
        public void TestFirstOrDefault()
        {
            int[] values = new[] { 10, 9, 9, int.MaxValue, -1, 6 };
            InsertToVector(values);
            Assert.AreEqual(9, this.vector.FirstOrDefault(i => i % 3 == 0));
            Assert.AreEqual(0, this.vector.FirstOrDefault(i => i == 23));
        }

        [Test]
        public void TestFirst()
        {
            int[] values = new[] { 10, 9, 9, int.MaxValue, -1, 6 };
            InsertToVector(values);
            Assert.AreEqual(9, this.vector.First(i => i % 3 == 0));
        }
        
        [Test]
        public void TestFirstNotExist()
        {
            int[] values = new[] { 10, 9, 9, int.MaxValue, -1, 6 };
            InsertToVector(values);
            Assert.Throws<InvalidOperationException>(() => this.vector.First(i => i < -3));
        }

        [Test]
        public void TestBinarySearch()
        {
            int[] values = new[] { -10, -2, 8, 9, 10, 20, int.MaxValue };
            InsertToVector(values);
            Assert.AreEqual(3, this.vector.Search(9));
        }
    }
}
