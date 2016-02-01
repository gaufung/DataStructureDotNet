using System;

namespace Sequence
{
    public class QueueFactory<T> where T:IComparable<T>
    {
        public static Queue<T> Generate()
        {
            return new QueueListImpl<T>();
        }
    }
}
