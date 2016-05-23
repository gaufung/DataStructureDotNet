using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Sequence;
using Sequence.GraphList;
using Sequence.GraphMatrix;

namespace GraphTest
{
    [TestFixture]
    public class TestRun
    {
        private Graph<Char, int, Double> graph;

        public TestRun()
        {
            /*Graphmatrix*/
            graph = GraphMatrix<char, int, double>.GraphFactory();
            
            /*GraphList*/
           //graph=GraphList<char, int, double>.GraphFactory();

            graph.Insert('a');
            graph.Insert('b');
            graph.Insert('c');
            graph.Insert('d');
            graph.Insert(1, 0, 1, 2.0);
            graph.Insert(1, 1, 2, 1.0);
            graph.Insert(1, 2, 3, 3.0);
            graph.Insert(1, 1, 3, 1.5);
        }

        [Test]
        public void TestNE()
        {
           Assert.AreEqual(graph.E,4);
           Assert.AreEqual(graph.N,4);
        }

        [Test]
        public void TestVertex()
        {
            Assert.AreEqual(graph.Vertex(0),'a');
            Assert.AreEqual(graph.Vertex(1), 'b');
            Assert.AreEqual(graph.Vertex(2), 'c');
            Assert.AreEqual(graph.Vertex(3), 'd');
            graph.Vertex(0, 'e');
            Assert.AreEqual(graph.Vertex(0),'e');
            Assert.AreEqual(2, graph.InDegree(3));
            Assert.AreEqual(0, graph.OutDegree(3));
            Assert.AreEqual(1, graph.InDegree(2));
            Assert.AreEqual(1, graph.OutDegree(2));
            Assert.AreEqual(1, graph.InDegree(1));
            Assert.AreEqual(2, graph.OutDegree(1));
            Assert.AreEqual(0, graph.InDegree(0));
            Assert.AreEqual(1, graph.OutDegree(0));
            ////
            Assert.AreEqual(1, graph.FirstNbr(0));
            Assert.AreEqual(-1, graph.NextNbr(0, 1));
            Assert.AreEqual(3, graph.FirstNbr(1));
            Assert.AreEqual(2, graph.NextNbr(1, 3));
            Assert.AreEqual(-1, graph.NextNbr(1, 2));
            ////
            Assert.AreEqual(VStatus.Undiscovered, graph.Status(0));
            Assert.AreEqual(VStatus.Undiscovered, graph.Status(1));
            Assert.AreEqual(VStatus.Undiscovered, graph.Status(2));
            Assert.AreEqual(VStatus.Undiscovered, graph.Status(3));


            graph.Remove(2);
            Assert.AreEqual(3,graph.N);
            Assert.AreEqual(2,graph.E);
            Assert.AreEqual(1,graph.FirstNbr(0));
            Assert.AreEqual(2,graph.FirstNbr(1));
        }

        [Test]
        public void TestEdge()
        {
            Assert.AreEqual(true,graph.Exist(0,1));
            Assert.AreEqual(true, graph.Exist(1, 2));
            Assert.AreEqual(true, graph.Exist(1, 3));
            Assert.AreEqual(true, graph.Exist(2, 3));
            //Assert.AreEqual();
            graph.Remove(1, 3);
            Assert.AreEqual(false,graph.Exist(1,3));
        }

        [Test]
        public void GraphBsfTravel()
        {
            graph.Bfs();
            Assert.AreEqual(-1,graph.Parent(0));
            Assert.AreEqual(0,graph.Parent(1));
            Assert.AreEqual(1,graph.Parent(2));
            Assert.AreEqual(1,graph.Parent(3));
        }

        [Test]
        public void GraphDsfTRavel()
        {
            graph.Dfs(1);
        }

        [Test]
        public void GraphTopoTravel()
        {
            var statck=graph.TopoSort(1);
            statck.Foreach(item=>Trace.Write(item));
        }

        [Test]
        public void GraphPrim()
        {
            graph.Prime(0);
        }
    }
}
