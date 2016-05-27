using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Sequence;

namespace CharSequenceTest
{
    [TestFixture]
    public class RunCharSequence
    {
        private ICharSequence _sequece;

        public RunCharSequence()
        {
            _sequece=new CharSequence(new char[]
            {
                'g','a','u','f','u','n','g'
            });
        }

        [Test]
        public void TestLength()
        {
            Assert.AreEqual(7,_sequece.Length);
            Assert.AreEqual('g',_sequece.CharAt(0));
        }

        [Test]
        public void TestEqual()
        {
            ICharSequence other=new CharSequence(
                new char[]
                {
                    'g','a','u','f','u','n','g'
                });

            Assert.AreEqual(true,_sequece.Equals(other));          
        }

        [Test]
        public void TestSubstr()
        {
            ICharSequence other=new CharSequence(
                new char[]
                {
                    'g','a','u'
                });
            Assert.AreEqual('g',_sequece.Prefix(3).CharAt(0));
        }

        [Test]
        public void TestIndexOf()
        {
            ICharSequence other=new CharSequence(new char[]
            {
                'a','u'
            });
            Assert.AreEqual(1,_sequece.IndexOf(other));
            other=new CharSequence(new char[]
            {
                'f','g'
            });
            Assert.AreEqual(-1,_sequece.IndexOf(other));
        }
    }
}
