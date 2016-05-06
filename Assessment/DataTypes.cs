using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{

    /// <summary>
    /// This Enumerator keeps the Roman Numeral number and their value
    /// </summary>
    public enum RomanNumerals
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

    /// <summary>
    /// This Enumerator is for keeping the track of state of parsing a sentence
    /// </summary>    
    public enum ParsingPhase
    {
        NONE,
        GALECTICALTERM,
        VARIABLES,
        INTEGERS,
    }
}
