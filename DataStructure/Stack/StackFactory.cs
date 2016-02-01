using System;


namespace Sequence
{

    /// <summary>
    /// 有
    /// </summary>
    public class StackFactory<T> where T : IComparable<T>
    {
        public static Stack<T> Generate()
        {
            return new StackVectorImpl<T>();
        }
    }
}
