using System;

namespace Sequence.BTree
{
    /// <summary>
    /// B树 Not Complete
    /// </summary>
    /// <typeparam name="T">类型参数，继承IComparable&lt;T&gt;接口</typeparam>
    public class BTree<T> where T: IComparable<T>
    {
        /// <summary>
        /// 结点数目
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// B 树的阶(表示该B树共几路分枝)
        /// <remarks>
        /// <list type="Bullet">
        /// <item>
        /// key&lt;_order
        /// </item>
        /// <item>
        /// key+1&gt;=(_order+1)/2
        /// </item>
        /// </list>
        /// </remarks>
        /// </summary>
        private readonly int _order;
        /// <summary>
        /// 根结点
        /// </summary>
        public BtNode<T> Root { get; private set; }

        /// <summary>
        /// 命中结点的上一个
        /// </summary>
        private BtNode<T> Hot { get; set; }

        /// <summary>
        /// 判断B树是否为空
        /// </summary>
        public bool Empty
        {
            get
            {
                return Root == null;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="order">默认order为5</param>
        public BTree(int order = 5)
        {
            _order = order;
            Size = 0;
            Root = new BtNode<T>();
        }
        /// <summary>
        /// 解决上溢问题
        /// </summary>
        /// <param name="v"></param>
        private void SolveOverFlow(BtNode<T> v)
        {
            if (_order > v.Key.Count) return;//尚未达到溢出状态，递归基返回(分支数目)
            int s = _order / 2;//获取中间的节点
            BtNode<T> u = new BtNode<T>();
            for (int j = 0; j < (_order-1)-s; j++)
            {
                u.Child.Insert(j, v.Child.RemoveFrom(s + 1));
                u.Key.Insert(j, v.Key.RemoveFrom(s + 1));
            }
            u.Child[_order-1-s] = v.Child.RemoveFrom(s + 1);
            for (int j = 0; j < _order-s; j++)
            {
                if (u.Child[j]!=null)
                {
                    u.Child[j].Parent = u;                    
                }
            }
            BtNode<T> p = v.Parent;
            T liftingNoe = v.Key.RemoveFrom(s);
            //如果V是Root
            if (p == null)
            {
                Root = new BtNode<T>(liftingNoe,v,u);
            }
            else
            {
                int r = 1 + p.Key.Search(liftingNoe);
                p.Key.Insert(r, liftingNoe);
                p.Child.Insert(r + 1, u);
                u.Parent = p;
                SolveOverFlow(p);//上升一层
            }
        }
        /// <summary>
        /// 节点下溢
        /// </summary>
        /// <param name="v"></param>
        protected void SolveUnderFlow(BtNode<T> v)
        {
            if ((_order + 1) / 2 <= v.Key.Count+1) return;//递归基
            BtNode<T> p = v.Parent;//找到其父节点
            if (p == null)//已经到达根节点
            {
                //如果关键码为空，但有唯一的非空的孩子
                if (v.Key.Count == 0 && v.Child[0] != null)
                {
                    Root = v.Child[0];
                    Root.Parent = null;
                    v.Child[0] = null;
                }
                return;
            }
            int r = 0;
            while (p.Child[r] != v)
            {
                r++;//查找到该孩子的节点
            }
            if (r > 0)//如果不是第一个孩子
            {
                BtNode<T> ls = p.Child[r - 1];//找到其左边的兄弟
                if ((_order + 1) / 2 < ls.Key.Count)//如果其兄弟大于分支数目的下限
                {
                    v.Key.Insert(0, p.Key[r - 1]);//v插入关键码
                    p.Key[r - 1] = ls.Key.RemoveFrom(ls.Key.Count - 1);//将左边的最后一个关键码移动到上面
                    v.Child.Insert(0, ls.Child.RemoveFrom(ls.Child.Count - 1));//指向移动左->右
                    if (v.Child[0] != null)
                    {
                        v.Child[0].Parent = v;//修改父亲指针
                        return;
                    }
                }
            }
            if (p.Child.Count - 1 > r)//如果v不是最后一个孩子
            {
                BtNode<T> rs = p.Child[r + 1];//右孩子
                if ((_order + 1) / 2 < rs.Key.Count)//如果有孩子大于
                {
                    v.Key.Insert(v.Key.Count, p.Key[r]);
                    p.Key[r] = rs.Key.RemoveFrom(0);
                    v.Child.Insert(v.Child.Count, rs.Child.RemoveFrom(0));
                    if (v.Child[v.Child.Count - 1] != null)
                    {
                        v.Child[v.Child.Count - 1].Parent = v;
                    }
                    return;
                }
            }
            if (0 < r)//不是最左孩子，合并操作
            {
                BtNode<T> ls = p.Child[r - 1];
                ls.Key.Insert(ls.Key.Count, p.Key.RemoveFrom(r - 1));
                p.Child.RemoveFrom(r);
                ls.Child.Insert(ls.Child.Count, v.Child.RemoveFrom(0));
                if (ls.Child[ls.Child.Count - 1] != null)
                {
                    ls.Child[ls.Child.Count - 1].Parent = ls;
                }
                while (v.Key.Count != 0)//右边节点整体复制
                {
                    ls.Key.Insert(ls.Key.Count, v.Key.RemoveFrom(0));
                    ls.Child.Insert(ls.Child.Count, v.Child.RemoveFrom(0));
                    if (ls.Child[ls.Child.Count - 1] != null)
                    {
                        ls.Child[ls.Child.Count - 1].Parent = ls;
                    }
                }
            }
            else//不是最右孩子，合并操作
            {
                BtNode<T> rs = p.Child[r + 1];//右兄弟
                rs.Key.Insert(0, p.Key.RemoveFrom(r));
                p.Child.RemoveFrom(r);
                rs.Child.Insert(0, v.Child.RemoveFrom(v.Child.Count - 1));
                if (rs.Child[0] != null)
                {
                    rs.Child[0].Parent = rs;
                }
                while (v.Key.Count != 0)
                {
                    rs.Key.Insert(0, v.Key.RemoveFrom(v.Key.Count - 1));
                    rs.Child.Insert(0, v.Child.RemoveFrom(v.Child.Count - 1));
                    if (rs.Child[0] != null)
                    {
                        rs.Child[0].Parent = rs;
                    }
                }
            }
            SolveUnderFlow(p);
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public BtNode<T> Search(T e)
        {
            BtNode<T> v = Root;
            Hot = null;
            while (v != null)
            {
                int r = v.Key.Search(e);//二分查找，返回不大于e的最大值
               // int r = v.Key.BinarySearch(e);
                if ((0 <= r) && e.Eq(v.Key[r]))//如果命中，返回当前超级节点
                {
                    return v;
                }
                Hot = v;
                v = v.Child[r + 1];//将搜索转向下一个超级节点
            }
            return null;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <remarks>如果不存在，返回false
        /// 删除成功返回true
        /// </remarks>
        /// <param name="e"></param>
        /// <returns>不存在false,删除成功返回true</returns>
        public bool Remove(T e)
        {
            BtNode<T> v = Search(e);
            if (v == null)
            {
                return false;
            }
            int r = v.Key.Search(e);//查找，必定能够查到到
            if (v.Child[0] != null)//如果v不是叶子节点
            {
                BtNode<T> u = v.Child[r + 1];
                while (u.Child[0] != null)//重复执行，一直找到其直接后继节点（中序遍历）
                {
                    u = u.Child[0];
                }
                v.Key[r] = u.Key[0];
                v = u;
                r = 0;
            }
            v.Key.RemoveAt(r);//删除其叶节点
            v.Child.RemoveAt(r + 1);
            Size--;
            SolveUnderFlow(v);//解决下溢问题
            return true;

        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <remarks>
        /// 如果该关键码存在，则不在插入，否则继续插入数据，直接插入叶结点
        /// </remarks>
        /// <param name="e">要插入的数据</param>
        /// <returns>返回是否插入成功</returns>
        public bool Insert(T e)
        {
            BtNode<T> v = Search(e);
            if (v != null) return false;//如果节点存在，不必插入
            //Hot指针指向的为当前要插入的数据的超级结点
            //并且肯定是位于叶结点处
            int r = Hot.Key.Search(e);//hot 当前null的父节点，对当前超级节点做一次查询
            Hot.Key.Insert(r + 1, e);
            Hot.Child.Insert(r + 2, null);
            Size++;
            SolveOverFlow(Hot);//上溢出处理
            return true;
        }

    }
}
