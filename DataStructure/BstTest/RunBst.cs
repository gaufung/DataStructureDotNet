using NUnit.Framework;
using Sequence;

namespace BstTest
{
    [TestFixture]
    public class RunBst
    {
        private Bst<int> bst;

        public RunBst()
        {
            bst=new Bst<int>();
            InitBst();
        }

        private void InitBst()
        {
            bst.Insert(3);
            bst.Insert(2);
            bst.Insert(7);
        }

        [Test]
        public void TestSearch()
        {
            Assert.AreEqual(bst.Root,bst.Search(3));
            Assert.AreEqual(null,bst.Search(10));
        }

        [Test]
        public void TestInsert()
        {
            bst.Insert(5);
            Assert.AreEqual(5,bst.Root.RChild.LChild.Data);
        }

        [Test]
        public void TestRemove()
        {
            Assert.AreEqual(false,bst.Remove(9));
            bst.Remove(3);
            Assert.AreEqual(7,bst.Root.Data);
            bst.Insert(9);
            bst.Insert(10);
            bst.Remove(9);
            Assert.AreEqual(10,bst.Root.RChild.Data);
        }
    }
}
