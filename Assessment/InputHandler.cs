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

        /// <summary>
        /// This Class provides functionality to parse the inputs of the operator
        /// </summary>
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

        /// <summary>
        /// Parsing the input got from the operator based on it being a question, galectical assignment, or a mixed assignment
        /// </summary>
        /// <param name="InputLine">A string contain the input that can be a question, galectical assignment, or a mixed assignment</param>
        /// <param name="outputResult">A string containing the value of what was inquired in the question or an empty in case of galectical or mized assignment</param>
        public void ParseInput(String InputLine, out string outputResult)
        {
            outputResult = "";
            GroupCollection inputGroups;
            try
            {
                if (IsGalecticalAssignment(InputLine))
                {
                    inputGroups = galecticalAssignmentRgx.Matches(InputLine)[0].Groups;
                    galecticHandler.ParsedAssignment(inputGroups[1].Value, inputGroups[2].Value);
                }
                else if (IsMixedAssignment(InputLine))
                {
                    inputGroups = mixedAssignmentRgx.Matches(InputLine)[0].Groups;
                    string[] RomanAndVariablesInsentence = Utility.SplitString(mixedAssignmentRgx.Match(InputLine).Groups[1].Value.Replace(" is", ""), " ", StringSplitOptions.RemoveEmptyEntries);
                    string digitValueOfaterial = mixedAssignmentRgx.Match(InputLine).Groups[3].Value;
                    List<string> elementsToProcess = new List<string>();
                    elementsToProcess.AddRange(RomanAndVariablesInsentence);
                    elementsToProcess.Add(digitValueOfaterial);

                    mixedHandler.ParsedAssignment(elementsToProcess.ToArray());
                }
                else if (IsQuestion(InputLine))
                {
                    inputGroups = questionRgx.Matches(InputLine)[0].Groups;
                    outputResult = ProvideAnswer(inputGroups[2].Value);
                }
                else
                    throw new ArgumentException();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Provide an answer for the question in case the variables are being assigned previously
        /// </summary>
        /// <param name="input">A string containing the question</param>
        /// <returns>A string containing the value of what was inquired in the question</returns>
        private string ProvideAnswer(String input)
        {
            ParsingPhase currentPhase = ParsingPhase.GALECTICALTERM;
            ParsingPhase previousphase = ParsingPhase.NONE;
            int variableCounter = 0;
            RomanNumerals queriedGalecticalTermValue;
            float queriedMixedTermValue = 1;
            bool queryResult;
            string constructedRomanNumeralSet = "";
            float calculatedResult;
            RomanNumeralParser parser = new RomanNumeralParser();

            try
            {
                foreach (var item in Utility.SplitString(input, " ", StringSplitOptions.RemoveEmptyEntries))
                {
                    queriedGalecticalTermValue = galecticHandler.QueryAssignedValue(item);
                    queryResult = mixedHandler.Contains(item);
                    if (queryResult)
                        queriedMixedTermValue = mixedHandler.QueryAssignedValue(item);

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

                
                calculatedResult = parser.CalculateRomanNumeralSet(Utility.SplitString(constructedRomanNumeralSet, " ", StringSplitOptions.RemoveEmptyEntries));

                if (variableCounter > 0)
                    calculatedResult *= queriedMixedTermValue;
            }
            catch (Exception e)
            {
                throw e;
            }

            return calculatedResult.ToString();
        }
    
        /// <summary>
        /// check if the input given is in the correct format of being considered as a question
        /// </summary>
        /// <param name="input">a string contains the question</param>
        /// <returns>boolean indicating whether the input is a question or not</returns>
        private bool IsQuestion(String input)
        {
            return questionRgx.IsMatch(input);
        }
        /// <summary>
        /// check if the input given is in the correct format of being considered as a mixed assignment of galectical term, Material and a number
        /// </summary>
        /// <param name="input">a string contains the mixed assignment</param>
        /// <returns>boolean indicating whether the input is a mixed assignment or not</returns>
        private bool IsMixedAssignment(String input)
        {
            return mixedAssignmentRgx.IsMatch(input);
        }

        /// <summary>
        /// check if the input given is in the correct format of being considered as a Assignment between Roman numbers and Galectical terms
        /// </summary>
        /// <param name="input">a string contains a galectival assignment</param>
        /// <returns>boolean indicating whether the input is a galectical assignment or not</returns>
        private bool IsGalecticalAssignment(String input)
        {
            return galecticalAssignmentRgx.IsMatch(input);
        }


    }
}
