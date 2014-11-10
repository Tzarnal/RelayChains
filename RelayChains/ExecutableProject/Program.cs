using System;
using RelayChains;

namespace ExecutableProject
{
    class Program
    {
        private static Chain _chain;

        static void Main(string[] args)
        {
            _chain = new Chain(3);
            IKnowKungFu();

            Console.WriteLine("> Finished Building Brain");
            Console.WriteLine();
                                            
            while (true)
            {
                var input = Console.ReadLine();
                
                if (input == "report")
                {
                    _chain.WriteAssociations();
                    Console.WriteLine();
                    continue;
                }

                var response = _chain.GenerateSentenceFromSentence(input);

                if (string.IsNullOrWhiteSpace(response))
                {
                    Console.WriteLine("> Empty Reponse");
                }
                else
                {
                    Console.WriteLine(">" + TextSanitizer.FixInputEnds(response));    
                }

                Console.WriteLine();
            }

        }

        private static void IKnowKungFu()
        {
            var file = new System.IO.StreamReader("KungFu.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                _chain.Learn(TextSanitizer.SanitizeInput(line));
            }

            file.Close();

            file = new System.IO.StreamReader("Braaaains.txt");
            while ((line = file.ReadLine()) != null)
            {
                _chain.Learn(TextSanitizer.SanitizeInput(line));
            }

            file.Close();
        }
    }
}
