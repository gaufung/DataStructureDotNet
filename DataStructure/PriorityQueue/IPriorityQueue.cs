using System;

namespace Sequence
{
    /// <summary>
    /// 优先级队列的操作接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPriorityQueue<T> where T:IComparable<T>
    {
        /// <summary>
        /// 插入某一项
        /// </summary>
        /// <param name="e"></param>
        void Insert(T e);

        /// <summary>
        /// 获取优先级最大的项
        /// </summary>
        /// <returns></returns>
        T GetMax();
        /// <summary>
        /// 删除最大项
        /// </summary>
        /// <returns></returns>
        T DelMax();
    }
}
