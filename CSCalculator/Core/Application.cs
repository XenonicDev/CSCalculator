using System;
using System.Collections.Generic;
using System.Text;

namespace CSCalculator.Core
{
    public class Application
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

        public static void FindLHSAndRHS(string Expression, int TokenIndex, out int LowerIndex, out int UpperIndex, out decimal LHS, out decimal RHS)
        {
            LowerIndex = 0;  // Lower Bounds of Substitution
            UpperIndex = Expression.Length - 1;  // Upper Bounds of Substitution
            LHS = 0;
            RHS = 0;

            int LHSOffset = TokenIndex;
            int RHSOffset = TokenIndex;

            if (Expression[TokenIndex - 1] == ' ')
            {
                --LHSOffset;
            }

            bool EnteredValueBlock = false;

            for (int Iter = LHSOffset; Iter >= 0; --Iter)
            {
                // Finish if it's the End of the Expression.
                if (Iter > 0)
                {
                    // Inside the LHS, Track it and Continue.
                    if (((int)Expression[Iter] >= 48 && (int)Expression[Iter] <= 57) || (int)Expression[Iter] == 46)
                    {
                        EnteredValueBlock = true;

                        continue;
                    }

                    else
                    {
                        // If We Haven't Yet Started the RHS.
                        if (!EnteredValueBlock)
                        {
                            continue;
                        }
                    }
                }

                // Beginning of Expression or Found End of LHS.
                LowerIndex = Iter;
                LHS = Convert.ToDecimal(Expression.Substring(Iter, LHSOffset - Iter));

                break;
            }

            if (Expression[TokenIndex + 1] == ' ')
            {
                ++RHSOffset;
            }

            EnteredValueBlock = false;

            for (int Iter = RHSOffset; Iter <= Expression.Length - 1; ++Iter)
            {
                // Finish if it's the End of the Expression.
                if (Iter < Expression.Length - 1)
                {
                    // Inside the RHS, Track it and Continue.
                    if (((int)Expression[Iter] >= 48 && (int)Expression[Iter] <= 57) || (int)Expression[Iter] == 46)
                    {
                        EnteredValueBlock = true;

                        continue;
                    }

                    else
                    {
                        // If We Haven't Yet Started the RHS.
                        if (!EnteredValueBlock)
                        {
                            continue;
                        }
                    }
                }

                // End of Expression or Found End of RHS.
                UpperIndex = Iter;
                RHS = Convert.ToDecimal(Expression.Substring(RHSOffset + 1, Iter - RHSOffset));

                break;
            }
        }

        public static decimal SolveOperation(char Operation, decimal LHS, decimal RHS)
        {
            switch (Operation)
            {
                case '+':
                    return LHS + RHS;
                case '-':
                    return LHS - RHS;
                case '*':
                    return LHS * RHS;
                case '/':
                    return LHS / RHS;
                case '^':
                    return (decimal)Math.Pow((double)LHS, (double)RHS);
                default:
                    throw new Exception("Unknown Operation");
            }
        }

        public static decimal Solve(string Expression)
        {
            // Handle more sub expressions.
            Parse(Expression);

            int LowerIndex = 0;
            int UpperIndex = 0;
            decimal LHS = 0m;
            decimal RHS = 0m;

            // Search Exponents.
            for (int Iter = 0; Iter < Expression.Length; ++Iter)
            {
                if (Expression[Iter] == '^')
                {
                    FindLHSAndRHS(Expression, Iter, out LowerIndex, out UpperIndex, out LHS, out RHS);

                    StringBuilder ResultExpression = new StringBuilder(Expression.Substring(0, LowerIndex));

                    ResultExpression.Append(SolveOperation(Expression[Iter], LHS, RHS).ToString());

                    // Only Add Rest of Expression if There's Something to Add.
                    if (UpperIndex != Expression.Length - 1)
                    {
                        ResultExpression.Append(Expression.Substring(UpperIndex, Expression.Length - UpperIndex));
                    }

                    // Replace Original Expression.
                    Expression = ResultExpression.ToString();

                    // Reset Iterator, Research the Expression.
                    Iter = 0;
                }
            }

            // Search Multiplication/Division.
            for (int Iter = 0; Iter < Expression.Length; ++Iter)
            {
                if (Expression[Iter] == '*' || Expression[Iter] == '/')
                {
                    FindLHSAndRHS(Expression, Iter, out LowerIndex, out UpperIndex, out LHS, out RHS);

                    StringBuilder ResultExpression = new StringBuilder(Expression.Substring(0, LowerIndex));

                    ResultExpression.Append(SolveOperation(Expression[Iter], LHS, RHS).ToString());

                    // Only Add Rest of Expression if There's Something to Add.
                    if (UpperIndex != Expression.Length - 1)
                    {
                        ResultExpression.Append(Expression.Substring(UpperIndex, Expression.Length - UpperIndex));
                    }

                    // Replace Original Expression.
                    Expression = ResultExpression.ToString();

                    // Reset Iterator, Research the Expression.
                    Iter = 0;
                }
            }

            // Search Addition/Subtraction.
            for (int Iter = 0; Iter < Expression.Length; ++Iter)
            {
                if (Expression[Iter] == '+' || Expression[Iter] == '-')
                {
                    FindLHSAndRHS(Expression, Iter, out LowerIndex, out UpperIndex, out LHS, out RHS);

                    StringBuilder ResultExpression = new StringBuilder(Expression.Substring(0, LowerIndex));

                    ResultExpression.Append(SolveOperation(Expression[Iter], LHS, RHS).ToString());

                    // Only Add Rest of Expression if There's Something to Add.
                    if (UpperIndex != Expression.Length - 1)
                    {
                        ResultExpression.Append(Expression.Substring(UpperIndex, Expression.Length - UpperIndex));
                    }

                    // Replace Original Expression.
                    Expression = ResultExpression.ToString();

                    // Reset Iterator, Research the Expression.
                    Iter = 0;
                }
            }

            return Convert.ToDecimal(Expression);
        }
    }
}
