using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RelayChains
{    
    public class ChainLink
    {        
        private Dictionary<string, int>  links = new Dictionary<string, int>();
        
        private Random random = new Random();

        //Adds a word to the link or increases the weight of a word that is already included
        public void AddWord(string word)
        {
            if (links.ContainsKey(word))
            {
                links[word]++;
            }
            else
            {
                links.Add(word,1);
            }
        }

        //Returns the most relevant Link, the link with the highest numeric weight.
        public string GetLink()
        {
            var selectedWord = from link in links
                               orderby link.Value descending 
                               select link.Key;

            return selectedWord.First();
        }

        //Returns a random word taking weight into account
        public string GetRandomLink()
        {
            string selectedWord = null;

            int totalWeight = links.Values.Sum();

            int randomResult = random.Next(0,totalWeight);

            foreach (KeyValuePair<string, int> entry in links)
            {
                if (randomResult < entry.Value)
                {
                    selectedWord = entry.Key;
                    break;
                }
                randomResult -= entry.Value;
            }

            return selectedWord;
        }

        //Prints the words in the link with their relative weights
        public string Report()
        {
            var report = new StringBuilder();
            
            foreach (KeyValuePair<string, int> entry in links)
            {
                report.AppendFormat("\t {0} - {1}\n", entry.Key, entry.Value);
            }

            return report.ToString();
        }
    }
}