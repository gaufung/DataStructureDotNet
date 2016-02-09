using NUnit.Framework;
using Sequence;
using Sequence.GraphMatrix;

namespace GraphTest
{
    [TestFixture]
    public class TestRun
    {
        private Graph<int, int, int> graph;

        public TestRun()
        {
            graph = GraphMatrix<int, int, int>.GraphFactory();
        }

        [Test]
        public void TestNE()
        {
            graph.Insert(1);
            graph.Insert(2);
            graph.Insert(3, 0, 1, 1);
            Assert.AreEqual(2,graph.N);
            Assert.AreEqual(1,graph.E);
            Assert.AreEqual(3,graph.Remove(0,1));
            Assert.AreEqual(0,graph.E);
        }


        private void InitGraph()
        {
            graph.Insert(1);

        }
    }
}
