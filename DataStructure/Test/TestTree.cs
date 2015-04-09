using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tree;
namespace Test
{
    [TestFixture]
    public class TestTree
    {
        [Test]
        public void Test()
        {
           BinTree<int> tree=new BinTree<int>();
           //Assert.AreEqual(1,tree.Size);
           tree.InsertAsRoot(2);
           tree.InsertAsLc(tree.Root, 1);
           tree.InsertAsRc(tree.Root, 3);
           tree.TravIn_I1(tree.Root,new Action<int>(i=>Console.WriteLine(i)));
        }
    }
}
