using System;
namespace Sequence
{
    /// <summary>
    /// 二叉搜索树的基类(Binary Search Tree)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Bst<T> : BinTree<T> where T :IComparable<T>
    {
        /// <summary>
        /// search()函数中最后访问的非空结点的位置
        /// </summary>
        protected BinNode<T> Hot;


        /// <summary>
        /// Constructor，构建一个空树并且Hot结点为null
        /// </summary>
        public Bst():base()
        {
            Hot = null;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual BinNode<T> Search(T e)
        {
           return  SearchIn(Root, e);
        }

        #region 比较操作
        /// <summary>
        /// 判断是否相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        protected   bool Eq(T a, T b)
        {
            return BinNode<T>.Eq(a, b);
        }
        /// <summary>
        /// 判断是否小于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        protected   bool Lt(T a, T b)
        {
            return BinNode<T>.Lt(a, b);
        }
        /// <summary>
        /// 判断是否大于
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        protected   Boolean Gt(T a, T b)
        {
            return !Lt(a, b);
        }
        #endregion
        /// <summary>
        /// 查找
        /// <remarks>
        /// 更新Hot结点
        /// </remarks>
        /// </summary>
        /// <param name="v"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected BinNode<T> SearchIn(BinNode<T> v, T e)
        {          
            if (v==null||Eq(v.Data,e))
            {
                return v;
            }
            //更新Hot结点
            Hot = v;
            return SearchIn((Lt(e, v.Data)) ? v.LChild : v.RChild, e);
        } 

        /// <summary>
        /// 插入结点，不考虑重复key
        /// <remarks>
        /// 使用Virtual修饰函数修饰，使之能够AVL 和Splay树等能够重载
        /// </remarks>
        /// </summary>
        /// <param name="e"></param>
        /// <returns>
        /// 返回插入的结点的位置（）
        /// </returns>
        public virtual BinNode<T> Insert(T e)
        {
            var x=Search(e);
            //如果查找到某一个已经存在的
            if (x!=null) return x;
            //如果插入的是根节点
            if (Hot == null)
            {
                Root = new BinNode<T>(e);
                Size++;
                UpdateHeightAbove(Root);
                return Root;
            }
            //如果不是跟结点
            x = Lt(e, Hot.Data) ? Hot.InsertAsLc(e) : Hot.InsertAsRc(e);
            Size++;
            UpdateHeightAbove(x);
            return x;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual bool Remove(T e)
        {
            var x = Search(e);
            if (x == null) return false;       
            RemoveAt(x);
            Size--;
            UpdateHeightAbove(Hot);
            return true;
        }

        protected  BinNode<T> RemoveAt(BinNode<T> x)
        {
            BinNode<T> w = x;
            BinNode<T> succ;
            //如果没有左孩子
            if (!x.HasLChild)
            {
                if (x.IsLChild)
                {
                    Hot.LChild = x.RChild;
                }
                else
                {
                    Hot.RChild = x.RChild;
                }
                succ = x.RChild;
            }
            else if (!x.HasRChild)
            {
                if (x.IsLChild)
                {
                    Hot.LChild = x.LChild;
                }
                else
                {
                    Hot.RChild = x.LChild;
                }
                succ = x.LChild;
            }
            else
            {
                w = w.Succ();
                Swap(w,x);
                BinNode<T> u = w.Parent;
                if (u == x)
                {
                    u.RChild = succ = w.RChild;
                }
                else
                {
                    u.LChild = succ = w.RChild;
                }
            }
            Hot = w.Parent;
            if (succ!=null)
            {
                succ.Parent = Hot;
            }
            return succ; 
        }

        /// <summary>
        /// 交换数据，用在删除操作
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        protected void Swap(BinNode<T> a, BinNode<T> b)
        {
            T temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;
        }
        protected BinNode<T> Connect34(BinNode<T> a, BinNode<T> b, BinNode<T> c,
           BinNode<T> t0, BinNode<T> t1, BinNode<T> t2, BinNode<T> t3)
        {
            a.LChild = t0;
            if (t0 != null)
            {
                t0.Parent = a;
            }
            a.RChild = t1;
            if (t1 != null)
            {
                t1.Parent = a;

            }
            UpdateHeight(a);
            c.LChild = t2;
            if (t2 != null)
            {
                t2.Parent = c;
            }
            c.RChild = t3;
            if (t3 != null)
            {
                t3.Parent = c;
            }
            UpdateHeight(c);
            b.LChild = a;
            a.Parent = b;
            b.RChild = c;
            c.Parent = b;
            UpdateHeight(b);
            return b;
        }
        
    }
}
