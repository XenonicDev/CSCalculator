using System;

using CSCalculator.Core;

namespace CSCalculatorRuntime
{
    class Program
    {
        public static void Main(string[] Args)
        {
            string Expression = "(3 + 2) * 4 / 2 + (4 /4) ^ 2";

            Console.WriteLine(Application.Solve(Expression));

            Console.Read();
        }
    }
}
