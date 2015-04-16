using System;
using System.Globalization;
using Bst.AVL;
using Bst.SplayTree;
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
            /*
            var bst=new Avl<int>();
            bst.Insert(1);
            bst.Insert(2);
            bst.Insert(3);
            bst.Insert(4);
            bst.Remove(1);
            Console.WriteLine(bst.Root.Data);
            bst.TravIn(new Action<int>(i => Console.WriteLine(i)));
             */
            var bst = new Splay<int>();
            bst.Insert(4);         
            bst.Insert(8);
            bst.Insert(6);
            Console.WriteLine(bst.Root.Data);
            Console.WriteLine(bst.Root.LChild.Data);
            Console.WriteLine(bst.Root.RChild.Data);
            //Console.WriteLine(bst.Root.Data);
            //Console.WriteLine(bst.Root.LChild.Data);
            //Console.WriteLine(bst.Root.LChild.RChild.Data);
            //Console.WriteLine(bst.Root.LChild.RChild.LChild.Data);
            //Console.WriteLine(bst.Root.LChild.LChild.Data);
            //Console.WriteLine(bst.Root.RChild.Data);
          //  Console.WriteLine(bst.Root.RChild.Data);
          //  Console.WriteLine(bst.Root.RChild.RChild.Data);
            //Console.WriteLine(bst.Root.RChild.RChild.RChild.Data);
            //Console.WriteLine(bst.Root.LChild.Data);
            //Console.WriteLine(bst.Root.LChild.LChild.Data);
           // Console.WriteLine();
            //bst.Insert(3);
           // Console.WriteLine(x.Data);
          //  bst.Insert(1);
           // Console.WriteLine(bst.Root.Data);
            //Console.WriteLine(bst.Root.RChild.Data);
            //Console.WriteLine(bst.Root.RChild.RChild.Data);
        //  bst.TravIn(new Action<int>(i => Console.WriteLine(i)));


        }
    }
}