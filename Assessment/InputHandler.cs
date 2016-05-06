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

            questionPattern = "^(how much|how many Credits) is ([a-zA-Z\\s]+) \\?$";
            galecticalAssignmentPatterm = "^(\\w+) is (I|V|X|L|C|D|M)$";
            mixedAssignmentPattern = "^(([A-za-z]+\\s)+is) (\\d+) Credits$";

            questionRgx = new Regex(questionPattern, RegexOptions.IgnoreCase);
            mixedAssignmentRgx = new Regex(mixedAssignmentPattern, RegexOptions.IgnoreCase);
            galecticalAssignmentRgx = new Regex(galecticalAssignmentPatterm, RegexOptions.IgnoreCase);
        }

        public string ParseInput(String InputLine)
        {
            string result="";
            GroupCollection inputGroups;

            if (IsGalecticalAssignment(InputLine))
            {
                inputGroups = galecticalAssignmentRgx.Matches(InputLine)[0].Groups;
                if (!galecticHandler.ParsedAssignment(inputGroups[1].Value, inputGroups[2].Value))
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
                inputGroups = mixedAssignmentRgx.Matches(InputLine)[0].Groups;
                string[] RomanAndVariablesInsentence = mixedAssignmentRgx.Match(InputLine).Groups[1].Value.Replace("is", "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                string digitValueOfaterial = mixedAssignmentRgx.Match(InputLine).Groups[3].Value;
                List<string> elementsToProcess = new List<string>();
                elementsToProcess.AddRange(RomanAndVariablesInsentence);
                elementsToProcess.Add(digitValueOfaterial);

                if (!mixedHandler.ParsedAssignment(elementsToProcess.ToArray()))
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
            ParsingPhase currentPhase = ParsingPhase.GALECTICALTERM;
            ParsingPhase previousphase = ParsingPhase.NONE;
            int variableCounter = 0;
            RomanNumerals queriedGalecticalTermValue;
            float queriedMixedTermValue = 1;
            bool queryResult;
            string constructedRomanNumeralSet="";
         
            foreach (var item in input.Split(new string[] {" "},StringSplitOptions.RemoveEmptyEntries))
            {
                queriedGalecticalTermValue = galecticHandler.QueryAssignedValue(item);
                queryResult = mixedHandler.QueryAssignedValue(item, out queriedMixedTermValue);

                if (RomanNumerals.INVALID != queriedGalecticalTermValue)
                    currentPhase = ParsingPhase.GALECTICALTERM;
                else if (queryResult)
                {
                    currentPhase = ParsingPhase.VARIABLES;
                    variableCounter++;
                }
                else
                    currentPhase = ParsingPhase.NONE;

                if ((int)currentPhase >= (int)previousphase && 1 >= variableCounter)
                {
                    switch (currentPhase)
                    {
                        case ParsingPhase.GALECTICALTERM:
                            constructedRomanNumeralSet += queriedGalecticalTermValue.ToString() + " ";
                            break;
                    }
                }
                else
                    throw new Exception("The order of variable in question is not valid");

                previousphase = currentPhase;
            }

            RomanNumeralParser parser = new RomanNumeralParser();
            float calculatedResult = parser.CalculateRomanNumeralSet(constructedRomanNumeralSet.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

            if (variableCounter>0)
                calculatedResult *= queriedMixedTermValue;

            return calculatedResult.ToString();
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
