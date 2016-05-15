using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Sequence;

namespace StackQueueTest
{
    [TestFixture]
    public class StackApplication
    {
        [Test]
        public void TestExpression1()
        {
            ExpressionCalc.Caluc("1+2/1");
            Assert.AreEqual(ExpressionCalc.Reuslt,3);
            Assert.AreEqual(ExpressionCalc.Rpn,"121/+");
        }

        [Test]
        public void TestExpression2()
        {
            ExpressionCalc.Caluc("1+2*(1+3)-3");
            Assert.AreEqual(ExpressionCalc.Reuslt,6);
            Assert.AreEqual(ExpressionCalc.Rpn,"1213+*+3-");
        }

        [Test]
        public void TestQueen()
        {        
           var solution= QueenSolution.PalceQueeu(4);
            for (int i = 0; i < solution.Size; i++)
            {
                Console.WriteLine(solution[i].ToString());
            }
        }

    }
}
