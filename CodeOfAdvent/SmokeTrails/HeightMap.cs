using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.SmokeTrails
{
  public class HeightMap
  {


    private int[,] map;
    private bool[,] basinMap;

    private int width;
    private int height;
    private int widthLastIndex;
    private int heightLastIndex;

    public int LastIndexInHeight => heightLastIndex;
    public int LastIndexInWidth => widthLastIndex;

    public HeightMap(string[] input)
    {
      width = input[0].Length;
      height = input.Length;
      widthLastIndex = width - 1;
      heightLastIndex = height - 1;



      map = new int[height, width];
      basinMap = new bool[height, width];

      for (int heightIndex = 0; heightIndex < height; heightIndex++)
      {

        for (int widthIndex = 0; widthIndex < width; widthIndex++)
        {
          int currentParsedChar = input[heightIndex][widthIndex] - '0';
          map[heightIndex, widthIndex] = currentParsedChar;
          basinMap[heightIndex, widthIndex] = currentParsedChar != 9;
        }
      }
    }

    public int GetThreeLagestBasins()
    {
      List<int> unsoretedResult = GetAllBasinSize();     
      unsoretedResult.Sort();
      unsoretedResult.Reverse();
      return unsoretedResult.Take(3).Aggregate(1, (sum , basinSize) => sum * basinSize);
    }

    public List<int> GetAllBasinSize()
    {
      var result = new List<int>();

      for (int y = 0; y < height; y++)
      {
        for (int x = 0; x < width; x++)
        {
          if (basinMap[y, x])
          {
            result.Add(GetBasinSizeAt(y, x));
          }
        }
      }

      return result;
    }

    public int GetBasinSizeAt(int startHeight,int startWidth)
    {
      int size = 0;
      var queueOfNextLocations = new Queue<(int, int)>();
      MarkForInspection(startHeight, startWidth);

      do
      {
        (int Y, int X) currentLocation = queueOfNextLocations.Dequeue();

        MarkIfLeftFree(currentLocation);
        MarkIfRightFree(currentLocation);
        MarkIfUpperFree(currentLocation);
        MarkIfBottomFree(currentLocation);

      } while (queueOfNextLocations.Count != 0);

      return size;

      void MarkForInspection(in int Y, in int X)
      {
        basinMap[Y, X] = false;
        queueOfNextLocations.Enqueue((Y, X));
        size++;
      }

      void MarkIfLeftFree(in (int Y, int X) location)
      {
        if (location.X > 0)
        {
          int moved = location.X - 1;
          if (basinMap[location.Y, location.X - 1])
          {            
            MarkForInspection(location.Y, location.X - 1);
          }
        }
       
      }

      void MarkIfRightFree(in (int Y, int X) location)
      {
        if (location.X < widthLastIndex)
        {
          int move = location.X + 1;
          if (basinMap[location.Y, location.X + 1])
          {
            MarkForInspection(location.Y, move);
          }
        }
        
      }

      void MarkIfUpperFree(in (int Y, int X) location)
      {
        if (location.Y > 0)
        {
          int move = location.Y - 1;
          if (basinMap[location.Y - 1, location.X])
          {            
            MarkForInspection(move, location.X);
          }
        }        
      }

      void MarkIfBottomFree(in (int Y, int X) location)
      {
        if (location.Y < heightLastIndex)
        {
          int move = location.Y + 1;
          if (basinMap[location.Y + 1, location.X])
          {
            MarkForInspection(move, location.X);
          }
        }        
      }
    }




    public int  GetTotalHeightOfLowestPoints()
    {
      int lowestLocationSum = 0;

      lowestLocationSum += GetUpperLeftCorner();
      lowestLocationSum += GetUpperRightCorner();
      lowestLocationSum += GetBottomLeftCorner();
      lowestLocationSum += GetBottomRightCorner();

      lowestLocationSum += GetPartialSumOfFram(GetUpperFrameLocation, widthLastIndex);
      lowestLocationSum += GetPartialSumOfFram(GetBottomFrameLocation, widthLastIndex);
      lowestLocationSum += GetPartialSumOfFram(GetLeftFrameLocation, heightLastIndex);
      lowestLocationSum += GetPartialSumOfFram(GetRightFrameLocation, heightLastIndex);

      for (int heightIndex = 1; heightIndex < heightLastIndex; heightIndex++)
      {

        for (int widthIndex = 1; widthIndex < widthLastIndex; widthIndex++)
        {
          lowestLocationSum += TryGetLowestLocation(heightIndex, widthIndex);
        }
      }


      return lowestLocationSum;

      int GetPartialSumOfFram(Func<int, int> frameLogic, in int lasIndex)
      {
        int sum = 0;
        for (int i = 1; i < lasIndex; i++)
        {
          sum += frameLogic(i);
        }
        return sum;
      }
    }
    #region corners

    private int GetUpperLeftCorner()
    {
      int cornerLocation = map[0, 0];
      int rigthLocation = map[0, 1];
      int bottomLocation = map[1, 0];

      return GetHeightIfLowestPoint(cornerLocation, rigthLocation, bottomLocation);
    }

    private int GetBottomLeftCorner()
    {
      int cornerLocation = map[heightLastIndex, 0];
      int rigthLocation = map[heightLastIndex, 1];
      int upperLocation = map[heightLastIndex - 1, 0];

      return GetHeightIfLowestPoint(cornerLocation, rigthLocation, upperLocation);
    }

    private int GetBottomRightCorner()
    {
      int cornerLocation = map[heightLastIndex, widthLastIndex];
      int leftLocation = map[heightLastIndex, widthLastIndex - 1];
      int upperLocation = map[heightLastIndex - 1, widthLastIndex];

      return GetHeightIfLowestPoint(cornerLocation, leftLocation, upperLocation);
    }

    private int GetUpperRightCorner()
    {
      int cornerLocation = map[0, widthLastIndex];
      int leftLocation = map[0, widthLastIndex - 1];
      int bottomLocation = map[1, widthLastIndex];

      return GetHeightIfLowestPoint(cornerLocation, leftLocation, bottomLocation);
    }

    #endregion

    #region Frame locations
    private int GetLeftFrameLocation(int locationHeight)
    {
      int middleLocation = map[locationHeight, 0];
      int upperLocation = map[locationHeight - 1, 0];
      int rightLocation = map[locationHeight, 1];
      int bottomLocation = map[locationHeight + 1, 0];

      return GetHeightIfLowestPoint(middleLocation, upperLocation, rightLocation, bottomLocation);
    }

    private int GetRightFrameLocation(int locationHeight)
    {
      int middleLocation = map[locationHeight, widthLastIndex];
      int bottomLocation = map[locationHeight + 1, widthLastIndex];
      int leftLocation = map[locationHeight, widthLastIndex - 1];
      int upperLocation = map[locationHeight - 1, widthLastIndex];

      return GetHeightIfLowestPoint(middleLocation, upperLocation, leftLocation, bottomLocation);
    }

    private int GetUpperFrameLocation(int locationWidth)
    {
      int middleLocation = map[0, locationWidth];
      int bottomLocation = map[1, locationWidth];
      int leftLocation = map[0, locationWidth - 1];
      int rightLocation = map[0, locationWidth + 1];

      return GetHeightIfLowestPoint(middleLocation, rightLocation, leftLocation, bottomLocation);
    }

    private int GetBottomFrameLocation(int locationWidth)
    {
      int middleLocation = map[heightLastIndex, locationWidth];
      int upperLocation = map[heightLastIndex - 1, locationWidth];
      int leftLocation = map[heightLastIndex, locationWidth - 1];
      int rightLocation = map[heightLastIndex, locationWidth + 1];

      return GetHeightIfLowestPoint(middleLocation, upperLocation, leftLocation, rightLocation);
    }

    #endregion

    private int TryGetLowestLocation(in int height,in int width)
    {
      int upperLocation = map[height - 1, width];
      int leftLocation = map[height, width - 1];
      int middleLocation = map[height, width];
      int bottomLocation = map[height + 1, width];
      int rigthLocation = map[height, width + 1];

      return GetHeightIfLowestPoint(middleLocation, upperLocation, leftLocation, rigthLocation, bottomLocation);
    }

    private int GetHeightIfLowestPoint(int middleLocation, params int[] otherLocations)
      => otherLocations.All(otherLocation => otherLocation > middleLocation) ? middleLocation + 1 : 0;

    
    
  }
}
