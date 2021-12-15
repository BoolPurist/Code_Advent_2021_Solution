using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CodeOfAdvent.Folding
{
  public class TransparentPaper
  {
    private const char EMPTY_SYMBOL = '.';
    private const char TAKEN_SYMBOL = '#';
    private readonly static Regex MatchForCoordinate = new Regex(@"\d+,\d+");
    private readonly static Regex MathForInstruction = new Regex(@".*(?<direction>[y|x])=(?<placeToFold>\d+)");    

    private bool[,] _grid;
    private FoldInstruction[] _instructionsToDo;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public int NumberOfFoldingDone { get; private set; } = 0;

    private record Coordinates(int Y, int X);
    private record FoldInstruction(bool IsXnotY, int FoldPlace);

    public TransparentPaper(string[] input)
    {
      Coordinates[] coordinates = input.Where(line => MatchForCoordinate.IsMatch(line))
        .Select(line => 
        {
          string[] parts = line.Split(',');
          return new Coordinates(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[0]) );
        })
        .ToArray();

      Width = coordinates.Max( element => element.X) + 1;
      Height = coordinates.Max( element => element.Y) + 1;
      
      _grid = new bool[Height, Width];

      foreach (Coordinates onePoint in coordinates)
      {
        _grid[onePoint.Y, onePoint.X] = true;
      }


      _instructionsToDo = input.Skip(coordinates.Length + 1)
        .Select(line => MathForInstruction.Match(line))
        .Where(match => match.Success)
        .Select(match =>
        {
          string direction = match.Groups["direction"].Value;
          int numberOfFolding = Convert.ToInt32(match.Groups["placeToFold"].Value);
          return new FoldInstruction(direction == "x", numberOfFolding);
        }).ToArray();
      

    }

    /// <summary>
    /// This functions assumes that the page is folded along the middle or on right/bottom half
    /// </summary>
    public void InvokeNextFolding()
    {
      FoldInstruction currentInstruction = _instructionsToDo[NumberOfFoldingDone];
      NumberOfFoldingDone++;

      int newWidth = Width;
      int newHeight = Height;
      int foldspace = currentInstruction.FoldPlace;
      int lastIndex;
      
      // Fold horizontal
      if (currentInstruction.IsXnotY)
      {
        newWidth = foldspace;
        lastIndex = Width - 1;

        for (int y = 0; y < Height; y++)
        {
          for (
            int x = GetDistanceBetweenMiddleAndEnd(),
            xRight = lastIndex;
            xRight > foldspace && x < foldspace;            
            )
          {
            _grid[y, x] = _grid[y, x] || _grid[y, xRight];
            x++;
            xRight--;
          }
        }
      }
      else
      {
        // Fold vertical
        newHeight = foldspace;
        lastIndex = Height - 1;        
        

        for (int x = 0; x < Width; x++)
        {
          for (
            int y = GetDistanceBetweenMiddleAndEnd(),
            yBottom = lastIndex;
            yBottom > foldspace && y < foldspace;
            )
          {
            _grid[y, x] = _grid[y, x] || _grid[yBottom, x];
            y++;
            yBottom--;
          }
        }
      }


      Width = newWidth;
      Height = newHeight;
      /// <summary>
      /// Offset if page is folded over right/bottom half and not in the middle
      /// </summary>
      int GetDistanceBetweenMiddleAndEnd()
      {
        int distanceBetweenFoldPlaceAndEnd = lastIndex - foldspace;
        return foldspace - distanceBetweenFoldPlaceAndEnd;
      }
    }

    public int GetCountOfDotsAtEnd()
    {
      while (NumberOfFoldingDone < _instructionsToDo.Length)
      {
        InvokeNextFolding();
      }

      return GetCountOfDots();
    }

    public int GetCountOfDotsOnce()
    {
      InvokeNextFolding();

      return GetCountOfDots();
    }

    private int GetCountOfDots()
    {
      int countOfDots = 0;
      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          countOfDots += _grid[y, x] ? 1 : 0;
        }
      }
      return countOfDots;
    }

    public override string ToString()
    {
      var outputBuilder = new StringBuilder();

      for (int y = 0, height = Height; y < height; y++ )
      {
        for (int x = 0, width = Width; x < width; x++)
        {
          bool isTaken = _grid[y, x];
          char currentSymbol = isTaken ? TAKEN_SYMBOL : EMPTY_SYMBOL;
          outputBuilder.Append(currentSymbol);
        }
        outputBuilder.AppendLine();
      }

      return outputBuilder.ToString();
    }
  }
}
