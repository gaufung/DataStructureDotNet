using System;

namespace Sequence
{
    internal class SkipNode<TK,TV> where TK:IComparable<TK>
    {
        public TK Key { get; set; }
        public TV Value { get; set; }

        public SkipNode<TK, TV>[] Forward { get; set; }

        public SkipNode(int level,TK key=default(TK),TV value=default(TV))
        {
            Key = key;
            Value = value;
            Forward=new SkipNode<TK, TV>[level+1];
        }
    }
}
