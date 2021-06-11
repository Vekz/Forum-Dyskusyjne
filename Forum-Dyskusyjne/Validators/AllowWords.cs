using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Forum_Dyskusyjne.Areas.Admin.Controllers;

namespace Forum_Dyskusyjne.Validators
{
    public class AllowWords:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string content = Convert.ToString(value);

            var disallowedWords = ProhibitedWordsController.ReadStringListFromJson(ProhibitedWordsController.JsonPath);

            var res = !(disallowedWords.Any(s =>
                    {
                        var pattern = @"\b" + Regex.Escape(s) + @"\b";

                        return Regex.IsMatch(content, pattern, RegexOptions.IgnoreCase);
                    }));

            return res;
        }
    }
}