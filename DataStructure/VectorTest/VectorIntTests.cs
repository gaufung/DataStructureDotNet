using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
