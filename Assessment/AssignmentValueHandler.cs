using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{
    public abstract class AssignmentValueHandler
    {
        public RomanNumeralParser parser;
        public abstract bool ParsedAssignment(params string[] AssignmentInputs);
        
    }

    public class GalecticRomanAssignmentValueHandler : AssignmentValueHandler
    {
        private Dictionary<string, RomanNumerals> assignmentDictionary;
        public GalecticRomanAssignmentValueHandler()
        {
            assignmentDictionary = new Dictionary<string, RomanNumerals>();
        }

        public override bool ParsedAssignment(params string[] AssignmentInputs)
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            RomanNumerals parsingResult = RomanNumerals.INVALID;

            if (2 != AssignmentInputs.Length && 0 == AssignmentInputs.Length && !Enum.TryParse<RomanNumerals>(AssignmentInputs[1],out parsingResult))
                return false;

            parsingResult = (RomanNumerals)Enum.Parse(typeof(RomanNumerals), AssignmentInputs[1]);
            assignmentDictionary.Add(AssignmentInputs[0], parsingResult);
            return true;
        }

        public RomanNumerals QueryAssignedValue(string GalecticTerm)
        {
            if (assignmentDictionary.ContainsKey(GalecticTerm))
                return assignmentDictionary[GalecticTerm];
            else
                return RomanNumerals.INVALID;
        }
    }


    public class MixedAssignmentHandler : AssignmentValueHandler
    {
        protected Dictionary<string, float> assignmentDictionary;
        GalecticRomanAssignmentValueHandler galecticRomanAssignmentValueHandler;
        public MixedAssignmentHandler(GalecticRomanAssignmentValueHandler GalecticRomanAssignmentValueHandlerInstance)
        {
            assignmentDictionary = new Dictionary<string, float>();
            galecticRomanAssignmentValueHandler = GalecticRomanAssignmentValueHandlerInstance;
        }

        public bool QueryAssignedValue(string Materials, out float retrivedValue)
        {
            retrivedValue = 0;
            if (assignmentDictionary.ContainsKey(Materials))
            {
                retrivedValue = assignmentDictionary[Materials];
                return true;
            }
            return false;
        }

        public bool TryParsed(params string[] AssignmentInputs)
        {
            RomanNumerals queriedItem;
            Int32 parsedInteger;
            bool integerParsingResult;
            int variableCounter = 0;
            int variableIndex = 0;
            string sumOfGalecticalRomanLetters = "";
            int numberAssigned = 0;
            ParsingPhase previousParsingPhase = ParsingPhase.NONE;
            ParsingPhase currentParsingPhase = ParsingPhase.GALECTICALTERM;

            for (int index = 0; index < AssignmentInputs.Length; index++)
            {
                queriedItem = galecticRomanAssignmentValueHandler.QueryAssignedValue(AssignmentInputs[index]);

                integerParsingResult = Int32.TryParse(AssignmentInputs[index], out parsedInteger);

                if (RomanNumerals.INVALID != queriedItem)
                    currentParsingPhase = ParsingPhase.GALECTICALTERM;
                else if (integerParsingResult)
                    currentParsingPhase = ParsingPhase.INTEGERS;
                else
                {
                    currentParsingPhase = ParsingPhase.VARIABLES;
                    variableIndex = index;
                    variableCounter++;
                }

                if ((int)currentParsingPhase >= (int)previousParsingPhase && variableCounter <= 1)
                {
                    switch (currentParsingPhase)
                    {
                        case ParsingPhase.GALECTICALTERM:
                            sumOfGalecticalRomanLetters += queriedItem.ToString();
                            break;
                        case ParsingPhase.INTEGERS:
                            numberAssigned = parsedInteger;
                            break;
                    }
                    previousParsingPhase = currentParsingPhase;
                }

                else
                    return false;
            } // does not handle two integer

            return true;
        }


        public override bool ParsedAssignment(params string[] AssignmentInputs)
        {
            RomanNumerals queriedItem;
            Int32 parsedInteger;
            bool integerParsingResult;
            int variableCounter = 0;
            int variableIndex = 0;
            string sumOfGalecticalRomanLetters = "";
            int numberAssigned = 0;
            ParsingPhase previousParsingPhase = ParsingPhase.NONE;
            ParsingPhase currentParsingPhase = ParsingPhase.GALECTICALTERM;

            for (int index = 0; index < AssignmentInputs.Length; index++)
            {
                queriedItem = galecticRomanAssignmentValueHandler.QueryAssignedValue(AssignmentInputs[index]);

                integerParsingResult = Int32.TryParse(AssignmentInputs[index], out parsedInteger);

                if (RomanNumerals.INVALID != queriedItem)
                    currentParsingPhase = ParsingPhase.GALECTICALTERM;
                else if (integerParsingResult)
                    currentParsingPhase = ParsingPhase.INTEGERS;
                else
                {
                    currentParsingPhase = ParsingPhase.VARIABLES;
                    variableIndex = index;
                    variableCounter++;
                }

                if ((int)currentParsingPhase >= (int)previousParsingPhase && variableCounter == 1)
                {
                    switch (currentParsingPhase)
                    {
                        case ParsingPhase.GALECTICALTERM:
                            sumOfGalecticalRomanLetters += queriedItem.ToString() + " ";
                            break;
                        case ParsingPhase.INTEGERS:
                            numberAssigned = parsedInteger;
                            break;
                    }
                    previousParsingPhase = currentParsingPhase;
                }

                else
                    return false;
            } // does not handle two integer

            int parsedGalecticalResult = parser.CalculateRomanNumeralSet(sumOfGalecticalRomanLetters.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            assignmentDictionary.Add(AssignmentInputs[variableIndex], numberAssigned / parsedGalecticalResult);

            return true;
        }

        

    }

    
}
