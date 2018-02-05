using System;
using System.Collections.Generic;
using System.Text;

namespace CSCalculator.Core
{
    public class ExpressionBuilder
    {
        private StringBuilder ExpBuilder;

        public void Add(char Value)
        {
            ExpBuilder.Append(Value);
        }

        public void RemoveAt(int Index)
        {
            ExpBuilder.Remove(Index, 1);
        }

        public void RemoveLast()
        {
            ExpBuilder.Remove(ExpBuilder.Length - 1, 1);
        }

        public void Clear()
        {
            ExpBuilder.Clear();
        }

        public string GetExpression()
        {
            return ExpBuilder.ToString();
        }
    }
}
