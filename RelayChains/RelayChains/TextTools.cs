using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RelayChains
{
    public class TextTools
    {

        public static string FindRelevantWordInSentence(string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                return null;
            
            string[] chunks = sentence.Split(' ');
            var sortedChunks = from c in chunks
                               orderby c.Length descending
                               select c;

            return sortedChunks.First();
        }

        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            
            input = StripExtranousSymbols(input);
            input = RemoveUrls(input);
            input = FixMiscelanious(input);
            return FixInputEnds(input);
        }

        public static string StripExtranousSymbols(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = Regex.Replace(input, @"[,()&:;]", String.Empty); //remove most extranous symbols
            input = Regex.Replace(input, "[\"]", String.Empty); //remove " too
            return input;
        }

        static string RemoveUrls(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            input = Regex.Replace(input, @"(https?|ftp|file)\://[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", ""); //Remove urls
            
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
