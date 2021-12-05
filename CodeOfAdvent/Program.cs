using System;
using System.Collections.Generic;
using System.Linq;

using CodeOfAdvent.Bingo;
using CodeOfAdvent.HydrorthermalVenture;

namespace CodeOfAdvent
{
  class Program
  {  
    static void Main(string[] args)
    {
      Execute_Day5_Task12_0();



    }
    private static void Execute_Day5_Task12()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY5_INPUT_A);
      var vents = InputVentParser.Parse(input);

      var diagram = new VentLineDiagram(1000, 1000);
      diagram.InsertVentLines(vents);
      Console.WriteLine(diagram.GetNumberOfPointsWithAtLeastOverlapOf(2));
    }

    private static void Execute_Day5_Task12_0()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY5_INPUT_0);
      var vents = InputVentParser.Parse(input);

      var diagram = new VentLineDiagram(10, 10);
      diagram.InsertVentLines(vents);
      Console.WriteLine(diagram);
      Console.WriteLine(diagram.GetNumberOfPointsWithAtLeastOverlapOf(2));

    }

    

    private static void Execute_Day4_Task1_A()
    {
      var game = new BingoGame(InputReader.ReadFromLocalFile(InputReader.DAY4_TASK_A_FILENAME));
      Execute_DAY4_TASK1_X(game, selectedGame => selectedGame.PlayTheGameAndGetWinnerBoard());
    }

    private static void Execute_Day4_Task1_B()
    {
      var game = new BingoGame(InputReader.ReadFromLocalFile(InputReader.DAY4_TASK_B_FILENAME));
      Execute_DAY4_TASK1_X(game, selectedGame => selectedGame.PlayTheGameAndGetWinnerBoard());
    }

    private static void Exectute_Day4_Task2_A()
    {
      var game = new BingoGame(InputReader.ReadFromLocalFile(InputReader.DAY4_TASK_A_FILENAME));
      Execute_DAY4_TASK1_X(game, selectedGame => selectedGame.PlayTheGameAndGetLastWinningBoard());
    }

    private static void Exectute_Day4_Task2_B()
    {
      var game = new BingoGame(InputReader.ReadFromLocalFile(InputReader.DAY4_TASK_B_FILENAME));
      Execute_DAY4_TASK1_X(game, selectedGame => selectedGame.PlayTheGameAndGetLastWinningBoard());
    }



    private static void Execute_DAY4_TASK1_X(BingoGame game, Func<BingoGame,BingoBoard> gameStrat)
    {
      BingoBoard board = gameStrat(game);
      PrintBoard(board);
      Console.WriteLine($"WinningNumber: {game.WinningNumber}");
      Console.WriteLine($"Final Result: {game.FinalResult}");

      static void PrintBoard(BingoBoard board)
      {
        Console.WriteLine("Numbers:");
        Console.WriteLine($"{board.ToString()}");
        Console.WriteLine("Status:");
        Console.WriteLine($"{board.GetBoardStatus()}");
        Console.WriteLine($"Sum of not marked numbers: {board.SumOfUnMarkedFiels}");
      }
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
      var outputInt = SequenceUtility.CastToWholeNumbers(output);

      Console.WriteLine(IncrementCounter.GetInrementCountVia3Mesure(outputInt));
    }


  }
}
