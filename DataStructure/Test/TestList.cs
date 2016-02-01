using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NUnit.Framework;
using List;
namespace Test
{

    [TestFixture]
    public class TestList
    {
        [Test]
        public void TestDemo()
        {
            List.List<int> list=new List.List<int>();
            //size,empty
          //  Assert.AreEqual(0,list.Size);
          //  Assert.AreEqual(false,list.Empty);
            list.InsertAsFirst(3);
            list.InsertAsFirst(2);
            list.InsertAsFirst(1);
            list.InsertAsFirst(1);
            list.InsertAsLast(5);
         //  Assert.AreEqual(4, list.Size);
            //Console.WriteLine(list[0]);
            //Console.WriteLine("first:"+list.First);
            //Console.WriteLine("last ："+list.Last);
            //list.InsertAsBefore(list.First, 10);
            //Console.WriteLine("new first:"+list.First);
            //list.InsertAsAfter(list.Last, 22);
            //Console.WriteLine("new Last: "+list.Last);
            //list.Remove(list.First);
            //Console.WriteLine("remove fisrt:"+list.First);
           // list.Traverse(new Action<int>(i=>Console.WriteLine(i)));
           // Console.WriteLine(list.DisOrder());
           // list.Traverse(new Action<int>(i => Console.WriteLine(i)));
            //var p = list.Find(4);
            //if (p==null)
            //{
            //    Console.WriteLine("null");
            //}
            //else {
            //    Console.WriteLine(p);
            //}
            list.Traverse(new Action<int>(i => Console.WriteLine(i)));
            list.Deduplicate();
            list.Traverse(new Action<int>(i=>Console.WriteLine(i)));
            //var p = list.Search(5);
            //if (p == null)
            //{
            //    Console.WriteLine("null");
            //}
            //else
            //{
            //    Console.WriteLine(p);
            //}
        }
    }
}
