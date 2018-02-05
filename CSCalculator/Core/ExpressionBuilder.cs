using System;
using System.Collections.Generic;
using System.Text;

namespace CSCalculator.Core
{
    public class ExpressionBuilder
    {
        private static StringBuilder ExpBuilder;

        public static void Add(string Value)
        {
            ExpBuilder.Append(Value);
        }

        public static void Remove(int Index)
        {
            ExpBuilder.Remove(Index, 1);
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
