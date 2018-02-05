using System;
using System.Collections.Generic;
using System.Collections;

namespace CSCalculator.Core
{
    struct CExpression
    {
        public string Expr;
        public string Result;

        public CExpression(string InExpr, string InResult)
        {
            Expr = InExpr;
            Result = InResult;
        }
    }

    class Memory
    {
        // List of CExpressions
        public static ArrayList History;
    }
}
