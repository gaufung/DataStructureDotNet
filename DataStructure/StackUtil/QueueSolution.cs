using System;
using System.Text;

namespace Sequence
{
    public class QueueSolution
    {
        public static IList<string> PalceQueue(int n)
        {
            Stack<Queue> solu = StackFactory<Queue>.Generate();
            IList<String> solutions = List<String>.ListFactory();
            Queue q=new Queue(0,0);
            do
            {
                if (n <= solu.Size || n <= q.Y)
                {
                    q = solu.Pop();
                    q.Y++;
                }
                else
                {
                    while (q.Y<n&&0<=solu.Find(q))
                    {
                        q.Y++;
                    }
                    if (n > q.Y)
                    {
                        solu.Push(q);
                        if (n <= solu.Size)
                        {
                            solutions.InsertAsLast(PrintSolution(solu));
                        }
                        q=new Queue(++q.X,0);
                    }
                }
            } while (0 < q.X || q.Y < n);
            return solutions;
        }

        private static string PrintSolution(Stack<Queue> solu)
        {
            StringBuilder sb=new StringBuilder();
            solu.Travese(item=>sb.Append(item));
            return sb.ToString();
        }
    }
}
