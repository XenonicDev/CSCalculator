using System;
using System.Collections.Generic;
using System.Text;

namespace CSCalculator.Core
{
    class Application
    {
        // Make the Parser's Job Easier
        public static void RegulateExpression(string Expression)
        {
            for (int Iter = 0; Iter < Expression.Length; ++Iter)
            {
                // Case: x(y) -> x*(y)
                if (Expression[Iter] == '(')
                {
                    if (Expression[Iter] != 0 && Expression[Iter - 1] != ' ')
                    {
                        Expression = Expression.Insert(Iter - 1, "*");
                    }
                }
            }
        }

        // Recursively Reduce and Solve an Expression
        public static void Parse(string Expression)
        {
            int StartIndex = -1, EndIndex = -1;
            int SubExpressionCounter = 0;

            // Search for '(' and ')'
            for (int Iter = 0; Iter < Expression.Length; ++Iter)
            {
                if (Expression[Iter] == '(')
                {
                    if (StartIndex != -1)
                    {
                        ++SubExpressionCounter;
                    }

                    else
                    {
                        StartIndex = Iter;
                    }
                }

                else if (Expression[Iter] == ')')
                {
                    if (SubExpressionCounter > 0)
                    {
                        --SubExpressionCounter;

                        continue;
                    }

                    EndIndex = Iter;

                    // Solve this expression and reduce.
                    string Substitue = Solve(Expression.Substring(StartIndex + 1, EndIndex - StartIndex - 1)).ToString();

                    // Begin Building the Reduced Expression.
                    StringBuilder NewExpression = new StringBuilder(Expression.Substring(0, StartIndex - 1));

                    // Add the Substitution.
                    NewExpression.Append(Substitue);

                    // Add the Rest of the Expression.
                    NewExpression.Append(Expression.Substring(EndIndex + 1, Expression.Length - EndIndex - 1));

                    Expression = NewExpression.ToString();

                    // Finished with this Set.
                    StartIndex = -1;
                    EndIndex = -1;
                }
            }
        }

        public static decimal Solve(string Expression)
        {
            decimal Result = 0m;

            // Handle more sub expressions.
            Parse(Expression);

            // Solve the Remaining Expression.


            return Result;
        }

        public static int Main()
        {
            return 0;
        }
    }
}
