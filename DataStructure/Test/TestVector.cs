using System;
using NUnit.Framework;
using Vector;
namespace Test
{
    [TestFixture]
    public class TestVector
    {
        [Test]
        public void TestCtor()
        {
            
            Vector.Vector<int> vector=new Vector<int>();
            vector.Insert(1);
            vector.Insert(2);
            vector.Insert(2);
            vector.Insert(3);
            //Console.WriteLine(vector.Size);
            //Console.WriteLine(vector[10]);
            vector.Traverse(new Action<int>(i => Console.WriteLine(i)));
            vector.Uniquify();
            vector.Traverse(new Action<int>(i=>Console.WriteLine(i)));
        }


    }
}
