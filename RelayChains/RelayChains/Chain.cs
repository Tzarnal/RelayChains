using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace RelayChains
{    
    public class Chain
    {        
        private Dictionary<ChainKey, ChainLink> _chain = new Dictionary<ChainKey, ChainLink>();        
        private Random _random = new Random();        
        private int _windowSize;

        public Chain(int windowSize)
        {
            _windowSize = windowSize;
        }

        //Learns the content of a given string
        public void Learn(string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
            {
                return;
            }
            
            var sentenceParts = sentence.Split(' ');
            
            //Can't make chains if sentence does not at least have one more word than windowsize we can't make key/link associations
            if (sentenceParts.Length < _windowSize +1)
            {
                return;
            }
            
            for (var i = 0; i < sentenceParts.Length - _windowSize; i++)
            {
                var keyWords = new string[_windowSize];
                
                for (int j = 0; j < _windowSize; j++)
                {
                    keyWords[j] = sentenceParts[i + j];
                }

                var key = new ChainKey(keyWords);
                var link = new ChainLink();
                link.AddWord(sentenceParts[i+_windowSize]);

                _chain.Add(key,link);

            }
        }

       
        //write a list of keys and the assciated words and weights to a file
        public void WriteAssociations(string path="report.txt")
        {
            var file = new StreamWriter(path);
            Console.WriteLine(">Writing associations to: " + path);
            
            foreach (var pair in _chain)
            {
                file.WriteLine(pair.Key.ToString());
                file.Write(pair.Value.Report());                                
            }
            
            Console.WriteLine(">Finished writing to: " + path);
        }

        //Generate a word from a sentence, selecting most relevant word
        public string GenerateWordFromSentence(string sentence)
        {
            
            var relevantWord = FindRelevantWordInSentence(sentence);

            var candidates = _chain.Where(c => c.Key.FirstWord == relevantWord).ToArray();
            
            if (candidates.Length > 0)
            {
                candidates = _chain.Where(c => c.Key.ToString().Contains(relevantWord)).ToArray();
            }

            if (candidates.Length > 0)
            {
                var radomCandidate = candidates[_random.Next(0, candidates.Length)].Key;
                return radomCandidate[1];
            }

            return "";

        }

        //Attempt to generate a sentence from a given word.
        public string GenerateSentenceFromWord(string word, int minLength = 8, int maxLength = 25)
        {
            ChainKey currentCandidate;
            var sentence = "";
            string nextWord;
            var sentenceLength = _random.Next(minLength, maxLength);
            var candidates = _chain.Where(c => c.Key.FirstWord == word).ToArray();            
            var currentKeyParts = new string[_windowSize];
            
            if (candidates.Length > 0)
            {
                currentCandidate = candidates[_random.Next(0, candidates.Length)].Key;                
                for (var i = 0; i < _windowSize; i++)
                {
                    currentKeyParts[i] = currentCandidate[i];
                }
            }
            else
            {
                return "";
            }

            sentence = string.Join(" ", currentKeyParts);
            sentenceLength -= _windowSize;

            do
            {
                nextWord = _chain[currentCandidate].GetRandomLink();
                sentence = string.Format("{0} {1}", sentence, nextWord);

                for (var i = 0; i < _windowSize - 1; i++)
                {
                    currentKeyParts[i] = currentKeyParts[i + 1];
                }
                currentKeyParts[_windowSize - 1] = nextWord;

                //Grab next candidate 
                candidates = _chain.Where(c => c.Key.ToString() == string.Join("",currentKeyParts)).ToArray();                
                if (candidates.Count() == 0)
                {
                    break;   
                }
                currentCandidate = candidates[_random.Next(0, candidates.Length)].Key;
                sentenceLength--;

            } while (nextWord != null && sentenceLength > 0);

            return sentence;
        }

        //Attempt to generate a sentence from a given sentence, if it fails it tries to generate a sentence just form the msot significant word.
        public string GenerateSentenceFromSentence(string sentence, int minLength = 8, int maxLength = 25)
        {
            return GenerateSentenceFromWord(GenerateWordFromSentence(sentence), minLength, maxLength);
        }

        private string FindRelevantWordInSentence(string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                return null;

            string[] chunks = sentence.Split(' ');
            var sortedChunks = from c in chunks
                               orderby c.Length descending
                               select c;

            return sortedChunks.First();
        }
    }
}
