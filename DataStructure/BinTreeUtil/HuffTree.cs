using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sequence
{
    public class HuffTree
    {
        /// <summary>
        /// 字符和其权重
        /// </summary>
        private readonly Dictionary<char, double> _charweights;

        /// <summary>
        /// 字符及其对应的编码
        /// </summary>
        private readonly Dictionary<char, string> _charCodeMap;

        /// <summary>
        /// Huffman编码树的根节点
        /// </summary>
        private BinNode<HuffChar> _huffmanRoot; 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="charweights">一个字典</param>
        public HuffTree(Dictionary<char, double> charweights)
        {
            _charweights = charweights;
            _charCodeMap=new Dictionary<char, string>();
            BuildTree();
            BuildCodeMap();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="weights"></param>
        public HuffTree(IEnumerable<char> nodes,IEnumerable<Double> weights)
        {
            if(nodes.Count()!=weights.Count())
                throw new ArgumentException("The char set count is not equal to weight's count");
            var nodesList = nodes.ToList();
            var weightList = weights.ToList();
            _charweights=new Dictionary<char, double>();
            for (int i = 0; i < nodesList.Count; i++)
            {
                if (_charweights.ContainsKey(nodesList[i]))
                {
                    throw new ArgumentException("The char set must not eqaul by each");
                }
                _charweights.Add(nodesList[i],weightList[i]);
            }
            _charCodeMap=new Dictionary<char, string>();
            BuildTree();
            BuildCodeMap();
        }


        #region 构建Huffman树
        private void BuildTree()
        {
            IVector<BinNode<HuffChar>> huffchars = InitHuffChars();
            while (huffchars.Size > 1)
            {
                Tuple<Int32, Int32> twoMin = FindTwoMin(huffchars, 0, huffchars.Size);
                int first = Math.Min(twoMin.Item1, twoMin.Item2);
                int second = Math.Max(twoMin.Item1, twoMin.Item2);
                var node1 = huffchars.Remove(second);
                var node2 = huffchars.Remove(first);

                BinNode<HuffChar> newNode = new BinNode<HuffChar>(
                    new HuffChar('^', node1.Data.Weight + node2.Data.Weight),null,node1,node2);
                node1.Parent = newNode;
                node2.Parent = newNode;
                huffchars.Insert(newNode);
            }
            _huffmanRoot = huffchars[0];
        }
        private  IVector<BinNode<HuffChar>> InitHuffChars()
        {
            IVector<BinNode<HuffChar>> huffChars =
                Vector<BinNode<HuffChar>>.VectorFactory();
            foreach (var charItem in _charweights.Keys)
            {
                huffChars.Insert(new BinNode<HuffChar>(
                    new HuffChar(charItem, _charweights[charItem])));
            }
            return huffChars;
        }

        /// <summary>
        /// 递归基，平凡情况，如果只有两个或者三个要素时
        /// </summary>
        /// <param name="huffchars"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        private  Tuple<Int32, Int32> TrivialTwoMin(IVector<BinNode<HuffChar>> huffchars, Int32 lo, int hi)
        {
            int first = huffchars[lo].Data.Weight < huffchars[lo + 1].Data.Weight ? lo : lo + 1;
            int second = first == lo ? lo + 1 : lo;
            for (int i = lo + 2; i < hi; i++)
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
            return new Tuple<int, int>(first, second);
        }

        /// <summary>
        /// 递归迭代版查找最小的两个要素
        /// </summary>
        /// <param name="huffchars"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        private  Tuple<Int32, Int32> FindTwoMin(IVector<BinNode<HuffChar>> huffchars, Int32 lo, Int32 hi)
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
            return
                huffchars[secondPart.Item2].Data.Weight < huffchars[firstPart.Item1].Data.Weight ?
                    secondPart :
                    new Tuple<int, int>(secondPart.Item1, firstPart.Item1);
        }
        #endregion


        #region Build char code Map

        /// <summary>
        /// 遍历整个二叉树
        /// </summary>
        private void BuildCodeMap()
        {
            _huffmanRoot.TravIn(BuildCodeMap);
        }

        /// <summary>
        /// 如果是叶节点
        /// </summary>
        /// <param name="node"></param>
        private void BuildCodeMap(BinNode<HuffChar> node)
        {
            if (!node.HasBothChild)
            {
                char code = node.Data.Ch;
                string s = string.Empty;
                while (node!=_huffmanRoot)
                {
                    if (node.IsLChild)
                    {
                        s += "0";
                    }
                    else
                    {
                        s += "1";
                    }
                    node = node.Parent;
                }
                _charCodeMap.Add(code,new string(s.Reverse().ToArray()));
            }
        }
        #endregion

        /// <summary>
        /// 将某个文本加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public String Encode(string text)
        {
            StringBuilder sb=new StringBuilder();
            foreach (var item in text)
            {
                if(!_charCodeMap.ContainsKey(item))
                    throw new ArgumentException("The some one char does not exist in char set");
                sb.Append(_charCodeMap[item]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将某个密文解码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string Decode(string code)
        {
            StringBuilder sb=new StringBuilder();
            BinNode<HuffChar> hot=_huffmanRoot;
            bool backToRoot = true;
            foreach (char t in code)
            {
                if (backToRoot)
                {
                    hot = _huffmanRoot;
                }
                if (hot == null)
                {
                    throw new ArgumentException("the code is not illeagl");
                }
                if (t == '0')
                {
                    hot = hot.LChild;
                    if (!hot.HasBothChild)
                    {
                        sb.Append(hot.Data.Ch);
                        backToRoot = true;
                    }
                    else
                    {
                        backToRoot = false;
                    }
                }
                else
                {
                    hot = hot.RChild;
                    if (!hot.HasBothChild)
                    {
                        sb.Append(hot.Data.Ch);
                        backToRoot = true;
                    }
                    else
                    {
                        backToRoot = false;
                    }
                }
            }
            return sb.ToString();
        }

    }
}
