using NUnit.Framework;
using Sequence;

namespace BinaryTreeTest
{

    [TestFixture]
    public class HuffmanTreeTest
    {
       
        [Test]
        public void TestHuffaman()
        {
            char[] chars = { 'a', 'b', 'c', 'd' };
            double[] weight = { 7.0, 5.0, 2.0, 4.0 };
            HuffTree huffman = new HuffTree(chars, weight);
            Assert.AreEqual(huffman.Decode(huffman.Encode("abcd")), "abcd");

        }
    }
}
