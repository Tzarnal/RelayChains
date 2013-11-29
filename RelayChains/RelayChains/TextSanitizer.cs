using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RelayChains
{
    public class TextSanitizer
    {

        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = RemoveUrls(input);
            input = StripExtranousSymbols(input);            
            input = FixMiscelanious(input);
            input = FixInputEnds(input);
            
            return input;
        }

        public static string StripExtranousSymbols(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = Regex.Replace(input, @"[,()&:;']", String.Empty); //remove most extranous symbols
            input = Regex.Replace(input, "[\"]", String.Empty); //remove " too
            return input;
        }

        static string RemoveUrls(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = Regex.Replace(input, @"(?i)\b((?:[a-z][\w-]+:(?:/{1,3}|[a-z0-9%])|www\d{0,3}[.]|[a-z0-9.\-]+[.][a-z]{2,4}/)(?:[^\s()<>]+|\(([^\s()<>]+|(\([^\s()<>]+\)))*\))+(?:\(([^\s()<>]+|(\([^\s()<>]+\)))*\)|[^\s`!()\[\]{};:'"".,<>?«»“”‘’]))", ""); //Remove urls
            
            return input;
        }

        public static string FixInputEnds(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            
            input = input.Trim(); //Remove useless whitespace at the beginning and end of sentence
            
            if (input.Last() != '.' && input.Last() != '!' && input.Last() != '?') //Ensure the last symbol is either a . ! or ?
                input += ".";
            
            input = char.ToUpper(input[0]) + input.Substring(1); //Make first letter a capital letter
            return input;
        }

        public static string FixMiscelanious(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = Regex.Replace(input, @"<[^>]*>", String.Empty); //remove html tags
            input = Regex.Replace(input, @"\s+", " "); //Remove long whitespaces

            return input;
        }
    }
}
