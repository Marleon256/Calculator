using ConsoleApp4.Operators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.BinaryOperators
{
    class BaseBinaryOperator : IBinaryOperator
    {
        public Associativity Associativity { get; private set; }

        public string StringRepresentation { get; private set; }

        public int Priority { get; private set; }

        Func<double, double, double> evaluator;

        public BaseBinaryOperator(Associativity associativity, string stringRepresentation, int priority, Func<double, double, double> evaluator)
        {
            Associativity = associativity;
            StringRepresentation = stringRepresentation;
            Priority = priority;
            this.evaluator = evaluator;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Associativity, StringRepresentation, Priority);
        }

        public override bool Equals(object obj)
        {
            var oper = obj as BaseBinaryOperator;
            return oper != null &&
                   Associativity == oper.Associativity &&
                   StringRepresentation == oper.StringRepresentation &&
                   Priority == oper.Priority;
        }

        public double Evaluate(double firstArgument, double secondArgument)
        {
            return evaluator(firstArgument, secondArgument);
        }
    }
}
