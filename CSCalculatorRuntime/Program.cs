using System;

using CSCalculator.Core;

namespace CSCalculatorRuntime
{
    class Program
    {
        public static void Main(string[] Args)
        {
            string Expression = "7/ 2 + 2 - 5 * 32";

            Console.WriteLine(Application.Solve(Expression));

            Console.Read();
        }
    }
}
