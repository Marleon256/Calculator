using ConsoleApp4.Operators;
using ConsoleApp4.UnaryOperators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.RPN
{
    class RpnResult
    {
        public string rpn;
        public Dictionary<char, double> numbersCoding;
        public Dictionary<char, IBinaryOperator> binaryOperatorsCoding;
        public Dictionary<char, IUnaryOperator> unaryOperatorsCoding;

        public RpnResult(string rpn, Dictionary<char, double> numbersCoding,
            Dictionary<char, IBinaryOperator> binaryOperatorsCoding, Dictionary<char, IUnaryOperator> unaryOperatorsCoding)
        {
            this.rpn = rpn;
            this.numbersCoding = numbersCoding;
            this.binaryOperatorsCoding = binaryOperatorsCoding;
            this.unaryOperatorsCoding = unaryOperatorsCoding;
        }
    }
}
