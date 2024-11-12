using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _Project.Language.Scripts.Utility
{
    public static class RegexTextFiller
    {
        public static string FillParams(this string txt, Dictionary<string,string> param)
        {
            string pattern = @"\${([^}]+)}"; // ${****}
            Match match = Regex.Match (txt, pattern, RegexOptions.IgnoreCase);
        
            while (match.Success) {
                string word = match.Value;
                string filteredKey = word.Substring (2, word.Length - 3);

                if (param.ContainsKey(filteredKey))
                {
                    txt = txt.Replace (word, param[filteredKey]);
                }
                else
                {
                    txt = txt.Replace(word, "NaN");
                }
                match = match.NextMatch ();
            }

            return txt;
        }
        
        public static bool IsNumeric(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            // Try parsing to int, double, or decimal
            return int.TryParse(str, out _) || 
                   double.TryParse(str, out _) || 
                   decimal.TryParse(str, out _);
        }
    }
}