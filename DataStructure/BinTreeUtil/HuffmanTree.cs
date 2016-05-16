using System;
using System.Diagnostics;

namespace Sequence
{
    public class HuffmanTree<T> where T:IComparable<T>
    {
        public static BinNode<T> BuildHuffmanTree(IVector<T> nodes,
            IVector<Double> weights)
        {
            Debug.Assert(nodes.Size==weights.Size);
            IVector<WeightBinNode<T>> weightNodes = InitWeightNodes(nodes, weights);
            while (weightNodes.Size>1)
            {
                Tuple<Int32, Int32> twoMin = FindTwoMin(weightNodes, 0, weightNodes.Size);
                int first = Math.Min(twoMin.Item1, twoMin.Item2);
                int second = Math.Max(twoMin.Item1, twoMin.Item2);
                WeightBinNode<T> node1 = weightNodes.Remove(second);
                WeightBinNode<T> node2 = weightNodes.Remove(first);
                WeightBinNode<T> newNode=new WeightBinNode<T>()
                {
                    Node = new BinNode<T>(),
                    Weight = node1.Weight+node2.Weight
                };
                if (node1.Weight < node2.Weight)
                {
                    newNode.Node.LChild = node1.Node;
                    newNode.Node.RChild = node2.Node;
                }
                else
                {
                    newNode.Node.LChild = node2.Node;
                    newNode.Node.RChild = node1.Node;
                }
                weightNodes.Insert(newNode);
            }
            return weightNodes[0].Node;
        }

        /// <summary>
        /// Init the WeightNode
        /// </summary>
        /// <param name="nodes">node's data T </param>
        /// <param name="weights">node's weight</param>
        /// <returns>the vector of weightBinNode</returns>
        private static IVector<WeightBinNode<T>> InitWeightNodes(IVector<T> nodes,
            IVector<Double> weights)
        {
            IVector<WeightBinNode<T>> weightNodes 
                = Vector<WeightBinNode<T>>.VectorFactory();
            for (int i = 0; i < nodes.Size; i++)
            {
                weightNodes.Insert(new WeightBinNode<T>()
                {
                    Node = new BinNode<T>(nodes[i]),
                    Weight = weights[i]
                });
            }
            return weightNodes;
        }

        #region 查找最小的两个的两个值 时间复杂度 log(n)

        /// <summary>
        /// 平凡情况
        /// </summary>
        /// <param name="weightNodes"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        private static Tuple<Int32, Int32> TrivialTwoMin(IVector<WeightBinNode<T>> weightNodes
          , Int32 lo, Int32 hi)
        {
            //the first index is minium
            //the second index is next minium
            int first = weightNodes[lo].Weight < weightNodes[lo + 1].Weight ? lo : lo + 1;
            int second = first == lo ? lo + 1 : lo;
            for (int i = lo + 2; i < hi; i++)
            {
                if (weightNodes[i].Weight < weightNodes[second].Weight)
                {
                    if (weightNodes[i].Weight < weightNodes[first].Weight)
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
            return new Tuple<int, int>(first, second);
        }


        /// <summary>
        /// 二分查找 [lo,hi)
        /// </summary>
        /// <param name="weightNodes"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        private static Tuple<Int32, Int32> FindTwoMin(IVector<WeightBinNode<T>> weightNodes
            , Int32 lo, Int32 hi)
        {
            if (hi - lo <= 3)
            {
                return TrivialTwoMin(weightNodes, lo, hi);
            }
            int mi = (hi + lo) >> 1;
            Tuple<Int32, Int32> firstPart = FindTwoMin(weightNodes, lo, mi);
            Tuple<Int32, Int32> secondPart = FindTwoMin(weightNodes, mi, hi);
            if (weightNodes[firstPart.Item1].Weight < weightNodes[secondPart.Item1].Weight)
            {
                return weightNodes[firstPart.Item2].Weight < weightNodes[secondPart.Item1].Weight ?
                    firstPart :
                    new Tuple<int, int>(firstPart.Item1, secondPart.Item1);
            }
            return
                weightNodes[secondPart.Item2].Weight < weightNodes[firstPart.Item1].Weight ?
                    secondPart :
                    new Tuple<int, int>(secondPart.Item1, firstPart.Item1);
        }
        #endregion
      
    }
}
