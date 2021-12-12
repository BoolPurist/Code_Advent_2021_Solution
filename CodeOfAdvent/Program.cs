using System;
using System.Collections.Generic;
using System.Linq;

using CodeOfAdvent.Bingo;
using CodeOfAdvent.HydrorthermalVenture;
using CodeOfAdvent.lanternfish;
using CodeOfAdvent.WhaleAndCrabs;
using CodeOfAdvent.DisplayDigits;
using CodeOfAdvent.SmokeTrails;
using CodeOfAdvent.BrokenSyntax;
using CodeOfAdvent.dumboOctopuses;
using CodeOfAdvent.PassagePathing;

namespace CodeOfAdvent
{
  class Program
  {  
    static void Main(string[] args)
    {
      Execute_Day12_Task2();
      
    }

    private static void Execute_Day12_Toy_Task2_1()
      => Execute_Day12_Routine(InputReader.DAY12_TOY_1, true);

    private static void Execute_Day12_Toy_Task2_2()
      => Execute_Day12_Routine(InputReader.DAY12_TOY_2, true);

    private static void Execute_Day12_Task2()
      => Execute_Day12_Routine(InputReader.DAY12, true);

    private static void Execute_Day12_Toy_1()
      => Execute_Day12_Routine(InputReader.DAY12_TOY_1, false);

    private static void Execute_Day12_Toy_2()
      => Execute_Day12_Routine(InputReader.DAY12_TOY_2, false);

    private static void Execute_Day12_Task1()
      => Execute_Day12_Routine(InputReader.DAY12, false);

    private static void Execute_Day12_Routine(string path, bool withJoker)
    {
      string[] input = InputReader.ReadFromLocalFile(path);
      var system = new CaveSystem(input);
      Console.WriteLine(system);
      int result = system.GetNumberOfPathsToEnd(withJoker);
      Console.WriteLine($"Number of paths to end {result}");
    }

    private static void Execute_Day11_Task2_Toy()
      => Execute_Day11_Taks2_Routine(InputReader.DAY11_TOY_2);

    private static void Execute_Day11_Task2()
      => Execute_Day11_Taks2_Routine(InputReader.DAY11);
    private static void Execute_Day11_Taks2_Routine(string path)
    {
      string[] input = InputReader.ReadFromLocalFile(path);
      var grid = new Octopuses(input);
      var result = grid.InvokeStepsUntilSync();
      Console.WriteLine(grid);
      Console.WriteLine($"Number of steps: {result}");
    }

    private static void Execute_Day11_Task1()
      => Execute_Day11_Routine(InputReader.DAY11, 100);
    private static void Execute_Day11_Task1_Toy_2()
      => Execute_Day11_Routine(InputReader.DAY11_TOY_2, 100);
    private static void Execute_Day11_Task1_Toy()
      => Execute_Day11_Routine(InputReader.DAY11_TOY, 2);

    private static void Execute_Day11_Routine(string path, int numberOfSteps)
    {
      string[] input = InputReader.ReadFromLocalFile(path);
      var grid = new Octopuses(input);
      grid.InvokeStepsBy(numberOfSteps);
      Console.WriteLine(grid);
      Console.WriteLine($"Number of flashes: {grid.NumberOfFlashes}");
    }
    private static void Execute_Day10_Task2()
      => Execute_Day10_Task2_Routine(InputReader.DAY10);

    private static void Execute_Day10_Task2_Toy()
      => Execute_Day10_Task2_Routine(InputReader.DAY10_TOY);

    private static void Execute_Day10_Task2_Routine(string path)
    {
      string[] input = InputReader.ReadFromLocalFile(path);
      ulong result = SyntaxParser.GetMiddleScore(input);
      Console.WriteLine($"Middle score: {result}");      
    }

    private static void Execute_Day10_Task1()
      => Execute_Day10_Task1_Routine(InputReader.DAY10);

    private static void Execute_Day10_Tas1_Toy()
      => Execute_Day10_Task1_Routine(InputReader.DAY10_TOY);

    private static void Execute_Day10_Task1_Routine(in string path)
    {
      string[] input = InputReader.ReadFromLocalFile(path);
      Console.WriteLine($"Result: {SyntaxParser.ParseForAllFirstWrongClosing(input)}");
      // Console.WriteLine($"Error score: {SymbolStorage.ParseBlockForWrongClosingChar(input)}");
    }

    private static void Execute_Day9_Task2()
      => Execute_Day9_Task2_Routine(InputReader.DAY9);

    private static void Execute_Day9_Task2_Toy()
      => Execute_Day9_Task2_Routine(InputReader.DAY9_TOY);


    private static void Execute_Day9()
      => Execute_Day9_Task1_Routine(InputReader.DAY9);

    private static void Execute_Day9_Toy()
      => Execute_Day9_Task1_Routine(InputReader.DAY9_TOY);

