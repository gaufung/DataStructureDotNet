using System;
using NUnit.Framework;
using Sequence;

namespace BinaryTreeTest
{
    [TestFixture]
    public class TestRun
    {
        private readonly BinTree<int> _tree;
        public TestRun()
        {
            _tree=new BinTree<int>();
        }
        [Test]
        public void TestEmptyTree()
        {
            Assert.AreEqual(true,_tree.Empty);
            Assert.AreEqual(0,_tree.Size);
            Assert.AreEqual(null,_tree.Root);
        }

        [Test]
        public void TestInsertAsRoot()
        {
            _tree.InsertAsRoot(2);
            Assert.AreEqual(2,_tree.Root.Data);
            Assert.AreEqual(1,_tree.Size);
            Assert.AreEqual(false,_tree.Empty);
        }

        [Test]
        public void TestInsertChild()
        {
            _tree.InsertAsRoot(0);
            var lc=_tree.InsertAsLc(_tree.Root, 1);
            var rc=_tree.InsertAsRc(_tree.Root, 2);
            Assert.AreEqual(lc.Data,1);
            Assert.AreEqual(rc.Data,2);
            Assert.AreEqual(_tree.Size,3);
        }
        [Test]
        public void TestAttachChild()
        {
            _tree.InsertAsRoot(0);
            _tree.InsertAsLc(_tree.Root, 1);
            _tree.InsertAsRc(_tree.Root, 2);
            //new tree
            var newTree=new BinTree<int>();
            newTree.InsertAsRoot(5);
            _tree.AttachAsLc(_tree.Root, newTree);
            Assert.AreEqual(_tree.Size,3);
            Assert.AreEqual(_tree.Root.LChild.Data,5);
        }

        [Test]
        public void TestRemove()
        {
            _tree.InsertAsRoot(0);
            _tree.InsertAsLc(_tree.Root, 1);
            _tree.InsertAsLc(_tree.Root.LChild, 2);
            int size = _tree.Remove(_tree.Root.LChild);
            Assert.AreEqual(size,2);
            Assert.AreEqual(_tree.Root.LChild,null);
        }

        [Test]
        public void TestSecede()
        {
            _tree.InsertAsRoot(0);
            _tree.InsertAsLc(_tree.Root, 1);
            _tree.InsertAsRc(_tree.Root, 2);
            _tree.InsertAsLc(_tree.Root.RChild, 3);
            var newTree = _tree.Secede(_tree.Root.RChild);
            Assert.AreEqual(2,newTree.Size);
            Assert.AreEqual(2,newTree.Root.Data);
        }

        [Test]
        public void TestTraverse()
        {
            _tree.InsertAsRoot(0);
            _tree.InsertAsLc(_tree.Root, 1);
            _tree.InsertAsRc(_tree.Root, 2);
            _tree.InsertAsLc(_tree.Root.RChild, 3);
            _tree.TravPre_I2(_tree.Root,Cout);
            Console.WriteLine();
            _tree.TravIn_I2(_tree.Root,Cout);
        }
        private void Cout(BinNode<int> value)
        {
            Console.Write(value.Data+"\t");
        }
    }
}
