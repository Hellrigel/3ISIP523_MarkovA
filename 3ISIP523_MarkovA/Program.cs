using System;
using System.Collections.Generic;
using System.Text;

class TextStatistics
{
    public string OriginalText { get; private set; }
    public int WordCount { get; private set; }
    public string ShortestWord { get; private set; }
    public string LongestWord { get; private set; }
    public int SentenceCount { get; private set; }
    public int VowelCount { get; private set; }
    public int ConsonantCount { get; private set; }
    public Dictionary<char, int> LetterFrequency { get; private set; }

   


