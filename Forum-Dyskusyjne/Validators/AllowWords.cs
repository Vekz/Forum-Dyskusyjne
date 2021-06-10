using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Forum_Dyskusyjne.Validators
{
    public class AllowWords:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string content = Convert.ToString(value);

            List<string> dissallowedWords = new List<string>(){"dam"}; // TODO make reading from DB

            var res = !(dissallowedWords.Any(s =>
                    {
                        var pattern = @"\b" + Regex.Escape(s) + @"\b";

                        return Regex.IsMatch(content, pattern, RegexOptions.IgnoreCase);
                    }));

            return res;
        }
    }
}