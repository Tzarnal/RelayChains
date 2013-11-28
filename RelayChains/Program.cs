using System;


namespace RelayChains
{
    class Program
    {
        private static Chain _chain;
        
        static void Main(string[] args)
        {
            _chain = new Chain(3);
            IKnowKungFu();

            _chain.Learn("There are now more details on how details are toxic to the lunar setting design space.");
            _chain.Learn("There are now more details on how solars are bland");
            _chain.Learn("There are now more details on how solars are exciting.");
            _chain.Learn("Its agreed that solars are bland because of they are not infernal enough.");
            _chain.Learn("Your Moon Is On Fire!");

            

            //_chain.PrintAssociations();

            while(true)
            {

                Console.WriteLine(">" + TextTools.FixInputEnds(_chain.GenerateSentenceFromSentence(Console.ReadLine())));
            }
            
        }

        private static void IKnowKungFu()
        {
            var file = new System.IO.StreamReader("KungFu.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                _chain.Learn(TextTools.SanitizeInput(line));
            }

            file.Close();

            file = new System.IO.StreamReader("Braaaains.txt");
            while ((line = file.ReadLine()) != null)
            {
                _chain.Learn(TextTools.SanitizeInput(line));
            }

            file.Close();
        }
    }
}
