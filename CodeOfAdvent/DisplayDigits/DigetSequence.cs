using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.DisplayDigits
{
  public class DigetSequence
  {
    private const int SEGMENT_NUMBER_FOR_1 = 2;
    private const int SEGMENT_NUMBER_FOR_4 = 4;
    private const int SEGMENT_NUMBER_FOR_7 = 3;
    private const int SEGMENT_NUMBER_FOR_8 = 7;

    private readonly static Dictionary<int, int> SIMPLE_DIGITS = new()
    {
      { SEGMENT_NUMBER_FOR_1, 1 },
      { SEGMENT_NUMBER_FOR_4, 4 },
      { SEGMENT_NUMBER_FOR_7, 7 },
      { SEGMENT_NUMBER_FOR_8, 8 }
    };

    private char[] configuration = new char[7];
    private readonly string[] digits;

    public static int GetNumberOfEasyDigits(string[] lines)
    {
      var segmentCount = new int[10];
      foreach (string oneLine in lines)
      {
        string[] uniquePatternsLeftDigits = oneLine.Split(" | ");

        string[] digits = uniquePatternsLeftDigits[1].Split(" ");

        foreach (string oneDigit in digits)
        {
          segmentCount[oneDigit.Length]++;
        }
      }

      int result = segmentCount[SEGMENT_NUMBER_FOR_1] + 
        segmentCount[SEGMENT_NUMBER_FOR_4] + 
        segmentCount[SEGMENT_NUMBER_FOR_7] + 
        segmentCount[SEGMENT_NUMBER_FOR_8];
      return result;
    }

    public static int GetSumOfDigits(string[] lines)
    {
      int total = 0;

      foreach (string oneLine in lines)
      {
        var currentDigitLine = new DigetSequence(oneLine);
        int digitValue = currentDigitLine.GetSumOfDigitSequence();
        total += digitValue;
      }

      return total;
    }
    public int GetSumOfDigitSequence()
    {
      int currentFactor = 1;
      int currrentSum = 0;
      for (int i = digits.Length -1; i != -1; i--)
      {
        int currentDigit = MapToDigit(digits[i]);
        currrentSum += currentDigit * currentFactor;
        currentFactor *= 10;
      }
      return currrentSum;
    }

    public DigetSequence(string input)
    {
      string[] uniquePatternsLeftDigits = input.Split(" | ");
      string[] patterns = uniquePatternsLeftDigits[0].Split(" ");
      digits = uniquePatternsLeftDigits[1].Split(" ");

      SetConfiguration(patterns);
      
    }

    private int MapToDigit(in string signalPackage)
    {
      int packageLength = signalPackage.Length;

      if (SIMPLE_DIGITS.TryGetValue(packageLength, out int simpleDigit))
      {
        return simpleDigit;
      }
      else if (packageLength == 6)
      {
        char missingSegmentInZero = configuration[3];
        if (!signalPackage.Contains(missingSegmentInZero))
        {
          return 0;
        }

        char missingSegmentInNine = configuration[4];
        if (!signalPackage.Contains(missingSegmentInNine))
        {
          return 9;
        }
        else
        {
          return 6;
        }
      }
      else
      {
        // packageLength == 6

        char exitingSegmentInTwo = configuration[4];
        if (signalPackage.Contains(exitingSegmentInTwo))
        {
          return 2;
        }

        char exitingSegmentInFive = configuration[1];

        if (signalPackage.Contains(exitingSegmentInFive))
        {
          return 5;
        }
        else
        {
          return 3;
        }
      }
    }

    private void SetConfiguration(string[] patterns)
    {
      string[] patternsToSimpleDigits = new string[4];
      string[] sixPattern = new string[3];
      int simpleIndex = 0;
      int sixIndex = 0;

      for (int i = 0; i < patterns.Length; i++)
      {
        string currentPattern = patterns[i];
        int patternLength = currentPattern.Length;

        if (SIMPLE_DIGITS.ContainsKey(patternLength))
        {
          patternsToSimpleDigits[simpleIndex++] = currentPattern;
        }
        else if (patternLength == 6)
        {
          sixPattern[sixIndex++] = currentPattern;
        }

      }

      patternsToSimpleDigits = patternsToSimpleDigits.OrderBy(element => element.Length).ToArray();
      var alphapet = CreateAlphapet();
      char charToRemove = 'z';
      var resolveBuffer = new char[2];

      SetCharsForNextResolve(0);

      foreach (char signal in alphapet)
      {
        if (patternsToSimpleDigits[1].Contains(signal))
        {
          charToRemove = signal;
          configuration[0] = signal;
          break;
        }
      }

      alphapet.Remove(charToRemove);
      Resolve(2, 5, resolveBuffer);     
      SetCharsForNextResolve(2);

      Resolve(3, 1, resolveBuffer);
      Resolve(4, 6, alphapet);


      void Resolve(in int indexForNotFound, in int indexForFound, IList<char> buffer)
      {
        foreach (string six in sixPattern)
        {
          if (!six.Contains(buffer[0]))
          {
            configuration[indexForNotFound] = buffer[0];
            configuration[indexForFound] = buffer[1];
          }
          else if (!six.Contains(buffer[1]))
          {
            configuration[indexForNotFound] = buffer[1];
            configuration[indexForFound] = buffer[0];
          }
        }
      }

      void SetCharsForNextResolve(in int indexToSimpleDigit)
      {
        for (int i = 0, j = 0; i < alphapet.Count && j < resolveBuffer.Length; i++)
        {
          char currenChar = alphapet[i];
          if (patternsToSimpleDigits[indexToSimpleDigit].Contains(currenChar))
          {
            resolveBuffer[j++] = currenChar;
            alphapet.RemoveAt(i--);
          }
        }
      }
    }

    private List<char> CreateAlphapet() => new()
    {
      'a',
      'b',
      'c',
      'd',
      'e',
      'f',
      'g'
    };

  }
}
