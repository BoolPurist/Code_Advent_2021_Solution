using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.ChitonMaze
{
  public class ChitonMazeInstance
  {
    private int[,] _maze;
    public int Height { get; private set; }
    public int Width { get; private set; }

    public bool Larger { get; private set; }

    public const int LARGER_FACTOR = 5;

    private Dictionary<int, int> _indexToMazeValue = new();

    private int _baseWidth;
    private int _baseHeight;

    Dictionary<int, int> _coordinateResults = new();
    
    private int GetterForMaze(in int y, in int x)
    {
      int xOrder = x / _baseWidth;
      int yOrder = y / _baseHeight;
      int addFactor = xOrder + yOrder;
      int interpolatedValue = _maze[y % _baseWidth, x % _baseHeight] + addFactor;
      return interpolatedValue > 9 ? interpolatedValue - 9 : interpolatedValue;
    }

    public ChitonMazeInstance(string[] lines, bool larger = false)
    {
      int newWidth = lines[0].Length;
      int newHeight = lines.Length;
      
      _baseWidth = newWidth;
      _baseHeight = newHeight;
      
      Larger = larger;

      _maze = new int[newHeight, newWidth];
      Width = newWidth;
      Height = newHeight;

      for (int y = 0; y < newHeight; y++)
      {
        for (int x = 0; x < newHeight; x++)
        {
          _maze[y, x] = lines[y][x] - '0';
        }
      }

      if (Larger)
      {
        Width *= LARGER_FACTOR;
        Height *= LARGER_FACTOR;
      }
      
    }

    private record Path(int RiskSum, int LastWidth, int LastHeight);

    public override string ToString()
    {
      var outputBuilder = new StringBuilder();

      for (int y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          outputBuilder.Append(GetterForMaze(y, x));
        }
        outputBuilder.AppendLine();
      }

      return outputBuilder.ToString();
    }

    public int GetLowestRisk()
    {
      var allPathsToWalk = new Queue<Path>();
      var riskMatrix = new int[Height, Width];

      int QueueCount = 0;

      for (int y = 0; y < riskMatrix.GetLength(0); y++)
      {
        for (int x = 0; x < riskMatrix.GetLength(1); x++)
        {
          riskMatrix[y, x] = int.MaxValue;
        }
      }
      
      
      allPathsToWalk.Enqueue(new Path(0, 0, 0));
      int TargetHeight = Height - 1;
      int TargetWidth = Width - 1;      

      do
      {
        Path currentPath = allPathsToWalk.Dequeue();

        int newHeightBottom = currentPath.LastHeight + 1;
        int newWidthRight = currentPath.LastWidth + 1;
        int newWidthLeft = currentPath.LastWidth - 1;
        int newHeightUp = currentPath.LastHeight - 1;

        bool isBottomFree = newHeightBottom < Height;
        bool isRightFree = newWidthRight < Width;
        bool isUpFree = newHeightUp > -1;
        bool isLeftFree = newWidthLeft > -1;
        
        int lowestRisk = riskMatrix[TargetHeight, TargetWidth];

       
        if (currentPath.LastHeight == TargetHeight && currentPath.LastWidth == TargetWidth)
        {          
          riskMatrix[TargetHeight, TargetWidth] = currentPath.RiskSum < lowestRisk
            ? currentPath.RiskSum : lowestRisk;
        }
        else
        {
          if (isBottomFree)
          {
            InspectFieldIfNeeded(
              newRiskSum => new Path(newRiskSum, currentPath.LastWidth, newHeightBottom),
              () => GetterForMaze(newHeightBottom, currentPath.LastWidth),
              () => riskMatrix[newHeightBottom, currentPath.LastWidth],
              newRiskSum => riskMatrix[newHeightBottom, currentPath.LastWidth] = newRiskSum
            );
          }
          if (isRightFree)
          {
            InspectFieldIfNeeded(
              newRiskSum => new Path(newRiskSum, newWidthRight, currentPath.LastHeight),
              () => GetterForMaze(currentPath.LastHeight, newWidthRight),
              () => riskMatrix[currentPath.LastHeight, newWidthRight],
              newRiskSum => riskMatrix[currentPath.LastHeight, newWidthRight] = newRiskSum
            );
          }
          if (isLeftFree)
          {
            InspectFieldIfNeeded(
              newRiskSum => new Path(newRiskSum, newWidthLeft, currentPath.LastHeight),
              () => GetterForMaze(currentPath.LastHeight, newWidthLeft),
              () => riskMatrix[currentPath.LastHeight, newWidthLeft],
              newRiskSum => riskMatrix[currentPath.LastHeight, newWidthLeft] = newRiskSum
            );
          }
          if (isUpFree)
          {
            InspectFieldIfNeeded( 
              newRiskSum => new Path(newRiskSum, currentPath.LastWidth, newHeightUp),
              () => GetterForMaze(newHeightUp, currentPath.LastWidth),
              () => riskMatrix[newHeightUp, currentPath.LastWidth],
              newRiskSum => riskMatrix[newHeightUp, currentPath.LastWidth] = newRiskSum
              );           
          }

          

          void InspectFieldIfNeeded(
            Func<int,Path> constructor,
            Func<int> mazeGetter, 
            Func<int> riskMatrixGetter, 
            Action<int> riskMatrixSetter
            )
          {
            int newRiskSum = currentPath.RiskSum + mazeGetter();

            if (riskMatrixGetter() > newRiskSum)
            {
              riskMatrixSetter(newRiskSum);
              allPathsToWalk.Enqueue(constructor(newRiskSum));
              QueueCount++;
            }
          }

        }



        

      } while (allPathsToWalk.Count > 0);

      Console.WriteLine($"QueueCount: {QueueCount}");

      return riskMatrix[TargetHeight, TargetWidth];
    }

  }
}
