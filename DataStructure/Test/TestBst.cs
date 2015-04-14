using System;
using Bst.AVL;
using NUnit.Framework;
using Bst;
namespace Test
{
    [TestFixture]
    class TestBst
    {
        [Test]
        public void Test()
        {
            var bst=new Avl<int>();
           bst.Insert(3);
           bst.Insert(5);
           bst.Insert(1);
            bst.Insert(6);
            bst.Insert(7);
            bst.Insert(8);
           bst.TravIn(new Action<int>(i => Console.WriteLine(i)));

        }
    }
}