using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Queue
{
    interface IQueue<T>
    {
        void Enqueue(T e);
        T Dequeue();
        T Front { get; set; }
        bool Empty { get; }
    }
}
