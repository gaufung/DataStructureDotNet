using System;
using Tree;

namespace Bst.SplayTree
{
    public class Splay<T>:Bst<T> where T:IComparable
    {
        /// <summary>
        /// 将节点延展至根
        /// </summary>
        /// <param name="v">最近访问的位置V</param>
        /// <returns></returns>
        public BinNode<T> splay(BinNode<T> v)
        {
            if (v == null) return null;
            BinNode<T> p, g;
            while((p=v.Parent)!=null&&(g=p.Parent)!=null)
            {
                BinNode<T> gg = g.Parent;
                bool isLChild=g.IsLChild;
                if (v.IsLChild)
                {
                    if (p.IsLChild)
                    {
                        g.AttachAsLChild(p.RChild);
                        p.AttachAsLChild(v.RChild);
                        p.AttachAsRChild(g);
                        v.AttachAsRChild(p);
                    }
                    else
                    {
                        p.AttachAsLChild(v.RChild);
                        g.AttachAsRChild(v.LChild);
                        v.AttachAsLChild(g);
                        v.AttachAsRChild(p);
                    }
                }
                else if (p.IsRChild)
                {
                    g.AttachAsRChild(p.LChild);
                    p.AttachAsRChild(v.LChild);
                    p.AttachAsLChild(g);
                    v.AttachAsLChild(p);

                }
                else
                {
                    p.AttachAsRChild(v.LChild);
                    g.AttachAsLChild(v.RChild);
                    v.AttachAsRChild(g);
                    v.AttachAsLChild(p);
                }
                if (gg == null)
                {
                    v.Parent = null;
                }
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
            if ((p=v.Parent)!=null)//做一次单旋
            {
                if (v.IsLChild)
                {
                    p.AttachAsLChild(v.RChild);
                    v.AttachAsRChild(p);
                }
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
        /// 重写查找
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override  BinNode<T> Search(T e)
        {
            var p=SearchIn(Root,e);           
            Root = splay(p==null?Hot:p);
            return Root;
        }
        /// <summary>
        /// 重写插入
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override BinNode<T> Insert(T e)
        {
            if (Root==null)
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
            if (Lt(Root.Data,e))
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
           // throw new NotImplementedException();
            if (Root==null||!Eq(e,Search(e).Data))
            {
                return false;
            }
            BinNode<T> w = Root;
            if (!Root.HasLChild)
            {
                Root = Root.RChild;
                if (Root!=null)
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
                lTree.Parent = null;
                Root.LChild = null;
                Root = Root.RChild;
                Root.Parent = null;
                Search(w.Data);
                Root.LChild = lTree;
                lTree.Parent = Root;
            }
            Size--;
            if (Root!=null)
            {
                UpdateHeight(Root);
            }
            return true;
        }

        #region 比较操作
        private static bool Eq(T a, T b)
        {
            return BinNode<T>.Eq(a, b);
        }

        private static bool Lt(T a, T b)
        {
            return BinNode<T>.Lt(a, b);
        }
        #endregion
    }
}
