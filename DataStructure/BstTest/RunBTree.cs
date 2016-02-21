using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sequence.BTree;

namespace BstTest
{
    [TestFixture]
    public class RunBTree
    {
        private BTree<int> _bst;

        public RunBTree()
        {
            _bst=new BTree<int>();
            InitTree();
        }
        public void InitTree()
        {
            _bst.Insert(4);
            _bst.Insert(7);
            _bst.Insert(10);
            _bst.Insert(9);
            _bst.Insert(12);
            
        }
        [Test]
        public void RunSearch()
        {
            Assert.AreEqual(null,_bst.Search(3));
            Assert.AreEqual(_bst.Root.Child[0],_bst.Search(4));
        }
    }
}
