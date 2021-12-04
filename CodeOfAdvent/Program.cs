using System;
using System.Collections.Generic;
using System.Linq;


namespace CodeOfAdvent
{
  class Program
  {

    

    static void Main(string[] args)
    {
      Execute_Day3_Task2_File();


    }

    private static void Execute_Day3_Task2_File()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY3_INPUT);
      var result = new COReport(input);
      Console.WriteLine($"Product: {result.Product}");
      Console.WriteLine($"CO2ScrubberRating: {result.CO2ScrubberRating}");
      Console.WriteLine($"OxygenGeneratorRating: {result.OxygenGeneratorRating}");
    }

    private static void Execute_Day3_Task2_Sub()
    {
      var report = new COReport(new string[]
        {
          "00100",
          "11110",
          "10110",
          "10111",
          "10101",
          "01111",
          "00111",
          "11100",
          "10000",
          "11001",
          "00010",
          "01010"
        }
      );

      Console.WriteLine($"CO2ScrubberRating: {report.CO2ScrubberRating}");
      Console.WriteLine($"OxygenGeneratorRating: {report.OxygenGeneratorRating}");
      Console.WriteLine($"Product: {report.Product}");
    }

    private static void Execute_Day3_Task1_File()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY3_INPUT);
      
      var report = new PowerReport(input);
      Console.WriteLine($"PowerConsumption: {report.PowerConsumption}");
      Console.WriteLine($"GammRate: {report.GammRate}");
      Console.WriteLine($"EpsilonRate: {report.EpsilonRate}");
    }


    private static void Execute_Day3_Task1()
    {
      var report = new PowerReport(InputReader.DAY3_TASK1_START_INPUT, 5);
      Console.WriteLine($"PowerConsumption: {report.PowerConsumption}");
      Console.WriteLine($"GammRate: {report.GammRate}");
      Console.WriteLine($"EpsilonRate: {report.EpsilonRate}");
    }

    private static void Execute_Day2_Task2()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY2_TASK2_FILENAME);
      var submarine = new Submarine();
      submarine.ProcessCommand(input);
      Console.WriteLine($"{nameof(Execute_Day2_Task2)}");
      Console.WriteLine($"Result: {submarine.ProductOfPosition}");
    }

    private static void Execute_Day2_Task2_Simple()
    {
      var submarine = new Submarine();
      submarine.ProcessCommand(InputReader.DAY2_TASK12_START_INPUT);
      Console.WriteLine($"Result: {submarine.ProductOfPosition}");
    }

    private static void Execute_Day2_Task1()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY2_TASK1_FILENAME);
      var submarine = new SubmarineV1();
      submarine.ProcessCommand(input);
      Console.WriteLine($"{nameof(Execute_Day2_Task1)}");
      Console.WriteLine($"Result: {submarine.ProductOfPosition}");
    }

    private static void Execute_Day2_Task1_Simple()
    {
      var submarine = new SubmarineV1();
      submarine.ProcessCommand(InputReader.DAY2_TASK12_START_INPUT);
      Console.WriteLine($"Result: {submarine.ProductOfPosition}");
    }

    private static void Execute_Day1_Task2()
    {
      var output = InputReader.ReadFromLocalFile("2_task.txt");
      var outputInt = InputReader.CastToWholeNumbers(output);

      Console.WriteLine(GetInrementCountVia3Mesure(outputInt));
    }

    private static void PrintSequence<T>(IEnumerable<T> array)
    {

      foreach (T element in array)
      {
        Console.WriteLine(element);
      }
    }



    

    private static int GetInrementCountFromInput(int[] input)
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

    private static int GetInrementCountVia3Mesure(int[] input)
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
