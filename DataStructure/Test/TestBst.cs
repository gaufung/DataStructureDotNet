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
            bst.Insert(2);
            bst.Insert(3);
            bst.Insert(4);
            bst.Remove(1);
            Console.WriteLine(bst.Root.Data);
            bst.TravIn(new Action<int>(i => Console.WriteLine(i)));
            /*
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
           
             */

        }
    }
}