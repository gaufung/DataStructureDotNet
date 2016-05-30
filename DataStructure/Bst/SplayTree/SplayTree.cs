using System;

namespace Sequence.SplayTree
{
    /// <summary>
    /// 伸展树
    /// <remarks>
    /// 继承Bst
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">类型参数</typeparam>
    [Serializable]
    public class SplayTree<T>:Bst<T> where T :IComparable<T>
    {

        /// <summary>
        /// 伸展某个结点
        /// </summary>
        /// <param name="v">要提升到根结点的结点</param>
        /// <returns>返回该结点</returns>
        private BinNode<T> Splay(BinNode<T> v)
        {
            if (v == null) return null;
            //p->parent
            //g->grandfather
            BinNode<T> p, g;
            while ((p = v.Parent) != null && (g = p.Parent) != null)
            {
                BinNode<T> gg = g.Parent;
                bool isLChild = g.IsLChild;
                if (v.IsLChild)
                {
                    //zig-zig
                    if (p.IsLChild)
                    {
                        g.AttachAsLChild(p.RChild);
                        p.AttachAsLChild(v.RChild);
                        p.AttachAsRChild(g);
                        v.AttachAsRChild(p);
                    }
                    //zig-zag
                    else
                    {
                        p.AttachAsLChild(v.RChild);
                        g.AttachAsRChild(v.LChild);
                        v.AttachAsLChild(g);
                        v.AttachAsRChild(p);
                    }
                }
                //zag-zag
                else if (p.IsRChild)
                {
                    g.AttachAsRChild(p.LChild);
                    p.AttachAsRChild(v.LChild);
                    p.AttachAsLChild(g);
                    v.AttachAsLChild(p);

                }
                //zag-zig
                else
                {
                    p.AttachAsRChild(v.LChild);
                    g.AttachAsLChild(v.RChild);
                    v.AttachAsRChild(g);
                    v.AttachAsLChild(p);
                }
                //如果g是根结点
                if (gg == null)
                {
                    v.Parent = null;
                }
                //如果不是
                else
                {
                    if (isLChild)
                    {

                        gg.AttachAsLChild(v);
                    }
                    else
                    {
                        gg.AttachAsRChild(v);
                    }
                }
                UpdateHeight(g);
                UpdateHeight(p);
                UpdateHeight(v);
            }
            //如果只剩下一个结点
            if ((p = v.Parent) != null)//做一次单旋
            {
                //zig
                if (v.IsLChild)
                {
                    p.AttachAsLChild(v.RChild);
                    v.AttachAsRChild(p);
                }
                //zag
                else
                {
                    p.AttachAsRChild(v.LChild);
                    v.AttachAsLChild(p);
                }
                UpdateHeight(p);
                UpdateHeight(v);
            }
            v.Parent = null;
            return v;
        }

        /// <summary>
        /// 查找函数
        /// <remarks>
        /// 查找到元素，如果命中，将其伸展到根结点，
        /// 否则将Hot结点伸展到跟结点
        /// </remarks>
        /// </summary>
        /// <param name="e">要查找的元素</param>
        /// <returns></returns>
        public override BinNode<T> Search(T e)
        {
            BinNode<T> p = SearchIn(Root,e);
            Root = Splay(p ?? Hot);
            return Root;
        }

        /// <summary>
        /// 重写插入
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override BinNode<T> Insert(T e)
        {
            if (Root == null)
            {
                Size++;
                return Root = new BinNode<T>(e);
            }
            if (Eq(e, Search(e).Data))
            {
                return Root;
            }
            Size++;
            BinNode<T> t = Root;
            if (Lt(Root.Data, e))
            {
                t.Parent = Root = new BinNode<T>(e, null, t, t.RChild);
                if (t.HasRChild)
                {
                    t.RChild.Parent = Root;
                    t.RChild = null;
                }
            }
            else
            {
                t.Parent = Root = new BinNode<T>(e, null, t.LChild, t);
                if (t.HasLChild)
                {
                    t.LChild.Parent = Root;
                    t.LChild = null;
                }
            }
            UpdateHeightAbove(t);
            return Root;
        }
        /// <summary>
        /// 重写删除
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override bool Remove(T e)
        {
            if (Root == null || !Eq(e, Search(e).Data))
            {
                return false;
            }
            BinNode<T> w = Root;
            if (!Root.HasLChild)
            {
                Root = Root.RChild;
                if (Root != null)
                {
                    Root.Parent = null;
                }
            }
            else if (!Root.HasRChild)
            {
                Root = Root.LChild;
                if (Root != null)
                {
                    Root.Parent = null;
                }
            }
            else
            {
                BinNode<T> lTree = Root.LChild;
                BinNode<T> succ = Root.Succ();
                if (succ.IsLChild)
                {
                    succ.Parent.LChild = null;
                }
                else
                {
                    succ.Parent.RChild = null;
                }
                succ.RChild = Root.RChild;
                Root = succ;
                Root.Parent = null;
                Root.LChild = lTree;
                lTree.Parent = Root;
                //lTree.Parent = null;
                //Root.LChild = null;
                //Root = Root.RChild;
                //Root.Parent = null;
                //Search(w.Data);
                //Root.LChild = lTree;
                //lTree.Parent = Root;
            }
            Size--;
            if (Root != null)
            {
                UpdateHeight(Root);
            }
            return true;
        }
    }
}
