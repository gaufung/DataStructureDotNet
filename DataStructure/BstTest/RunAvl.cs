using System;
using NUnit.Framework;
using Sequence;
using Sequence.AVL;

namespace BstTest
{
    [TestFixture]
    public class RunAvl
    {
        private readonly Bst<int> _bst;

        public RunAvl()
        {
            _bst=new AvlTree<int>();
            InitAvl();
        }

        private void InitAvl()
        {
            _bst.Insert(2);
            _bst.Insert(4);
            _bst.Insert(8);
        }

        [Test]
        public void TestSearch()
        {
            Assert.AreEqual(4,_bst.Root.Data);
            Assert.AreEqual(2,_bst.Root.LChild.Data);
            Assert.AreEqual(8,_bst.Root.RChild.Data);
        }
        [Test]
        public void TestInsert()
        {
            _bst.Insert(6);
            Assert.AreEqual(6,_bst.Root.RChild.LChild.Data);
            _bst.Insert(5);
            Assert.AreEqual(6,_bst.Root.RChild.Data);
        }

        [Test]
        public void TestRemove()
        {
            _bst.Insert(1);
            _bst.Insert(7);
            _bst.Insert(6);
            _bst.Insert(5);
            _bst.Remove(1);
            Assert.AreEqual(6,_bst.Root.Data);
        }
    }
}
