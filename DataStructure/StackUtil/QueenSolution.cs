using System;
using System.Text;

namespace Sequence
{
    public class QueenSolution
    {
        public static IList<string> PalceQueeu(int n)
        {
            Stack<Queen> solu = StackVectorImpl<Queen>.StackFactory();
            IList<String> solutions = List<String>.ListFactory();
            Queen q=new Queen(0,0);
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
                    if (q.Y<n)
                    {
                        solu.Push(q);
                        if (n <= solu.Size)
                        {
                            solutions.InsertAsLast(PrintSolution(solu));
                        }
                        q=new Queen(q.X+1,0);
                    }
                }
            } while (0 < q.X || q.Y < n);
            return solutions;
        }

        private static string PrintSolution(Stack<Queen> solu)
        {
            StringBuilder sb=new StringBuilder();
            solu.Foreach(item=>sb.Append(item));
            return sb.ToString();
        }
    }
}