    private static void Execute_Day9_Task2_Routine(in string path)
    {
      string[] input = InputReader.ReadFromLocalFile(path);
      var map = new HeightMap(input);
      Console.WriteLine($"Result: {map.GetThreeLagestBasins()}");
    }
    private static void Execute_Day9_Task1_Routine(in string path)
    {
      string[] input = InputReader.ReadFromLocalFile(path);
      var map = new HeightMap(input);
      Console.WriteLine($"Result: {map.GetTotalHeightOfLowestPoints()}");
    }

    private static void Execute_Day8_Task2()
    {
      string[] input = InputReader.ReadFromLocalFile(InputReader.DAY8);

      int result = DigetSequence.GetSumOfDigits(input);

      Console.WriteLine($"Result: {result}");
    }

    private static void Execute_Day8_Task2_Toy()
    {
      string[] input = InputReader.ReadFromLocalFile(InputReader.DAY8_TOY);

      int result = DigetSequence.GetSumOfDigits(input);

      Console.WriteLine($"Result: {result}");
    }

    

    private static void Execute_Day8_Task1()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY8);
      var result = DigetSequence.GetNumberOfEasyDigits(input);
      Console.WriteLine($"Result: {result}");
    }

    private static void Execute_Day8_Toy()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY8_TOY);
      var result = DigetSequence.GetNumberOfEasyDigits(input);
      Console.WriteLine($"Result: {result}");
    }

    private static void Execute_Day7_Task2()
    {
      var textInput = InputReader.GetOneLinerInput(InputReader.DAY7, ",");
      var input = SequenceUtility.CastToOneWholeNumberPerLine(textInput);
      var crabFormation = new CrabFormation(input, false);
      Console.WriteLine($"SweetSpot: {crabFormation.SweetSpot}");
      Console.WriteLine($"UsedFuel: {crabFormation.MinimalFuelUsage}");
    }

    private static void Execute_Day7_Task1()
    {
      var textInput = InputReader.GetOneLinerInput(InputReader.DAY7, ",");
      var input = SequenceUtility.CastToOneWholeNumberPerLine(textInput);
      var crabFormation = new CrabFormation(input);
      Console.WriteLine($"SweetSpot: {crabFormation.SweetSpot}");
      Console.WriteLine($"UsedFuel: {crabFormation.MinimalFuelUsage}");
    }

    private static void Execute_Day7_Toy2()
    {
      var input = InputReader.DAY_7_TOY_ARRAY;
      var crabFormation = new CrabFormation(input, false);

      Console.WriteLine($"SweetSpot: {crabFormation.SweetSpot}");
      Console.WriteLine($"UsedFuel: {crabFormation.MinimalFuelUsage}");
    }

    private static void Execute_Day7_Toy()
    {      
      var input = InputReader.DAY_7_TOY_ARRAY; 
      var crabFormation = new CrabFormation(input);

      Console.WriteLine($"SweetSpot: {crabFormation.SweetSpot}");
      Console.WriteLine($"UsedFuel: {crabFormation.MinimalFuelUsage}");
    }


    private static void Execute_Day6_Execute_Task1()
    {
      var tokens = InputReader.GetOneLinerInput(InputReader.DAY5, ",");
      int[] input = SequenceUtility.CastToOneWholeNumberPerLine(tokens);

      var output = FishInterpolation.GetInterpolatedPopulationCount(
        80,
        input,
        7,
        9
        );

      Console.WriteLine($"Result with 80 Days: {output}");

      output = FishInterpolation.GetInterpolatedPopulationCount(
        256,
        input,
        7,
        9,
        10
        );

      
      Console.WriteLine($"Result with 256 Days: {output}");
    }

    private static void Execute_Day6_Execute_0()
    {
      var input = new int[] { 3, 4, 3, 1, 2 };
      var resultAfter18Days = FishInterpolation.GetInterpolatedPopulationCount(18, input, 7, 9);
      Console.WriteLine($"Result after 18: {resultAfter18Days}");
      var resultAfter80Days = FishInterpolation.GetInterpolatedPopulationCount(80, input, 7, 9);
      Console.WriteLine($"Result after 80: {resultAfter80Days}");
      var resultAfter256Days = FishInterpolation.GetInterpolatedPopulationCount(256, input, 7, 9);
      Console.WriteLine($"Result after 256: {resultAfter256Days}");
    }

    

    private static void Execute_Day5_Task12()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY5_INPUT_A);
      var vents = InputVentParser.Parse(input);

      var diagram = new VentLineDiagram(vents);
      
      Console.WriteLine(diagram.GetNumberOfPointsWithAtLeastOverlapOf(2));
    }

    private static void Execute_Day5_Task12_0()
    {
      var input = InputReader.ReadFromLocalFile(InputReader.DAY5_INPUT_0);
      var vents = InputVentParser.Parse(input);

      var diagram = new VentLineDiagram(vents);
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
      var outputInt = SequenceUtility.CastToOneWholeNumberPerLine(output);

      Console.WriteLine(IncrementCounter.GetInrementCountVia3Mesure(outputInt));
    }


  }
}
