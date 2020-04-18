using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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

            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
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
