using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{
    public static class Utility
    {
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
