using System;
using System.Collections.Generic;
using System.Text;

namespace CSCalculator.Core
{
    public class ExpressionBuilder
    {
        private static StringBuilder ExpBuilder;

        public static void Add(char Value)
        {
            ExpBuilder.Append(Value);
        }

        public static void RemoveAt(int Index)
        {
            ExpBuilder.Remove(Index, 1);
        }

        public static void RemoveLast()
        {
            ExpBuilder.Remove(ExpBuilder.Length - 1, 1);
        }

        public static void Clear()
        {
            ExpBuilder.Clear();
        }

        public static string GetExpression()
        {
            return ExpBuilder.ToString();
        }
    }
}
