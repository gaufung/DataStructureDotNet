using System;

namespace Sequence
{
    /// <summary>
    /// 串的ADT接口
    /// </summary>
    public interface IString
    {
        /// <summary>
        /// 串的长度
        /// </summary>
        int Length { get;}
        /// <summary>
        /// 在某个位置的字符串
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        char CharAt(int i);
        /// <summary>
        /// 从位置i的k个字符串组成的子串
        /// </summary>
        /// <param name="i">开始位置</param>
        /// <param name="k">字符的个数</param>
        /// <returns>子串</returns>
        IString SubStr(int i, int k);
        /// <summary>
        /// 长度为k的前缀
        /// </summary>
        /// <param name="k">个数</param>
        /// <returns>子串</returns>
        IString Prefix(int k);
        /// <summary>
        /// 长度为k的后缀
        /// </summary>
        /// <param name="k">个数</param>
        /// <returns>后缀</returns>
        IString Suffix(int k);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool Equals(IString other);
        /// <summary>
        /// 字符串链接
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        void Concat(IString other);
        /// <summary>
        /// 若substr是当前字符串的一个子串，则返回该子串的起始位置，否则返回-1
        /// </summary>
        /// <param name="substr"></param>
        /// <returns></returns>
        int IndexOf(IString substr);

        /// <summary>
        /// 遍历整个操作
        /// </summary>
        /// <param name="action">action操作</param>
        void Foreach(Action<Char> action);

        /// <summary>
        /// 判断是否存在字符满足条件
        /// </summary>
        /// <param name="func"></param>
        bool Any(Func<char, bool> func);

        /// <summary>
        /// 获取第一个满足条件的字符
        /// </summary>
        /// <param name="func"></param>
        /// <returns>the index: -1 means no match char </returns>
        int First(Func<char, bool> func);
    }
}
