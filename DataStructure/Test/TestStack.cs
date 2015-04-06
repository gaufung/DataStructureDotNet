using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Stack;
namespace Test
{
    [TestFixture]
    class TestStack
    {
        [Test]
        public void Test()
        {
            Stack.Stack<int> stack=new Stack.Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(2);
            stack.Push(2);
            Console.WriteLine(stack.Size);
            Console.WriteLine(stack.Top());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Size);
        }
    }
}
