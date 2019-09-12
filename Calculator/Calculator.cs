using ConsoleApp4.RPN;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class Calculator
    {
        public double Evaluate(RpnResult rpnResult)
        {
            var resultStack = new Stack<char>();
            for(int i = 0; i < rpnResult.rpn.Length; i++)
            {
                if (rpnResult.numbersCoding.ContainsKey(rpnResult.rpn[i]))
                    resultStack.Push(rpnResult.rpn[i]);
                if (rpnResult.binaryOperatorsCoding.ContainsKey(rpnResult.rpn[i]))
                {
                    var binartyOperator = rpnResult.binaryOperatorsCoding[rpnResult.rpn[i]];
                    var firstArgument = resultStack.Pop();
                    var secondArgument = resultStack.Peek();
                    var evaluateResult = binartyOperator.Evaluate(
                        rpnResult.numbersCoding[firstArgument],
                        rpnResult.numbersCoding[secondArgument]
                        );
                    rpnResult.numbersCoding[secondArgument] = evaluateResult;
                }
                if (rpnResult.unaryOperatorsCoding.ContainsKey(rpnResult.rpn[i]))
                {
                    var argument = resultStack.Peek();
                    var unaryOperator = rpnResult.unaryOperatorsCoding[rpnResult.rpn[i]];
                    rpnResult.numbersCoding[argument] = unaryOperator.Evaluate(rpnResult.numbersCoding[argument]);
                }
            }
            return rpnResult.numbersCoding[resultStack.Peek()];
        }
    }
}
