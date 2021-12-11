using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.dumboOctopuses
{
  public class Octopuses
  {
    public int NumberOfFlashes => _numberOfFlashes;
    private int _numberOfFlashes = 0;
    private int[,] _octopusesGrid;
    public Octopuses(string[] input)
    {
      string firstLine = input[0];

      _octopusesGrid = new int[input.Length, firstLine.Length];

      for (int height = 0; height < input.Length; height++)
      {
        string numbersInLine = input[height];
        for (int width = 0, widthLength = _octopusesGrid.GetLength(1); width < widthLength; width++)
        {
          _octopusesGrid[height, width] = Convert.ToInt32(numbersInLine[width] - '0');
        }
      }
    }

    private void IterateThroughGrid(Action<int,int> actionOnElement, Action<int> actionOnRow = null)
    {
      for (int height = 0, heightLength = _octopusesGrid.GetLength(0); height < heightLength; height++)
      {
        for (int width = 0, widthLength = _octopusesGrid.GetLength(1); width < widthLength; width++)
        {
          actionOnElement(height, width);
        }
        actionOnRow?.Invoke(height);
      }
    }

    private record Coordinates(int Y, int X);
    public void InvokeStep()
    {
      var aboutToFlash = new Queue<Coordinates>();
      var notMarkedForFlash = new bool[_octopusesGrid.GetLength(0), _octopusesGrid.GetLength(1)];
      int gridHeight = _octopusesGrid.GetLength(0);
      int gridWidth = _octopusesGrid.GetLength(1);
      

      IterateThroughGrid(OnOctopuseInvoke);

      while (aboutToFlash.Count > 0)
      {
        Coordinates oneOctopuse = aboutToFlash.Dequeue();
        int y = oneOctopuse.Y;
        int x = oneOctopuse.X;
        int upperY = y - 1;
        int leftX = x - 1;
        int downY = y + 1;
        int rightX = x + 1;
        bool upFree = upperY != -1;
        bool leftFree = leftX != -1;
        bool rightFree = rightX < gridWidth;
        bool downFree = downY < gridHeight;

        SparkTop();
        SparkUpRight();
        SparkRight();
        SparkDownRight();
        SparkDown();
        SparkDownLeft();
        SparkLeft();
        SparkUpLeft();
       
        void SparkTop()
        {
          if (upFree)
          {
            IgniteFlashOnIfNeeded(upperY, x);
          }                   
        }
        void SparkDown()
        {
          if (downFree)
          {
            IgniteFlashOnIfNeeded(downY, x);
          }
        }
        void SparkLeft()
        {
          if (leftFree)
          {
            IgniteFlashOnIfNeeded(y, leftX);
          }
        }
        void SparkRight()
        {
          if (rightFree)
          {
            IgniteFlashOnIfNeeded(y, rightX);
          }
        }
        void SparkUpLeft()
        {
          if (upFree && leftFree)
          {
            IgniteFlashOnIfNeeded(upperY, leftX);
          }
        }
        void SparkUpRight()
        {
          if (upFree && rightFree)
          {
            IgniteFlashOnIfNeeded(upperY, rightX);
          }
        }
        void SparkDownLeft()
        {
          if (downFree && leftFree)
          {
            IgniteFlashOnIfNeeded(downY, leftX);
          }
        }
        void SparkDownRight()
        {
          if (downFree && rightFree)
          {
            IgniteFlashOnIfNeeded(downY, rightX);
          }
        }

        void IgniteFlashOnIfNeeded(int y, int x)
        {
          if (notMarkedForFlash[y, x])
          {
            _octopusesGrid[y, x]++;
            if (_octopusesGrid[y, x] == 10)
            {
              aboutToFlash.Enqueue(new Coordinates(y, x));
              notMarkedForFlash[y, x] = false;
            }
          }
        }
      }

      IterateThroughGrid(OnOctupuseReset);

      void OnOctopuseInvoke(int height, int width)
      {
        notMarkedForFlash[height, width] = true;
        _octopusesGrid[height, width]++;

        if (_octopusesGrid[height, width] == 10)
        {
          notMarkedForFlash[height, width] = false;
          aboutToFlash.Enqueue(new Coordinates(height, width));
        }
      }

      void OnOctupuseReset(int height, int width)
      {
        if (!notMarkedForFlash[height, width])
        {
          _octopusesGrid[height, width] = 0;
          _numberOfFlashes++;
        }
      }
    }

    public void InvokeStepsBy(int numberOfSteps)
    {
      for (int i = 0; i < numberOfSteps; i++)
      {
        InvokeStep();
      }
    }

    public int InvokeStepsUntilSync()
    {
      bool isNotSync = true;
      int step = 0;
      while (isNotSync)
      {
        step++;
        InvokeStep();

        isNotSync = CheckIfNotSync();
      }

      return step;

      bool CheckIfNotSync()
      {
        int syncValue = _octopusesGrid[0, 0];

        for (int height = 0, heightLength = _octopusesGrid.GetLength(0); height < heightLength; height++)
        {
          for (int width = 0, widthLength = _octopusesGrid.GetLength(0); width < heightLength; width++)
          {
            if (syncValue != _octopusesGrid[height, width])
            {
              return true;
            }
          }
        }

        return false;
        
      }
    }

    public override string ToString()
    {
      var ouputBuilder = new StringBuilder(_octopusesGrid.GetLength(0) * _octopusesGrid.GetLength(1));

      IterateThroughGrid(ActionOnElement, ActionOnRow);
      
      void ActionOnElement(int height, int width) 
        => ouputBuilder.Append($"{_octopusesGrid[height, width],2}");

      void ActionOnRow(int height)
        => ouputBuilder.AppendLine();

      return ouputBuilder.ToString();
    }
  }
}
