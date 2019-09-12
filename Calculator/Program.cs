using ConsoleApp4.BinaryOperators;
using ConsoleApp4.Operators;
using ConsoleApp4.UnaryOperators;
using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {           
            var input = Console.ReadLine();
            PrepareInput(input);

            var binaryOperators = new List<IBinaryOperator>()
            {
                new BaseBinaryOperator(
                    Associativity.Right, "+", 2, (x, y) => y + x
                    ),
                new BaseBinaryOperator(
                    Associativity.Right, "-", 2, (x, y) => y - x
                    ),
                new BaseBinaryOperator(
                    Associativity.Right, "*", 3, (x, y) => y * x
                    ),
                new BaseBinaryOperator(
                    Associativity.Right, "/", 3, (x, y) => y / x
                    ),
                new BaseBinaryOperator(
                    Associativity.Left, "^", 3, (x, y) => Math.Pow(y, x)
                    )
            };
            var unaryOperators = new List<IUnaryOperator>()
            {
                new BaseUnaryOperator(
                    "++", x => x + 1
                    ),
                new BaseUnaryOperator(
                    "--", x => x - 1
                    )
            };

            var rpnAlgorithm = new RpnAlgorithm();

            foreach (var binaryOperator in binaryOperators)
                rpnAlgorithm.RegisterBinaryOperator(binaryOperator);

            foreach (var unaryOperator in unaryOperators)
                rpnAlgorithm.RegisterUnaryOperator(unaryOperator);

            var rpn = rpnAlgorithm.ToRPN(input);
            var calculator = new Calculator();
            Console.WriteLine(calculator.Evaluate(rpn));
        }

        public static string PrepareInput(string input)
        {
            input = input.Replace(" ", "");
            if (input.StartsWith("-") || input.StartsWith("+"))
                input = "0" + input;
            input.Replace("(-", "(0-");
            input.Replace("(+", "(0+");
            return input;
        }
    }
}
