using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.BrokenSyntax
{
  public static class SyntaxParser
  {
    public static readonly Dictionary<char, char> SymbolMapping =
      new()
      {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
        { '<', '>' }
      };

    public static readonly Dictionary<char, int> SymbolClosingErrorMap =
      new()
      {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 }
      };

    public static readonly Dictionary<char, int> CompletionMap =
      new()
      {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 },
      };

    public static int ParseForFirstWrongClosing(string line)
    {

      var expectedClosingSymbols = new Stack<char>();

      for (int i = 0; i < line.Length; i++)
      {
        char currentChar = line[i];

        if (SymbolMapping.TryGetValue(currentChar, out char expectedClosing))
        {
          expectedClosingSymbols.Push(expectedClosing);
        }
        else
        {
          // found close one
          char expectedClosingSymbol = expectedClosingSymbols.Pop();

          if (currentChar != expectedClosingSymbol)
          {
            return SymbolClosingErrorMap[currentChar];
          }
        }
      }

      return 0;
    }

    public static int ParseForAllFirstWrongClosing(string[] codeBlock)
    {
      int sum = 0;
      foreach (string line in codeBlock)
      {
        sum += ParseForFirstWrongClosing(line);
      }

      return sum;

    }

    private static Queue<char> GetLeftOpeningSymbols(string line)
    {

      var foundOpeningSymbols = new Stack<char>();

      for (int i = 0; i < line.Length; i++)
      {
        char currentChar = line[i];

        if (SymbolMapping.ContainsKey(currentChar))
        {
          foundOpeningSymbols.Push(currentChar);
        }
        else
        {
          // found close one
          foundOpeningSymbols.Pop();
        }
      }

      return MapOpeningSymbolsToClosed(foundOpeningSymbols);
    }

    private static Queue<char> MapOpeningSymbolsToClosed(Stack<char> openingSymbols)
    {
      var result = new Queue<char>();
      foreach (char openingSymbolToMap in openingSymbols)
      {
        char mappedSymbol = SymbolMapping[openingSymbolToMap];
        result.Enqueue(mappedSymbol);
      }

      return result;
    }

    public static ulong GetCompletionScore(string line)
    {
      string completion = CreateCompletionFrom(line);

      ulong totalScore = 0;
      for (int i = 0; i < completion.Length; i++)
      {
        char currentSymbol = completion[i];
        int completionBase = CompletionMap[currentSymbol];
        totalScore *= 5;
        totalScore += Convert.ToUInt64(completionBase);
      }

      return totalScore;
    }

    public static ulong GetMiddleScore(string[] codeBlock)
    {
      string[] correctLines = FilterOutCorruptedLines(codeBlock);
      var scores = new ulong[correctLines.Length];
      int length = correctLines.Length;
      for (int i = 0; i < length; i++)
      {
        scores[i] = GetCompletionScore(correctLines[i]);
      }

      Array.Sort(scores);
      int middleIndex = length / 2;      
      ulong middleScore = scores[middleIndex];
      return middleScore;
    }

    public static string CreateCompletionFrom(string line)
    {
      Queue<char> complementSymbol = GetLeftOpeningSymbols(line);
      var textOutput = new StringBuilder(complementSymbol.Count);
      foreach (char symbol in complementSymbol)
      {
        textOutput.Append(symbol);
      }
      return textOutput.ToString();
    }

    public static string[] FilterOutCorruptedLines(string[] lines)
      => lines.Where(oneLine => ParseForFirstWrongClosing(oneLine) == 0).ToArray();
    
  }




}
