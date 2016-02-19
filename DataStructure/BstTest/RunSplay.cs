using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Sequence;
using Sequence.SplayTree;

namespace BstTest
{
    [TestFixture]
    public class RunSplay
    {

        private Bst<int> _bst;

        public RunSplay()
        {
            _bst=new SplayTree<int>();
            InitTree();
        }

        private void InitTree()
        {
            _bst.Insert(3);
            _bst.Insert(2);
            _bst.Insert(8);
        }

        [Test]
        public void TestSplay()
        {
            Assert.AreEqual(8,_bst.Root.Data);
            Assert.AreEqual(3,_bst.Root.LChild.Data);
            Assert.AreEqual(2,_bst.Root.LChild.LChild.Data);
        }

        [Test]
        public void TestSearch()
        {
            _bst.Search(2);
            Assert.AreEqual(2,_bst.Root.Data);
        }

        [Test]
        public void TestRemove()
        {
            _bst.Remove(3);
            Assert.AreEqual(8,_bst.Root.Data);
        }
    }
}
