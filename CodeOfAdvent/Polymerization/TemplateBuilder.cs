using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.Polymerization
{
  public class TemplateBuilder
  {
    private Dictionary<string, char> _buildSteps = new();

    private string _startingTemplate;

    private StringBuilder _currentSequence;

    private HashSet<char> _alaphapet = new();

    private Object locker = new object();

    private record NewParts(string LeftPair, string RightPair);

    private Dictionary<string, NewParts> _possibleNewParts = new();

    public TemplateBuilder(string[] input)
    {
      _startingTemplate = input[0];
      _currentSequence = new StringBuilder(_startingTemplate);

      for (int i = 2; i < input.Length; i++)
      {
        string currentLine = input[i];
        string[] inputSeqeunceLeftFromOutput = currentLine.Split(" -> ");
        char left = inputSeqeunceLeftFromOutput[0][0];
        char right = inputSeqeunceLeftFromOutput[0][1];
        char midddle = inputSeqeunceLeftFromOutput[1][0];
        _buildSteps.Add(inputSeqeunceLeftFromOutput[0], midddle);
        _alaphapet.Add(left);
        _alaphapet.Add(right);
        _alaphapet.Add(midddle);
        
        _possibleNewParts.Add($"{left}{right}", new NewParts($"{left}{midddle}", $"{midddle}{right}"));
      }


    }



    public long GetDifferenceBetweenMinAndMax(int steps)
    {
      Dictionary<string, long> pairsToCount = new();
      var symbolCounter = _alaphapet.Cast<char>().ToDictionary(key => key, key => 0L);
      string input = _currentSequence.ToString();
      int length = input.Length - 1;
      for (int i = 0; i < length; i++)
      {
        string letters = input.Substring(i, 2);

        if (pairsToCount.ContainsKey(letters))
        {
          pairsToCount[letters]++;
        }
        else
        {
          pairsToCount.Add(letters, 1);

        }


        symbolCounter[letters[0]]++;
      }
      
      symbolCounter[_currentSequence[length]]++;

      for (int i = 0; i < steps; i++)
      {

        List<KeyValuePair<string, long>> previousPairs = pairsToCount.ToList();
        previousPairs = previousPairs.Where(pair => pair.Value != 0).ToList();
        pairsToCount.Clear();
        foreach (KeyValuePair<string, long> letterPair in previousPairs)
        {
          long number = letterPair.Value;
          string letters = letterPair.Key;

          char insertedChar = _buildSteps[letters];
          symbolCounter[insertedChar] += number;

          NewParts toAdd = _possibleNewParts[letters];

          if (!pairsToCount.TryAdd(toAdd.LeftPair, number))
          {
            pairsToCount[toAdd.LeftPair] += number;
          }
          if (!pairsToCount.TryAdd(toAdd.RightPair, number))
          {
            pairsToCount[toAdd.RightPair] += number;
          }
        }
      }

      long min = symbolCounter.Min(pair => pair.Value);
      long max = symbolCounter.Max(pair => pair.Value);

      return max - min;
    }
    public int GetResultBy(int steps)
    {
      var counting = new Dictionary<char, int>(_alaphapet
          .Cast<char>()
          .ToDictionary(key => key, key => 0)
          );

      string input = _currentSequence.ToString();
      int runsToDo = _currentSequence.Length - 1;


      for (int i = 0, lengthLoop = _currentSequence.Length - 1; i < lengthLoop; i++)
      {

        string word = input.Substring(i, 2);
        counting[word[0]]++;
        Invoke(word, 1);
        


        void Invoke(in string word, in int step)
        {

          char insertedChar = _buildSteps[word];
          counting[insertedChar]++;

          if (steps > step)
          {
            Invoke($"{word[0]}{insertedChar}", step + 1);

            Invoke($"{insertedChar}{word[1]}", step + 1);          
          }

        }
      }

      counting[_currentSequence[^1]]++;

      int min = counting.Min(pair => pair.Value);
      int max = counting.Max(pair => pair.Value);

      return max - min;


    }


    public override string ToString() => _currentSequence.ToString();

  }
}
