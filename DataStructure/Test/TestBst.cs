using System;
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
            Bst<int> bst=new Bst<int>();
           bst.Insert(3);
           bst.Insert(5);
           bst.Insert(1);
           bst.Insert(2);
           bst.Insert(4);
            bst.Remove(3);
             bst.TravIn(new Action<int>(i => Console.WriteLine(i)));

        }
    }
}