using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.HydrorthermalVenture
{
  public class VentLineDiagram
  {
    private const char EMPTY_CHAR = '.';

    private readonly int[,] _diagramValues;

    public VentLineDiagram(int width, int height)
    {
      Debug.Assert(width > 0);
      Debug.Assert(height > 0);

      _diagramValues = new int[height, width];
      
    }

    public VentLineDiagram(IEnumerable<LineOfVent> vents)
    {
      int maxValue = 0;

      foreach (LineOfVent vent in vents)
      {
        maxValue = Math.Max(maxValue, vent.MaxValue);        
      }

      maxValue++;

      _diagramValues = new int[maxValue, maxValue];

      InsertVentLines(vents);
    }



    public int MaxCount
    {
      get
      {
        int maxCount = 0;

        IterateThrough(element => maxCount = Math.Max(maxCount, element));

        return maxCount;
      }
    }

    public int GetNumberOfPointsWithAtLeastOverlapOf(in int overlap)
    {
      int countOfOverlaps = 0;
      IterateThrough(count => { if (count >= 2) countOfOverlaps++; });
      return countOfOverlaps;
    }

    public void InsertVentLines(IEnumerable<LineOfVent> sequenceToInsert)
    {
      foreach (LineOfVent line in sequenceToInsert)
      {        
        InsertVentLine(line);        
      }
    }

    

    public void InsertVentLine(LineOfVent lineOfVent)
    {

      if (lineOfVent.IsHorizontal)
      {
        foreach (int currentX in GetPointsAlongLine(lineOfVent.FirstPoint.X, lineOfVent.SecondPoint.X))
        {
          var newPosition = new Vector2Int(currentX, lineOfVent.FirstPoint.Y);
          Insert(newPosition);
        }
      }
      else if (lineOfVent.IsVertical)
      {
        foreach (int currentY in GetPointsAlongLine(lineOfVent.FirstPoint.Y, lineOfVent.SecondPoint.Y))
        {
          var newPosition = new Vector2Int(lineOfVent.FirstPoint.X, currentY);
          Insert(newPosition);
        }
      }
      else
      {
        int[] xPoints = GetPointsAlongLine(lineOfVent.FirstPoint.X, lineOfVent.SecondPoint.X).ToArray();
        int[] yPoints = GetPointsAlongLine(lineOfVent.FirstPoint.Y, lineOfVent.SecondPoint.Y).ToArray();

        Debug.Assert(xPoints.Length == yPoints.Length);

        for (int currentDiagonalIndex = 0; currentDiagonalIndex < xPoints.Length; currentDiagonalIndex++)
        {
          var newPosition = new Vector2Int(xPoints[currentDiagonalIndex], yPoints[currentDiagonalIndex]);
          Insert(newPosition);
        }
      }

      IEnumerable<int> GetPointsAlongLine(in int start, in int end)
      {

        if (start == end)
        {
          return null;
        }
        else if (start < end)
        {
          return GetRange(start, end);
        }
        else
        {          
          var sequence = GetRange(end, start);
          sequence = sequence.Reverse();
          return sequence;
        }

        IEnumerable<int> GetRange(in int start, in int end) 
          => Enumerable.Range(start, (end - start) + 1);
      }
    }

    private void Insert(Vector2Int position) => _diagramValues[position.Y, position.X]++;
    
    public override string ToString()
    {
      var outputBuilder = new StringBuilder();

      int maxCount = MaxCount;
      int desiredWordLength = maxCount.ToString().Length;


      IterateThrough(AppendNextElement, (element) => outputBuilder.AppendLine());

      return outputBuilder.ToString();

      void AppendNextElement(int currentOverlapCount)
      {
        string baseValueToAppend = "";

        if (currentOverlapCount != 0)
        {
          baseValueToAppend = currentOverlapCount.ToString();          
        }
        else
        {
          baseValueToAppend = EMPTY_CHAR.ToString();
          
        }
        
        int baseValueToAppendLength = baseValueToAppend.Length;
        int offsetToDesiredWordLength = desiredWordLength - baseValueToAppendLength;
        
        if (offsetToDesiredWordLength > 0)
        {
          outputBuilder.Append(new string(' ', offsetToDesiredWordLength));
        }

        outputBuilder.Append($"{baseValueToAppend}");
      }
    }

    private void IterateThrough(Action<int> callForEveryElement, Action<int> callForEveryRow = null)
    {
      for (int heightIndex = 0, height = _diagramValues.GetLength(1); heightIndex < height; heightIndex++)
      {

        for (int widthIndex = 0, width = _diagramValues.GetLength(0); widthIndex < width; widthIndex++)
        {
          int currentOverlapCount = _diagramValues[heightIndex, widthIndex];
          callForEveryElement(currentOverlapCount);
        }

        callForEveryRow?.Invoke(heightIndex);
      }
    }


  }
}
