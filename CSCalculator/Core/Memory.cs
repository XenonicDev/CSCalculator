using System;
using System.Collections.Generic;
using System.Collections;

namespace CSCalculator.Core
{
    public struct CExpression
    {
        public string Expr;
        public string Result;

        public CExpression(string InExpr, string InResult)
        {
            Expr = InExpr;
            Result = InResult;
        }
    }

    public class Memory
    {
        // List of CExpressions
        private ArrayList History;

        public void Initialize()
        {
            History = new ArrayList();
        }

        public void Add(CExpression Expr)
        {
            History.Add(Expr);
        }

        public void Clear()
        {
            History.Clear();
        }
    }
}
