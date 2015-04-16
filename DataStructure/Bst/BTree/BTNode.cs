using System;
using System.Collections.Generic;

namespace Bst.BTree
{
    /// <summary>
    /// 超级节点的定义
    /// </summary>
    /// <typeparam name="T">关键码</typeparam>
    public class BtNode<T> where T:IComparable
    {
        public BtNode<T> Parent { get; set; }
        public List<T> Key { get; set; }
        public List<BtNode<T>> Child { get; set; }

        public BtNode()
        {
            Parent = null;
            Child.Insert(0,null);
        }
        public BtNode(T e, BtNode<T> lc = null, BtNode<T> rc = null)
        {
            Parent = null;
            Key.Insert(0,e);
            Child.Insert(0,lc);
            Child.Insert(1,rc);           
            if (lc!=null)
            {
                lc.Parent=this;
            }
            if(rc!=null)
            { 
                rc.Parent=this;
            }
        } 
    }
}
