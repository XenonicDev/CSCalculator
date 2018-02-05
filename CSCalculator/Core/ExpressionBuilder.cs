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
                    if (ExpBuilder[Iter] != 0 && ExpBuilder[Iter - 1] != ' ')
                    {
                        ExpBuilder = ExpBuilder.Insert(Iter - 1, "*");
                    }
                }
            }
        }

        // Get the Expression in a Processable Format
        public string GetExpression()
        {
            return ExpBuilder.ToString();
        }
        
        // Get the Expression in a Human Readable Format
        public string GetReadableExpression()
        {
            StringBuilder Result = new StringBuilder();
            
            for (int Iter = 0; Iter < ExpBuilder.Length; ++Iter)
            {
                // Negate to '-'
                if (ExpBuilder[Iter] == (char)Symbols.Negate)
                {
                    Result.Append('-');
                }
                
                else
                {
                    Result.Append(ExpBuilder[Iter]);   
                }
            }
            
            return Result.ToString();
        }
    }
}
