using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{
    public static class Utility
    {
        /// <summary>
        /// split a string into an array of string based on a provided seperator
        /// </summary>
        /// <param name="input">A string requested to be splited</param>
        /// <param name="Seperator">Seperator for splitting the input</param>
        /// <param name="SplitOption">Condition of spliting the string regarding the empty cells</param>
        /// <returns></returns>
        public static string[] SplitString(string input, string Seperator, StringSplitOptions SplitOption)
        {
            try
            {
                return input.Split(new string[] { Seperator }, SplitOption);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
