using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.UnaryOperators
{
    interface IUnaryOperator
    {
        string StringRepresentation { get; }
        double Evaluate(double argument);
    }
}
