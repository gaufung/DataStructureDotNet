using System;
using Graph;
using Graph.GraphMatrix;
using NUnit.Framework;
namespace Test
{
    [TestFixture]
    public class TestGraph
    {
        [Test]
        public void Test()
        {
            //Graph.GraphMatrix.Graph<int,int,int> graph=new Graph<int, int, int>();
            //graph.Insert(2);
            //graph.Insert(3);
            //graph.Insert(4);
            //graph.Insert(6);
            //graph.Insert(2, 0, 1, 2);
            //graph.Insert(3, 1, 2, 1);
            //graph.Insert(3, 0, 3, 6);
            //var res = graph.Prim();
            //Console.WriteLine(res.Count);
            //foreach (var primEdge in res)
            //{
            //    Console.WriteLine(primEdge);
            //}
            Action<Graph.GraphMatrix.Graph<int, int>, int, int> bfsAction = (Graph<int, int> g, int s, int w) =>
            {
                if (g.Status(w)==VStatus.Undiscovered)
                {
                    if (g.Priority(w) > g.Priority(s) + 1)
                    {
                        g.Priority(w, g.Priority(s) + 1);
                        g.Parent(w, s);
                    }
                }
            };
            Action<Graph.GraphMatrix.Graph<int, int>, int, int> dfsAction = (Graph<int, int> g, int s, int w) =>
            {
                if (g.Status(w) == VStatus.Undiscovered)
                {
                    if (g.Priority(w) > g.Priority(s) - 1)
                    {
                        g.Priority(w, g.Priority(s) - 1);
                        g.Parent(w, s);
                        return;
                    }
                }
            };
            Graph.GraphMatrix.Graph<int,int> graph=new Graph<int, int>();
            graph.Insert(2);
            graph.Insert(3);
            graph.Insert(4);
            graph.Insert(6);
            graph.Insert(2, 0, 1, 2);
            graph.Insert(3, 1, 2, 1);
            graph.Insert(3, 0, 3, 6);
            graph.Insert(2, 2, 3, 5);
            graph.Pfs(bfsAction);
            for (int i = 0; i < graph.N; i++)
            {
                Console.WriteLine(graph.Parent(i)); 
            }

        }
       
        
    }
}
