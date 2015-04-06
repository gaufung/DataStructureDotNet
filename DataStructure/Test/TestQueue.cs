using System;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Queue;
namespace Test
{
    [TestFixture]
    class TestQueue
    {
        [Test]
        public void Test()
        {
            Queue<int> queue=new Queue<int>();
            queue.Enqueue(2);
            queue.Enqueue(3);
            Console.WriteLine(queue.Empty);
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Front);
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Empty);
            
        }
    }
}
