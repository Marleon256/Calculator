using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.UnaryOperators
{
    class BaseUnaryOperator : IUnaryOperator
    {
        public string StringRepresentation { get; private set; }

        Func<double, double> evaluator;

        public BaseUnaryOperator(string stringRepresentation, Func<double, double> evaluator)
        {
            StringRepresentation = stringRepresentation;
            this.evaluator = evaluator;
        }

        public double Evaluate(double argument)
        {
            return evaluator(argument);
        }
    }
}
