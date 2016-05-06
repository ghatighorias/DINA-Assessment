using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{
    class InputHandler
    {
        GalecticRomanAssignmentValueHandler galecticHandler;
        MixedAssignmentHandler mixedHandler;

        public InputHandler()
        {
            galecticHandler = new GalecticRomanAssignmentValueHandler();
            mixedHandler = new MixedAssignmentHandler(galecticHandler);
        }

        public string ParseInput(String InputLine)
        {
            string result="";

            if (isGalecticalAssignment(InputLine))
            {
                if (!galecticHandler.ParsedAssignment(InputLine.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries)))
                {
                    result = "Error";
                }

            }
            else if (isQuestion(InputLine))
            {
                result = ProvideAnswer(InputLine);
            }
            else if (isMixedAssignment(InputLine))
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

        private bool isQuestion(String input)
        {
            return input.StartsWith("how many Credits is", StringComparison.CurrentCultureIgnoreCase);
        }
        private bool isMixedAssignment(String input)
        {
            string[] fixedstring = input.Replace(" Credits", "").Replace(" is", "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            return mixedHandler.TryParsed(fixedstring);//fixedstring.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
        }
        private bool isGalecticalAssignment(String input)
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            string[] elements = input.Split(new string[] { " is " }, StringSplitOptions.RemoveEmptyEntries);
            if (!parser.TryParse(elements[0]) && parser.TryParse(elements[1]))
                return true;

            return false;
        }
    }
}
