using System;
using Tree;
namespace Bst
{
    public class Bst<T> : BinTree<T> where T :IComparable
    {
        protected BinNode<T> Hot;

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
        private static bool Eq(T a, T b)
        {
            return BinNode<T>.Eq(a, b);
        }

        private static bool Lt(T a, T b)
        {
            return BinNode<T>.Lt(a, b);
        }
        #endregion

        private  BinNode<T> SearchIn(BinNode<T> v, T e)
        {          
            if (v==null||Eq(v.Data,e))
            {
                return v;
            }
            Hot = v;
            return SearchIn((Lt(e, v.Data)) ? v.LChild : v.RChild, e);
        } 

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual BinNode<T> Insert(T e)
        {
            var x=Search(e);
            //如果查找到某一个已经存在的
            if (x!=null)
            {
                return x;
            }
            //如果插入的是根节点
            if (Hot == null)
            {
                Root = new BinNode<T>(e);
                Size++;
                UpdateHeightAbove(Root);
                return Root;
            }
            else
            {
                if (Lt(e, Hot.Data))
                {
                   
                    x= Hot.InsertAsLc(e);
                }
                else
                {
                   x=Hot.InsertAsRc(e);
                }
                Size++;
                UpdateHeightAbove(x);
                return x;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public virtual bool Remove(T e)
        {
            var x = Search(e);
            if (x==null)
            {
                return false;
            }
            RemoveAt(x);
            Size--;
            UpdateHeightAbove(Hot);
            return true;
        }

        protected  BinNode<T> RemoveAt(BinNode<T> x)
        {
            BinNode<T> w = x;
            BinNode<T> succ;
            if (!x.HasLChild)
            {
                if (x.IsLChild)
                {
                    Hot.LChild = x.RChild;
                 //   x.RChild.Parent = Hot;
                }
                else
                {
                    Hot.RChild = x.RChild;
                 //   x.RChild.Parent = Hot;
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

        private static  void Swap(BinNode<T> a, BinNode<T> b)
        {
            T temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;
        }
        
    }
}
