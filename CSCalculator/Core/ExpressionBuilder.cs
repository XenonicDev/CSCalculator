using System;
using System.Collections.Generic;
using System.Text;

namespace CSCalculator.Core
{
    public class ExpressionBuilder
    {
        private StringBuilder ExpBuilder;

        public void Initialize()
        {
            ExpBuilder = new StringBuilder();
        }

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
        
        public void Format()
        {
            for (int Iter = 0; Iter < ExpBuilder.Length; ++Iter)
            {
                // Case: x(y) -> x*(y)
                if (ExpBuilder[Iter] == '(')
                {
                    if (Iter != 0 && (ExpBuilder[Iter - 1] == ' ' || ((int)ExpBuilder[Iter - 1] >= 48 && (int)ExpBuilder[Iter - 1] <= 57)))
                    {
                        ExpBuilder = ExpBuilder.Insert(Iter, (char)Symbols.Multiply);

                        ++Iter;  // Double Increment to Avoid Rechecking.
                    }
                }
            }
        }

        // Get the Expression in a Processable Format
        public string GetExpression()
        {
            return ExpBuilder.ToString();
        }

        public string ToReadableExpression(string Expression)
        {
            StringBuilder Result = new StringBuilder();

            for (int Iter = 0; Iter < Expression.Length; ++Iter)
            {
                // Negate to '-'
                if (Expression[Iter] == (char)Symbols.Negate)
                {
                    Result.Append('-');
                }

                else if (Expression[Iter] == (char)Symbols.Exponential)
                {
                    Result.Append('ᴇ');
                }

                else
                {
                    Result.Append(Expression[Iter]);
                }
            }

            return Result.ToString();
        }

        // Get the Expression in a Human Readable Format
        public string GetReadableExpression()
        {
            return ToReadableExpression(ExpBuilder.ToString());
        }
    }
}
