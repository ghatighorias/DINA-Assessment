using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{
    /// <summary>
    /// An Abstract class for the Mixed and Galectical to be derived from
    /// </summary>
    public abstract class AssignmentValueHandler
    {
        protected RomanNumeralParser parser;
        /// <summary>
        /// The function make sure any derived class will be able to parse a specific assignment sentence
        /// </summary>
        /// <param name="AssignmentInputs"></param>
        public abstract void ParsedAssignment(params string[] AssignmentInputs);
        
    }

    /// <summary>
    /// Takes care of handling the galectical assignments
    /// </summary>
    public class GalecticRomanAssignmentValueHandler : AssignmentValueHandler
    {
        private Dictionary<string, RomanNumerals> assignmentDictionary;
        public GalecticRomanAssignmentValueHandler()
        {
            assignmentDictionary = new Dictionary<string, RomanNumerals>();
        }

        /// <summary>
        /// Parse a galectical assignment consist of a galectical term and a roman numeral value
        /// </summary>
        /// <param name="AssignmentInputs">An array of string indicating the tokens in the assignment</param>
        public override void ParsedAssignment(params string[] AssignmentInputs)
        {
            RomanNumeralParser parser = new RomanNumeralParser();
            RomanNumerals parsingResult = RomanNumerals.INVALID;

            if (2 != AssignmentInputs.Length && 0 == AssignmentInputs.Length && !Enum.TryParse<RomanNumerals>(AssignmentInputs[1], out parsingResult))
                throw new ArgumentException("Input can not be parsed");

            parsingResult = (RomanNumerals)Enum.Parse(typeof(RomanNumerals), AssignmentInputs[1]);
            assignmentDictionary.Add(AssignmentInputs[0], parsingResult);
        }

        /// <summary>
        /// Check the already assigment for a galectical term and retrived the Roman numeral equivalent
        /// </summary>
        /// <param name="GalecticTerm">the galectical term to be searched</param>
        /// <returns>the equivalent oman numeral letter</returns>
        public RomanNumerals QueryAssignedValue(string GalecticTerm)
        {
            if (assignmentDictionary.ContainsKey(GalecticTerm))
                return assignmentDictionary[GalecticTerm];
            else
                return RomanNumerals.INVALID;
        }
    }

    /// <summary>
    /// Takes care of handling the mixed assignments
    /// </summary>
    public class MixedAssignmentHandler : AssignmentValueHandler
    {
        protected Dictionary<string, float> assignmentDictionary;
        GalecticRomanAssignmentValueHandler galecticRomanAssignmentValueHandler;
        public MixedAssignmentHandler(GalecticRomanAssignmentValueHandler GalecticRomanAssignmentValuveHandlerInstance)
        {
            assignmentDictionary = new Dictionary<string, float>();
            galecticRomanAssignmentValueHandler = GalecticRomanAssignmentValuveHandlerInstance;
            parser = new RomanNumeralParser();
        }

        /// <summary>
        /// Check if the Material requested is previously assigned or not
        /// </summary>
        /// <param name="Materials">The material to request</param>
        /// <returns>A boolean indicating whether the Material is previously assigned or not</returns>
        public bool Contains(string Materials)
        {
            return assignmentDictionary.ContainsKey(Materials);
        }

        /// <summary>
        /// Retriving the mathematical value of a Material for a previously parsed material
        /// </summary>
        /// <param name="Materials">The material to get the value for</param>
        /// <returns>a float indicating the parsed value for the requested Material</returns>
        public float QueryAssignedValue(string Materials)
        {
            try
            {
                return assignmentDictionary[Materials];
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Parse a mixed assignment and keep the assigned athematical value to the material in a dictionary
        /// </summary>
        /// <param name="AssignmentInputs">An array of string containing the tokens of the assignment</param>
        public override void ParsedAssignment(params string[] AssignmentInputs)
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
                            sumOfGalecticalRomanLetters += queriedItem.ToString() + " ";
                            break;
                        case ParsingPhase.INTEGERS:
                            numberAssigned = parsedInteger;
                            break;
                    }
                    previousParsingPhase = currentParsingPhase;
                }

                else
                    throw new ArgumentException("Input can not be parsed");
            } // does not handle two integer
            
            int parsedGalecticalResult = parser.CalculateRomanNumeralSet(Utility.SplitString(sumOfGalecticalRomanLetters, " ", StringSplitOptions.RemoveEmptyEntries));
            assignmentDictionary.Add(AssignmentInputs[variableIndex], numberAssigned / parsedGalecticalResult);
        }

    }   
}
