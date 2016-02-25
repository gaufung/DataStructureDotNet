namespace Sequence
{
    /// <summary>
    /// 串的ADT接口
    /// </summary>
    public interface ICharSequence
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
        ICharSequence SubStr(int i, int k);
        /// <summary>
        /// 长度为k的前缀
        /// </summary>
        /// <param name="k">个数</param>
        /// <returns>子串</returns>
        ICharSequence Prefix(int k);
        /// <summary>
        /// 长度为k的后缀
        /// </summary>
        /// <param name="k">个数</param>
        /// <returns>后缀</returns>
        ICharSequence Suffix(int k);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool Equals(ICharSequence other);
        /// <summary>
        /// 字符串链接
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        void Concat(ICharSequence other);
        /// <summary>
        /// 若substr是当前字符串的一个子串，则返回该子串的起始位置，否则返回-1
        /// </summary>
        /// <param name="substr"></param>
        /// <returns></returns>
        int IndexOf(ICharSequence substr);
    }
}
