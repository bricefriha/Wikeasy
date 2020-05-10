using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using TMDbLib.Objects.General;

namespace Wikeasy.Helpers
{
    public class WkeToolbox
    {
        /// <summary>
        /// Function to control the input cases
        /// </summary>
        /// <param name="input">input from search bar</param>
        /// <returns>the input in title case</returns>
        public static string FiltringInputSearch(string input)
        {
            string result;
            // Lower input cases
            //input = input.ToLower();

            // If there's a content wrapped btw parentheses
            if (input.Contains("(") && input.Contains(")"))
            {
                var inputSplit = input.Split('(', ')');

                // Get things outside of the parenthesis
                var titlePart = inputSplit[0];

                // Extract parenthesis content
                var precisionPart = inputSplit[1];

                result = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(titlePart.ToLower()) + "(" + precisionPart +")";
            }
            else
            {
                result = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
            }
            

            return result;
        }
        /// <summary>
        /// Remove every html node from a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HtmlEscape(string input)
        {

            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
