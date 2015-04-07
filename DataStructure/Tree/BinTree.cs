using System;


namespace Tree
{
    class BinTree<T> where T:IComparable<T>
    {
        #region 属性
        public int Size { get;  private set; }
        public BinNode<T> Root { get; private set; }

        public bool Empty { get { return Root == null; } } 
        #endregion

        /// <summary>
        /// 更新当前节点的高度
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        protected virtual int UpdateHeight(BinNode<T> x)
        {
            return x.Height = 1 + Math.Max(Status(x.LChild), Status(x.RChild));
        }
        /// <summary>
        /// 更新当前节点和上面给结点所有的高度
        /// </summary>
        /// <param name="x"></param>
        protected void UpdateHeightAbove(BinNode<T> x)
        {
            while (x!=null)
            {
                UpdateHeight(x);
                x = x.Parent;
            }
        }
        #region 构造函数

        public BinTree()
        {
            Size = 0;
            Root = null;
        }
        #endregion

        private int Status(BinNode<T> p)
        {
            return p != null ? p.Height : -1;
        }
        public BinNode<T> InsertAsRoot(T e)
        {
            Size = 1;
            return Root = new BinNode<T>(e);
        }

        public BinNode<T> InsertAsLc(BinNode<T> x, T e)
        {
            Size++;
            x.InsertAsLc(e);
            UpdateHeightAbove(x);
            return x.LChild;
        }
        public BinNode<T> InsertAsRc(BinNode<T> x, T e)
        {
            Size++;
            x.InsertAsRc(e);
            UpdateHeightAbove(x);
            return x.RChild;
        }

        public BinNode<T> AttachAsLc(BinNode<T> x, BinTree<T> tr)
        {
            x.LChild = tr.Root;
            x.LChild.Parent = x;
            Size += tr.Size;
            UpdateHeightAbove(x);
            return x;
        }

        public BinNode<T> AttachAsRc(BinNode<T> x, BinTree<T> tr)
        {
            x.RChild = tr.Root;
            x.RChild.Parent = x;
            Size += tr.Size;
            UpdateHeightAbove(x);
            return x;
        }

        public int Remove(BinNode<T> x)
        {
            ClearFromParent(x);
            UpdateHeight(x.Parent);
            int n = RemoveAt(x);
            Size -= n;
            return n;
        }

        private void ClearFromParent(BinNode<T> x)
        {            
            if (x.IsRChild)
            {
                x.Parent.RChild = null;
            }
            else
            {
                x.Parent.LChild = null;
            }
        }
        private int RemoveAt(BinNode<T> x)
        {
            if (x==null)
            {
                return 0;
            }
            int n = 1 + RemoveAt(x.LChild) + RemoveAt(x.RChild);
            return n;
        }

        BinTree<T> Secede(BinNode<T> x)
        {
            ClearFromParent(x);
            UpdateHeightAbove(x.Parent);
            var s=new BinTree<T>();
            s.Root = x;
            x.Parent = null;
            s.Size = x.Size();
            Size -= s.Size;
            return s;
        }

        public void TravLevel(Action<T> action)
        {
            if (!Empty)
            {
                Root.TravLevel(action);
            }
        }

        public void TravPre(Action<T> action)
        {
            if (!Empty)
            {
                Root.TravPre(action);
            }
        }

        public void TravIn(Action<T> action)
        {
            if (!Empty)
            {
                Root.TravIn(action);
            }
        }

        public void TravePost(Action<T> action)
        {
            if (!Empty)
            {
                Root.TravPost(action);
            }
        }
        

    }
}
