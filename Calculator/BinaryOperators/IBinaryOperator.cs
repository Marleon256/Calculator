using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Operators
{
    interface IBinaryOperator
    {
        Associativity Associativity { get; }
        string StringRepresentation { get; }
        int Priority { get; }
        double Evaluate(double firstArgument, double secondArgument);
    }
}
