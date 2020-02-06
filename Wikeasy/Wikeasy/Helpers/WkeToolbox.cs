using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
