using System;

using CSCalculator.Core;

namespace CSCalculatorRuntime
{
    class Program
    {
        public static void Main(string[] Args)
        {
            string Expression = "4 + 5";

            Console.WriteLine(Application.Solve(Expression));

            Console.Read();
        }
    }
}
