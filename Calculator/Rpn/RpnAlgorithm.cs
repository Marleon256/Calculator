using ConsoleApp4.Operators;
using ConsoleApp4.RPN;
using ConsoleApp4.UnaryOperators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class RpnAlgorithm
    {

        int currentPosition = 0;
        string expression;
        Stack<char> stack = new Stack<char>();
        StringBuilder resultBuilder;

        Dictionary<char, double> charNumberCoding = new Dictionary<char, double>();

        Dictionary<string, char> binaryOperatorCoding = new Dictionary<string, char>();
        Dictionary<char, IBinaryOperator> charBinaryOperator = new Dictionary<char, IBinaryOperator>();

        Dictionary<string, char> unaryOperatorCoding = new Dictionary<string, char>();
        Dictionary<char, IUnaryOperator> charUnaryOperator = new Dictionary<char, IUnaryOperator>();

        int currentNumber = 45;

        public RpnResult ToRPN(string expressionToParse)
        {
            expression = expressionToParse;
            resultBuilder = new StringBuilder(expressionToParse.Length);
            while (currentPosition < expression.Length)
            {
                HandleNumbers();
                HandleOperators();
                HandleBrackets();
            }
            while (stack.Count > 0)
                resultBuilder.Append(stack.Pop());
            return new RpnResult(resultBuilder.ToString(), charNumberCoding, charBinaryOperator, charUnaryOperator);
        }

        private void HandleNumbers()
        {
            var numberSet = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ',' };
            if (!numberSet.Contains(expression[currentPosition]))
                return;
            var numberBuilder = new StringBuilder();
            while (currentPosition < expression.Length && numberSet.Contains(expression[currentPosition]))
            {
                numberBuilder.Append(expression[currentPosition]);
                currentPosition++;
            }
            var needChar = GetNextChar();
            charNumberCoding[needChar] = double.Parse(numberBuilder.ToString());
            resultBuilder.Append(needChar);
        }

        private void HandleOperators()
        {
            var binaryOperator = ReadBinaryOperator();
            var unaryOperator = ReadUnaryOperator();
            if (unaryOperator != null && binaryOperator != null)
            {
                if (binaryOperator.StringRepresentation.Length > unaryOperator.StringRepresentation.Length)
                {
                    currentPosition += binaryOperator.StringRepresentation.Length;
                    HandleBinaryOperator(binaryOperator);
                }
                else
                {
                    currentPosition += unaryOperator.StringRepresentation.Length;
                    stack.Push(unaryOperatorCoding[unaryOperator.StringRepresentation]);
                }

            }
            if (binaryOperator != null && unaryOperator == null)
            {
                currentPosition += binaryOperator.StringRepresentation.Length;
                HandleBinaryOperator(binaryOperator);
            }
            if (unaryOperator != null && binaryOperator == null)
            {
                currentPosition += unaryOperator.StringRepresentation.Length;
                stack.Push(unaryOperatorCoding[unaryOperator.StringRepresentation]);
            }
        }

        private void HandleBrackets()
        {
            if (currentPosition >= expression.Length)
                return;
            if (expression[currentPosition] == '(')
            {
                currentPosition++;
                stack.Push('(');
            }
            if (expression[currentPosition] == ')')
            {
                currentPosition++;
                var current = stack.Pop();
                while (current != '(')
                {
                    resultBuilder.Append(current);
                    current = stack.Pop();
                }
            }
        }

        private IBinaryOperator ReadBinaryOperator()
        {
            var stringRepresentation = ReturnSame(binaryOperatorCoding.Keys);
            if (stringRepresentation != null)
                return charBinaryOperator[binaryOperatorCoding[stringRepresentation]];
            return null;
        }

        private IUnaryOperator ReadUnaryOperator()
        {
            var stringRepresentation = ReturnSame(unaryOperatorCoding.Keys);
            if (stringRepresentation != null)
                return charUnaryOperator[unaryOperatorCoding[stringRepresentation]];
            return null;
        }

        private string ReturnSame(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                if (currentPosition + key.Length > expression.Length)
                    continue;
                var success = true;
                for (int i = 0; i < key.Length; i++)
                {
                    success &= expression[currentPosition + i] == key[i];
                    if (!success)
                        break;
                }
                if (success)
                {
                    return key;
                }
            }
            return null;
        }

        private void HandleBinaryOperator(IBinaryOperator binaryOperator)
        {
            while (stack.Count > 0)
            {
                var morePriority = false;
                var equalPriority = false;
                var peekOperatorLeftAssociativity = false;
                var unaryOperator = charUnaryOperator.ContainsKey(stack.Peek());
                if (charBinaryOperator.ContainsKey(stack.Peek()))
                {
                    var peekOperator = charBinaryOperator[stack.Peek()];
                    morePriority = peekOperator.Priority > binaryOperator.Priority;
                    equalPriority = peekOperator.Priority == binaryOperator.Priority;
                    peekOperatorLeftAssociativity = peekOperator.Associativity == Associativity.Left;
                }
                if (unaryOperator || morePriority || (equalPriority && peekOperatorLeftAssociativity))
                    resultBuilder.Append(stack.Pop());
                else
                    break;
            }
            stack.Push(binaryOperatorCoding[binaryOperator.StringRepresentation]);
        }

        public void RegisterUnaryOperator(IUnaryOperator unaryOperator)
        {
            var coding = GetNextChar();
            unaryOperatorCoding[unaryOperator.StringRepresentation] = coding;
            charUnaryOperator[coding] = unaryOperator;
        }

        public void RegisterBinaryOperator(IBinaryOperator binaryOperator)
        {
            var coding = GetNextChar();
            binaryOperatorCoding[binaryOperator.StringRepresentation] = coding;
            charBinaryOperator[coding] = binaryOperator;
        }

        private char GetNextChar()
        {
            var currentChar = (char)currentNumber;
            currentNumber++;
            return currentChar;
        }
    }
}
