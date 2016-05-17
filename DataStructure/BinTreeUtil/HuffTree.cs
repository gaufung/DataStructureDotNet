using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sequence
{
    public class HuffTree
    {
        public static BinNode<HuffChar> BuildHuffTree(char[] nodes, double[] weights)
        {
            if(nodes.Length!=weights.Length)
                throw new ArgumentException("The node's count is not equal to weight's count");
            IVector<BinNode<HuffChar>> huffchars = InitHuffChars(nodes, weights);
            while (huffchars.Size>1)
            {
                Tuple<Int32, Int32> twoMin = FindTwoMin(huffchars, 0, huffchars.Size);
                int first = Math.Min(twoMin.Item1, twoMin.Item2);
                int second = Math.Max(twoMin.Item1, twoMin.Item2);
                var node1 = huffchars.Remove(second);
                var node2 = huffchars.Remove(first);

                BinNode<HuffChar> newNode = new BinNode<HuffChar>(
                    new HuffChar('^', node1.Data.Weight + node2.Data.Weight)); ;
                if (node1.Data.Weight < node2.Data.Weight)
                {
                    newNode.LChild = node1;
                    newNode.RChild = node2;
                }
                else
                {
                    newNode.LChild = node2;
                    newNode.RChild = node1;
                }
                huffchars.Insert(newNode);
            }
            return huffchars[0];
        }

        private static IVector<BinNode<HuffChar>> InitHuffChars(char[] nodes, double[] weights)
        {
            IVector<BinNode<HuffChar>> huffChars =
                Vector<BinNode<HuffChar>>.VectorFactory();
            for (int i = 0; i < nodes.Length; i++)
            {
                huffChars.Insert(
                            new BinNode<HuffChar>(
                                new HuffChar(nodes[i], weights[i])
                                    )
                                );
            }
            return huffChars;
        }

        #region 查找最小的两个权重最小的节点, 时间复杂度为log(n)

        private static Tuple<Int32, Int32> TrivialTwoMin(IVector<BinNode<HuffChar>> huffchars, Int32 lo, int hi)
        {
            int first = huffchars[lo].Data.Weight < huffchars[lo + 1].Data.Weight ? lo : lo + 1;
            int second = first == lo ? lo + 1 : lo;
            for (int i = lo+2; i < hi; i++)
            {
                if (huffchars[i].Data.Weight < huffchars[second].Data.Weight)
                {
                    if (huffchars[i].Data.Weight < huffchars[first].Data.Weight)
                    {
                        second = first;
                        first = i;
                    }
                    else
                    {
                        second = i;
                    }
                }
            }
            return new Tuple<int, int>(first,second);
        }

        private static Tuple<Int32, Int32> FindTwoMin(IVector<BinNode<HuffChar>> huffchars, Int32 lo, Int32 hi)
        {
            if (hi - lo <= 3)
            {
                return TrivialTwoMin(huffchars, lo, hi);
            }
            int mi = (hi + lo) >> 1;
            Tuple<Int32, Int32> firstPart = FindTwoMin(huffchars, lo, mi);
            Tuple<Int32, Int32> secondPart = FindTwoMin(huffchars, mi, hi);
            if (huffchars[firstPart.Item1].Data.Weight < huffchars[secondPart.Item1].Data.Weight)
            {
                return huffchars[firstPart.Item2].Data.Weight < huffchars[secondPart.Item1].Data.Weight ?
                    firstPart :
                    new Tuple<int, int>(firstPart.Item1, secondPart.Item1);
            }
            else
            {
                return
               huffchars[secondPart.Item2].Data.Weight < huffchars[firstPart.Item1].Data.Weight ?
                   secondPart :
                   new Tuple<int, int>(secondPart.Item1, firstPart.Item1);
            }
           
        }
        #endregion

    }
}
