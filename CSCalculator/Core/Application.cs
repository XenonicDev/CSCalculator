using System;
using System.Collections.Generic;
using System.Text;

namespace CSCalculator.Core
{
    public class Application
    {
        // Recursively Reduce and Solve an Expression
        public static void Parse(ref string Expression, out string Error)
        {
            Error = "";

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
                    string Substitue = Solve(Expression.Substring(StartIndex + 1, EndIndex - StartIndex - 1), out Error).ToString();

                    // Begin Building the Reduced Expression.
                    StringBuilder NewExpression = new StringBuilder(Expression.Substring(0, StartIndex));

                    // Add the Substitution.
                    NewExpression.Append(Substitue);

                    // Add the Rest of the Expression.
                    if (EndIndex != Expression.Length - 1)
                    {
                        NewExpression.Append(Expression.Substring(EndIndex + 1, Expression.Length - EndIndex - 1));
                    }

                    Expression = NewExpression.ToString();

                    // Finished with this Set.
                    StartIndex = -1;
                    EndIndex = -1;
                }
            }
        }

        public static void FindLHSAndRHS(string Expression, int TokenIndex, out int LowerIndex, out int UpperIndex, out double LHS, out double RHS)
        {
            LowerIndex = 0;  // Lower Bounds of Substitution
            UpperIndex = Expression.Length - 1;  // Upper Bounds of Substitution
            LHS = 0;
            RHS = 0;

            int LHSOffset = TokenIndex;
            int RHSOffset = TokenIndex;

            bool EnteredValueBlock = false;

            if (TokenIndex == 0)
            {
                LHS = 0d;
            }

            else if (Expression[TokenIndex - 1] == ' ')
            {
                --LHSOffset;
            }

            if (Expression[LHSOffset] == (char)Symbols.Sine)
            {
                LHS = 0d;
            }

            else if (Expression[LHSOffset] == (char)Symbols.Cosine)
            {
                LHS = 0d;
            }

            else if (Expression[LHSOffset] == (char)Symbols.Tangent)
            {
                LHS = 0d;
            }

            else if (Expression[LHSOffset] == (char)Symbols.Cosecant)
            {
                LHS = 0d;
            }

            else if (Expression[LHSOffset] == (char)Symbols.Secant)
            {
                LHS = 0d;
            }

            else if (Expression[LHSOffset] == (char)Symbols.Cotangent)
            {
                LHS = 0d;
            }

            else if (Expression[LHSOffset] == (char)Symbols.Logarithm)
            {
                LHS = 0d;
            }

            else if (Expression[LHSOffset] == (char)Symbols.NaturalLogarithm)
            {
                LHS = 0d;
            }

            else
            {
                for (int Iter = LHSOffset; Iter >= 0; --Iter)
                {
                    // Finish if it's the End of the Expression.
                    if (Iter > 0)
                    {
                        // Inside the LHS, Track it and Continue.
                        if (((int)Expression[Iter] >= 48 && (int)Expression[Iter] <= 57) || (int)Expression[Iter] == 46 || (int)Expression[Iter] == (int)Symbols.Negate || (int)Expression[Iter] == (int)Symbols.Exponential)
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
                    LowerIndex = (Iter == 0 ? Iter : Iter + 1);

                    string LHSExpr = Expression.Substring(LowerIndex, LHSOffset - LowerIndex);

                    LHS = Convert.ToDouble(LHSExpr.Replace((char)Symbols.Negate, '-'));

                    break;
                }
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
                    if (((int)Expression[Iter] >= 48 && (int)Expression[Iter] <= 57) || (int)Expression[Iter] == 46 || (int)Expression[Iter] == (int)Symbols.Exponential)
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
                UpperIndex = (Iter == Expression.Length - 1 ? Iter : Iter - 1);

                string RHSExpr = Expression.Substring(RHSOffset + 1, UpperIndex - RHSOffset);

                // Catch for Case: x-~y
                RHS = Convert.ToDouble(RHSExpr.Replace((char)Symbols.Negate, '-'));

                break;
            }
        }

        public static double SolveOperation(char Operation, double LHS, double RHS)
        {
            switch (Operation)
            {
                case (char)Symbols.Add:
                    return LHS + RHS;
                case (char)Symbols.Subtract:
                    return LHS - RHS;
                case (char)Symbols.Multiply:
                    return LHS * RHS;
                case (char)Symbols.Divide:
                    return LHS / RHS;
                case (char)Symbols.Caret:
                    return Math.Pow(LHS, RHS);
                case (char)Symbols.Sine:
                    return Math.Sin(RHS);
                case (char)Symbols.Cosine:
                    return Math.Cos(RHS);
                case (char)Symbols.Tangent:
                    return Math.Tan(RHS);
                case (char)Symbols.Cosecant:
                    return Math.Asin(RHS);
                case (char)Symbols.Secant:
                    return Math.Acos(RHS);
                case (char)Symbols.Cotangent:
                    return Math.Atan(RHS);
                case (char)Symbols.Logarithm:
                    return Math.Log10(RHS);
                case (char)Symbols.NaturalLogarithm:
                    return Math.Log(RHS);
                case (char)Symbols.Root:
                    return Math.Pow(LHS, 1d / RHS);
                default:
                    throw new Exception("Unknown Operation");
            }
        }

        public static double Solve(string Expression, out string Error)
        {
            Error = "";

            double Result;

            try
            {
                // Handle more sub expressions.
                Parse(ref Expression, out Error);

                int LowerIndex = 0;
                int UpperIndex = 0;
                double LHS = 0d;
                double RHS = 0d;

                // Search Special Functions
                for (int Iter = 0; Iter < Expression.Length; ++Iter)
                {
                    bool FoundFunc = false;

                    if (Expression[Iter] == (char)Symbols.Sine)
                    {
                        FoundFunc = true;
                    }

                    else if (Expression[Iter] == (char)Symbols.Cosine)
                    {
                        FoundFunc = true;
                    }

                    else if (Expression[Iter] == (char)Symbols.Tangent)
                    {
                        FoundFunc = true;
                    }

                    else if (Expression[Iter] == (char)Symbols.Cosecant)
                    {
                        FoundFunc = true;
                    }

                    else if (Expression[Iter] == (char)Symbols.Secant)
                    {
                        FoundFunc = true;
                    }

                    else if (Expression[Iter] == (char)Symbols.Cotangent)
                    {
                        FoundFunc = true;
                    }

                    else if (Expression[Iter] == (char)Symbols.Logarithm)
                    {
                        FoundFunc = true;
                    }

                    else if (Expression[Iter] == (char)Symbols.NaturalLogarithm)
                    {
                        FoundFunc = true;
                    }

                    if (FoundFunc)
                    {
                        FindLHSAndRHS(Expression, Iter, out LowerIndex, out UpperIndex, out LHS, out RHS);

                        StringBuilder ResultExpression = new StringBuilder(Expression.Substring(0, LowerIndex));

                        ResultExpression.Append(SolveOperation(Expression[Iter], LHS, RHS).ToString());

                        // Only Add Rest of Expression if There's Something to Add.
                        if (UpperIndex != Expression.Length - 1)
                        {
                            ResultExpression.Append(Expression.Substring(UpperIndex + 1, Expression.Length - UpperIndex - 1));
                        }

                        // Replace Original Expression.
                        Expression = ResultExpression.ToString();

                        // Reset Iterator, Research the Expression.
                        Iter = 0;
                    }
                }

                // Search Carets.
                for (int Iter = 0; Iter < Expression.Length; ++Iter)
                {
                    if (Expression[Iter] == (char)Symbols.Caret)
                    {
                        FindLHSAndRHS(Expression, Iter, out LowerIndex, out UpperIndex, out LHS, out RHS);

                        StringBuilder ResultExpression = new StringBuilder(Expression.Substring(0, LowerIndex));

                        ResultExpression.Append(SolveOperation(Expression[Iter], LHS, RHS).ToString());

                        // Only Add Rest of Expression if There's Something to Add.
                        if (UpperIndex != Expression.Length - 1)
                        {
                            ResultExpression.Append(Expression.Substring(UpperIndex + 1, Expression.Length - UpperIndex - 1));
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
                    if (Expression[Iter] == (char)Symbols.Multiply || Expression[Iter] == (char)Symbols.Divide)
                    {
                        FindLHSAndRHS(Expression, Iter, out LowerIndex, out UpperIndex, out LHS, out RHS);

                        StringBuilder ResultExpression = new StringBuilder(Expression.Substring(0, LowerIndex));

                        ResultExpression.Append(SolveOperation(Expression[Iter], LHS, RHS).ToString());

                        // Only Add Rest of Expression if There's Something to Add.
                        if (UpperIndex != Expression.Length - 1)
                        {
                            ResultExpression.Append(Expression.Substring(UpperIndex + 1, Expression.Length - UpperIndex - 1));
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
                    if (Expression[Iter] == (char)Symbols.Add || (Expression[Iter] == (char)Symbols.Subtract && Iter != 0))
                    {
                        FindLHSAndRHS(Expression, Iter, out LowerIndex, out UpperIndex, out LHS, out RHS);

                        StringBuilder ResultExpression = new StringBuilder(Expression.Substring(0, LowerIndex));

                        ResultExpression.Append(SolveOperation(Expression[Iter], LHS, RHS).ToString());

                        // Only Add Rest of Expression if There's Something to Add.
                        if (UpperIndex != Expression.Length - 1)
                        {
                            ResultExpression.Append(Expression.Substring(UpperIndex + 1, Expression.Length - UpperIndex - 1));
                        }

                        // Replace Original Expression.
                        Expression = ResultExpression.ToString();

                        // Reset Iterator, Research the Expression.
                        Iter = 0;
                    }
                }

                // Perform Last Minute Check for Negations. Catches Negations in Exponentials.
                Expression = Expression.Replace((char)Symbols.Negate, '-');

                Result = Convert.ToDouble(Expression);
            }

            catch (Exception e)
            {
                Error = "Arithmetic Error";

                return 0d;
            }

            return Result;
        }
    }
}
