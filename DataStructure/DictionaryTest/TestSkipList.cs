using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;
using Sequence;

namespace DictionaryTest
{
    [TestFixture]
    public class TestSkipList
    {
        private Dictionary<int, string> dic;

        public TestSkipList()
        {
            dic=new HashDictionary<int, string>();
        }

        [Test]
        public void TestInsert()
        {
            dic[1] = "string";
            dic[102] = "gaofeng";
            Assert.AreEqual("string", dic[1]);
            Assert.AreEqual("gaofeng", dic[102]);
        }
        [Test]
        public void TestRemove()
        {
            dic[1] = "gau";
            dic[102] = "fung";
            dic.Remove(1);
            Assert.AreEqual("fung", dic[102]);
            Assert.AreEqual(false, dic.Contains(1));
        }
        [Test]
        public void TestSize()
        {
            dic[1] = "gaufung";
            Assert.AreEqual(dic.Size, 1);
            dic[1] = "fung";
            Assert.AreEqual(dic.Size, 1);
            dic[3] = "fund";
            Assert.AreEqual(dic.Size, 2);
        }
    }
}
