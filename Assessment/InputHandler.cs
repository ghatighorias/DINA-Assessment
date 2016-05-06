using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assessment
{
    public class InputHandler
    {
        GalecticRomanAssignmentValueHandler galecticHandler;
        MixedAssignmentHandler mixedHandler;

        string questionPattern;
        string mixedAssignmentPattern;
        string galecticalAssignmentPatterm;

        Regex questionRgx;
        Regex mixedAssignmentRgx;
        Regex galecticalAssignmentRgx;

        public InputHandler()
        {
            galecticHandler = new GalecticRomanAssignmentValueHandler();
            mixedHandler = new MixedAssignmentHandler(galecticHandler);

            questionPattern = "^(how much|how many Credits) is [a-zA-Z\\s]+ \\?$";
            galecticalAssignmentPatterm = "^\\w+ is (I|V|X|L|C|D|M)$";
            mixedAssignmentPattern = "^([a-zA-z]+\\s)+is \\d+ Credits$";

            questionRgx = new Regex(questionPattern, RegexOptions.IgnoreCase);
            mixedAssignmentRgx = new Regex(mixedAssignmentPattern, RegexOptions.IgnoreCase);
            galecticalAssignmentRgx = new Regex(galecticalAssignmentPatterm, RegexOptions.IgnoreCase);
        }

        public string ParseInput(String InputLine)
        {
            string result="";

            if (IsGalecticalAssignment(InputLine))
            {
                if (!galecticHandler.ParsedAssignment(InputLine.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries)))
                {
                    result = "Error";
                }

            }
            else if (IsQuestion(InputLine))
            {
                result = ProvideAnswer(InputLine);
            }
            else if (IsMixedAssignment(InputLine))
            {
                string[] fixedstring = InputLine.Replace(" Credits", "").Replace(" is", "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (!mixedHandler.ParsedAssignment(fixedstring))
                {
                    result = "Error";
                }
            }
            else
                return "Error";


            return result;
        }

        private string ProvideAnswer(String input)
        {
            
            string[] bareQuestionParts = input.Replace(" ?", "").Replace("how many Credits is ", "").Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries);
            RomanNumerals queriedGalecticalValue;
            string reconstructedRomanString = "";
            float queriedMaterialValue;
            foreach (var item in bareQuestionParts)
            {
                queriedGalecticalValue = galecticHandler.QueryAssignedValue(item);
                if (RomanNumerals.INVALID != queriedGalecticalValue)
                {
                    reconstructedRomanString += " ";
                    continue;
                }

                if (!mixedHandler.QueryAssignedValue(item, out queriedMaterialValue))
                    return "Error";
            }

            RomanNumeralParser parser = new RomanNumeralParser(reconstructedRomanString);

            return parser.ToString();
        }

        public bool IsQuestion(String input)
        {
            return questionRgx.IsMatch(input);
        }
        public bool IsMixedAssignment(String input)
        {
            return mixedAssignmentRgx.IsMatch(input);
        }
        public bool IsGalecticalAssignment(String input)
        {
            return galecticalAssignmentRgx.IsMatch(input);
        }


    }
}
