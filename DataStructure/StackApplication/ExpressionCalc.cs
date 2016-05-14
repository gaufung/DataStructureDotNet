using System;
using System.Text;
using Sequence;

namespace StackApplication
{
    class ExpressionCalc
    {
        #region 操作符比较矩阵
        static Char[,] pri ={
           /* +   -   *   /   ^   !   (   )   \0*/
     /*+*/   {'>','>','<','<','<','<','<','>','>'},
     /*-*/   {'>','>','<','<','<','<','<','>','>'},
     /***/   {'>','>','>','>','<','<','<','>','>'},
     /*/*/   {'>','>','>','>','<','<','<','>','>'},
     /*^*/   {'>','>','>','>','>','<','<','>','>'},
     /*!*/   {'>','>','>','>','>','>',' ','>','>'},
     /*（*/  {'<','<','<','<','<','<','<','=',' '},
     /*)*/   {' ',' ',' ',' ',' ',' ',' ',' ',' '},
     /*\0*/  {'<','<','<','<','<','<','<',' ','='},
        };
        static char OrderBetween(char char1, char char2)
        {
            return pri[CharOrder(char1), CharOrder(char2)];
        }

        static int CharOrder(char convertChar)
        {
            if (convertChar == '+') return 0;
            if (convertChar == '-') return 1;
            if (convertChar == '*') return 2;
            if (convertChar == '/') return 3;
            if (convertChar == '^') return 4;
            if (convertChar == '!') return 5;
            if (convertChar == '(') return 6;
            if (convertChar == ')') return 7;
            return 8;
        }
        #endregion

        #region 辅助计算工具
        static int Caluc(int n)
        {
            if (n == 1) return n;
            return n * Caluc(n - 1);
        }

        static int Caluc(int opnd1, char op, int opnd2)
        {
            if (op == '+') return opnd1 + opnd2;
            if (op == '-') return opnd1 - opnd2;
            if (op == '*') return opnd1 * opnd2;
            return opnd1 / opnd2;
        }
        static bool Isdigit(char dight)
        {
            return dight >= '0' && dight < '9';
        }

        static void ReadNumber(ref int counter, string expression, Stack<int> opnd)
        {
            int result = 0;
            while (counter < expression.Length
                && Isdigit(expression[counter]))
            {
                result = result * 10 + Convert.ToInt32(expression[counter].ToString());
                counter++;
            }
            opnd.Push(result);
        }
        #endregion

        public static void Caluc(string expression )
        {
             StringBuilder rpn=new StringBuilder();
            Stack<int> opnd = StackFactory<int>.Generate();
            var optr = StackFactory<Char>.Generate();
            optr.Push('\0');
            int counter = 0;
            while (!optr.Empty)
            {
               // if(counter>expression.Length) break;
                if (counter<expression.Length&&Isdigit(expression[counter]))
                {
                    ReadNumber(ref counter, expression, opnd);
                    rpn.Append(opnd.Top);
                }
                else
                {
                    char compareOp = counter == expression.Length ? '\0' : expression[counter];
                    switch (OrderBetween(optr.Top,compareOp))
                    {
                        case '<':
                            optr.Push(expression[counter]);
                            counter ++;
                            break;
                        case '=':
                            optr.Pop();
                            counter++;
                            break;
                        case '>':
                            char op = optr.Pop();
                            rpn.Append(op);
                            if ('!' == op)
                            {
                                int pOnd = opnd.Pop();
                                opnd.Push(Caluc(pOnd));
                            }
                            else
                            {
                                int pOnd2 = opnd.Pop();
                                int pOnd1 = opnd.Pop();
                                opnd.Push(Caluc(pOnd1,op,pOnd2));
                            }
                            break;
                    }
                }
            }
            Console.WriteLine(expression+" = "+opnd.Pop());
            Console.WriteLine("RPN = "+rpn);
        }
       
    }
}
