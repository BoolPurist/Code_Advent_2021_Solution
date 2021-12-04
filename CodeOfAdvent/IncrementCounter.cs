using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent
{
  public static class IncrementCounter
  {
    public static int GetInrementCountFromInput(int[] input)
    {
      int prevNumber = input[0];
      int numberOfIncrements = 0;
      for (int i = 1; i < input.Length; i++)
      {
        int currentNumber = input[i];
        if (currentNumber > prevNumber)
        {
          numberOfIncrements++;
        }

        prevNumber = currentNumber;
      }
      return numberOfIncrements;
    }


    public static int GetInrementCountVia3Mesure(int[] input)
    {
      int maxLength = input.Length - 2;
      int prev3Sum = Get3Sume(0);
      int increment = 0;

      for (int i = 1; i < maxLength; i++)
      {
        int current3Sum = Get3Sume(i);
        if (current3Sum > prev3Sum)
        {
          increment++;
        }

        prev3Sum = current3Sum;
      }

      return increment;

      int Get3Sume(int startIndex)
      {
        int length = startIndex + 3;
        int sum = 0;
        for (int i = startIndex; i < length; i++)
        {
          sum += input[i];
        }

        return sum;
      }
    }


  }
}
