using System;
using System.Collections.Generic;

namespace Assessment
{
    public class RomanNumeralParser
    {
        /// <summary>
        /// Keeps the track of assigned Roman Numeral letters and their equivalent mathematical values
        /// </summary>
        private List<RomanNumerals> parsedRomanNumerals;

        /// <summary>
        /// This class Provides functionality to handle the conversion of Roman numeral numbers to mathematical numbers
        /// </summary>
        public RomanNumeralParser() { }

        /// <summary>
        /// Calculating the digital value of a Roman numeral series
        /// </summary>
        /// <param name="RomanNumeralLetters">A series of Roman Numeral Number to be clculated</param>
        /// <returns>An Integer indicating the mathematical equivalent of the Roman numeral series</returns>
        public int CalculateRomanNumeralSet(params string[] RomanNumeralLetters)
        {
            int sum = 0;

            try
            {
                ParseRomanNumeralSet(RomanNumeralLetters);
                int firstIndex = 0;
                int secondIndex = 1;

                while (firstIndex < RomanNumeralLetters.Length)
                {
                    if (secondIndex == RomanNumeralLetters.Length) // handling the last letter in the queue
                        sum += AddNumerals(parsedRomanNumerals[firstIndex], RomanNumerals.INVALID);
                    else
                    {
                        if (parsedRomanNumerals[firstIndex] >= parsedRomanNumerals[secondIndex])
                            sum += AddNumerals(parsedRomanNumerals[firstIndex], RomanNumerals.INVALID);
                        else
                        {
                            sum += SubtractNumerals(parsedRomanNumerals[secondIndex], parsedRomanNumerals[firstIndex]);
                            firstIndex++;
                            secondIndex++;
                        }

                    }

                    firstIndex++;
                    secondIndex++;
                }
            }
            catch(ArgumentException e)
            {
                throw e;
            }
            return sum;
        }

        /// <summary>
        /// Parsing the given series of Roman numeral number
        /// </summary>
        /// <param name="RomanNumeralLetters">A series of Roman Numeral Number</param>
        private void ParseRomanNumeralSet(params string[] RomanNumeralLetters)
        {
            RomanNumerals parsedItem;
            parsedRomanNumerals = new List<RomanNumerals>();

            foreach (var item in RomanNumeralLetters)
            {
                if (Enum.TryParse<RomanNumerals>(item, out parsedItem))
                    parsedRomanNumerals.Add(parsedItem);
                else
                    throw new ArgumentException("The input is not a valid Roman Numeral series");
            }
        }

        /// <summary>
        /// Try to parse a series of Roman Numeral letter
        /// </summary>
        /// <param name="RomanNumeralLetters">A series of Roman Numeral Number</param>
        /// <returns>A boolean indicating whether the series is capable of being parsed or not</returns>
        public bool TryParse(params string[] RomanNumeralLetters)
        {
            RomanNumerals parsedItem;

            foreach (var item in RomanNumeralLetters)
            {
                if (!Enum.TryParse<RomanNumerals>(item.Trim(), out parsedItem))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Add the digital value of two Roman numeral number
        /// </summary>
        /// <param name="first">Roman numeral number 1</param>
        /// <param name="second">Roman numeral number 2</param>
        /// <returns>the result of the add operation</returns>
        private int AddNumerals(RomanNumerals first, RomanNumerals second)
        {
            return (int)first + (int)second;
        }

        /// <summary>
        /// Subtract the digital value of two Roman numeral number
        /// </summary>
        /// <param name="first">Roman numeral number 1 which is the base number</param>
        /// <param name="second">Roman numeral number 2 to be subtracted from the first</param>
        /// <returns>the result of the subtract operation</returns>
        private int SubtractNumerals(RomanNumerals first, RomanNumerals second)
        {
            return (int)first - (int)second;
        }
    }
}
