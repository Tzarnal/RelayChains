RelayChains
=============
A simple text Markov Chain implementation with configurable window size mostly useful for chatterbots for .net, written in c#.

```C#
Include RelayChains;

var chain = new Chain(3);

chain.Learn("RelayChains takes a number in its constructor, this determines the window size.");
chain.Learn("The amount of words that will be combined and linked to new words.");

chain.Learn(TextSanitizer.SanitizeInput("With TextSanitizer you can remove  things from the text that look bad."));
chain.Learn(TextSanitizer.SanitizeInput("Have no place being stored or can trip up the learning process"));

Console.WriteLine(chain.GenerateSentenceFromSentence("Generate a sentence from a full sentence."));
Console.WriteLine(chain.GenerateSentenceFromWord("OrASingleWord"));
Console.WriteLine(chain.GenerateWordFromSentence("Generate a single word from a sentence"));
Console.WriteLine(chain.GenerateRandomSentence()); //You don't need an input sentence, RelayChains can start from nothing.
```