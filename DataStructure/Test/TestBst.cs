using System;
using System.Collections.Generic;
using System.Globalization;
using Bst.AVL;
using Bst.BTree;
using Bst.RB;
using Bst.SplayTree;
using NUnit.Framework;


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
           /* var bst = new Splay<int>();
            bst.Insert(4);         
            bst.Insert(8);
            bst.Insert(6);
            Console.WriteLine(bst.Root.Data);
            Console.WriteLine(bst.Root.LChild.Data);
            Console.WriteLine(bst.Root.RChild.Data);
            */
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
            //BTree<int> bt=new BTree<int>();
            //bt.Insert(2);
            //bt.Insert(3);
            //bt.Insert(5);
            //bt.Insert(6);
            //bt.Remove(5);
         //  bt.Root.Key.ForEach(new Action<int>(i => Console.WriteLine(i)));
           // Console.WriteLine(bt.Root.Key[0]);
            var bt = new RedBlack<int>();
            bt.Insert(1);
            bt.Insert(2);
            bt.Insert(3);
            bt.Insert(0);
            bt.Remove(3);
            Console.WriteLine(bt.Root.Data);
            Console.WriteLine(bt.Root.Color);

        }
    }
    
}