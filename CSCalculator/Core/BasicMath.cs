using System;
using System.Collections.Generic;
using System.Text;

namespace CSCalculator.Core
{
    class BasicMath
    {
        static decimal Add(decimal LHS, decimal RHS)
        {
            return LHS + RHS;
        }

        static decimal Subtract(decimal LHS, decimal RHS)
        {
            return LHS - RHS;
        }

        static decimal Multiply(decimal LHS, decimal RHS)
        {
            return LHS * RHS;
        }

        static decimal Divide(decimal LHS, decimal RHS)
        {
            return LHS / RHS;
        }

        static decimal Power(decimal Base, decimal Exponent)
        {
            return (decimal)Math.Pow((double)Base, (double)Exponent);
        }
    }
}
