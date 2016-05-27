//using NUnit.Framework;
//using Sequence;

//namespace BinaryTreeTest
//{

//    [TestFixture]
//    public class HuffmanTreeTest
//    {
//        [Test]
//        public void Test()
//        {
//            //IVector<char> nodes=Vector<char>.VectorFactory();
//            //nodes.Insert(0, 'a');
//            //nodes.Insert(0, 'b');
//            //nodes.Insert(0, 'c');
//            //nodes.Insert(0, 'd');
//            //IVector<double> weights=Vector<double>.VectorFactory();
//            //weights.Insert(0, 7.0);
//            //weights.Insert(0, 5.0);
//            //weights.Insert(0, 2.0);
//            //weights.Insert(0, 4.0);
//            //BinNode<char> root=HuffmanTree<Char>.BuildHuffmanTree(nodes, weights);
//            //root.TravLevel(item => Trace.Write(item));

//        }

//        [Test]
//        public void TestNew()
//        {
//            char[] chars = {'a', 'b', 'c', 'd'};
//            double[] weight = {7.0, 5.0, 2.0, 4.0};
//            HuffTree huffman=new HuffTree(chars,weight);
//            Assert.AreEqual(huffman.Decode(huffman.Encode("abcd")),"abcd");
            
//        }
//    }
//}
