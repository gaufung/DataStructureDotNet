using System;
using System.Text;
using Sequence;

namespace StackApplication
{
    /// <summary>
    /// 使用栈模拟 整数运算
    /// </summary>
    class Program
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
        static char OrderBetween(char char1,char char2)
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
 
        static void Main(string[] args)
        {  
        }

       
    }
}
