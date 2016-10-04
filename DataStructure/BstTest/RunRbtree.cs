using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sequence;
using Sequence.RB;

namespace BstTest
{
    [TestFixture]
    public class RunRbtree
    {
        RBTree<int> tree;

        public RunRbtree()
        {
            tree=new RBTree<int>();
        }

        [Test]
        public void Run()
        {
            tree.Insert(1);
            Assert.AreEqual(1,tree.Root.Data);
            Assert.AreEqual(RbColor.RbBlack,tree.Root.Color);
            tree.Insert(2);
            Assert.AreEqual(RbColor.RbRed,tree.Root.RChild.Color);
            tree.Insert(3);
            Assert.AreEqual(2,tree.Root.Data);
            Assert.AreEqual(RbColor.RbBlack,tree.Root.Color);
            Assert.AreEqual(RbColor.RbRed, tree.Root.RChild.Color);
            Assert.AreEqual(RbColor.RbRed, tree.Root.LChild.Color);
        }

        [Test]
        public void RunInsert()
        {
            for (int i = 0; i < 10; i++)
            {
                tree.Insert(i);
            }
        }
    }
}
