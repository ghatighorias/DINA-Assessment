using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{
  

    enum RomanNumerals
    {
        INVALID = 0,
        I = 1,
        V = 5,
        X = 10,
        L = 50,
        C = 100,
        D = 500,
        M = 1000
    }

    enum InputType
    {
        INVALID,
        ASSIGNMENT,
        CONVERSION,
        QUESTION
    }

    enum ParsingPhase
    {
        NONE,
        GALECTICALTERM,
        VARIABLES,
        INTEGERS,
    }
}
