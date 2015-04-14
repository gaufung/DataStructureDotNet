using System;
using Bst.AVL;
using NUnit.Framework;
using Bst;
using Tree;

namespace Test
{
    [TestFixture]
    class TestBst
    {
        [Test]
        public void Test()
        {
            var bst=new Avl<int>();
            bst.Insert(1);
           
          //  Console.WriteLine(bst.Root.BalFac());
            bst.Insert(2);
            
          //  Console.WriteLine(bst.Root.BalFac());
            bst.Insert(3);
            Console.WriteLine(bst.Root.Height);
            Console.WriteLine(bst.Root.RChild.Height);
            Console.WriteLine(bst.Root.LChild.Height);
         //   bst.Remove(3);
         //   Console.WriteLine(bst.Root.Height);
            //}
          //  Console.WriteLine(bst.Root.LChild.Data);
           // bst.TravIn(new Action<int>(i => Console.WriteLine(i)));

        }
    }
}