using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.Bingo
{
  public class BingoBoard
  {
    public const int BOARD_HEIGHT = 5;
    public const int BOARD_WIDTH = 5;

    public readonly static int BOARD_SIZE = BOARD_HEIGHT * BOARD_WIDTH;
    public const char TAKEN_CHAR = 'O';
    public const char EMPTY_CHAR = 'X';

    private bool[] _marking = new bool[BOARD_SIZE];
    private int[] _rowCount = new int[5];
    private int[] _columnCount = new int[5];

    public int LastInsertedNumber { get; private set; }

    private Dictionary<int, int> _mapping = new();

    public BingoBoard(int[] rows)
    {
      for (int index = 0; index < rows.Length; index++)
      {
        int currentNumber = rows[index];
        _mapping.Add(currentNumber, index);
      }
    }

    public int SumOfUnMarkedFiels
    {
      get 
      {
        int sum = 0;

        int[] numbers = CreateNumberAsSequence();

        for(int markIndex = 0; markIndex < _marking.Length; markIndex++)
        {
          bool isMarked = _marking[markIndex];
          if (!isMarked)
          {
            int currentNotMarkedNumber = numbers[markIndex];
            sum += currentNotMarkedNumber;
          }
        }

        return sum;
      }
    }
   
    public bool HasWon
    {
      get
      {
        foreach (int count in _rowCount)
        {
          if (count == BOARD_WIDTH)
          {
            return true;
          }
        }

        foreach (int count in _columnCount)
        {
          if (count == BOARD_HEIGHT)
          {
            return true;
          }
        }

        return false;
      }
    }

    public override string ToString()
    {
      int[] numberAsSequnce = CreateNumberAsSequence();
      var outputBuilder = new StringBuilder();

      for (int currentIndex = 0, columnIndex = 0; currentIndex < numberAsSequnce.Length; currentIndex++, columnIndex++)
      {
        int currentNumber = numberAsSequnce[currentIndex];
        
        if (columnIndex == 5)
        {
          columnIndex = 0;
          outputBuilder.AppendLine();
        }

        string digit = currentNumber < 10 ? $" {currentNumber}" : currentNumber.ToString();
        outputBuilder.Append($"{digit} ");
      }

      return outputBuilder.ToString();
    }

    public string GetBoardStatus()
    {
      
      var outputBuilder = new StringBuilder();

      for (int currentIndex = 0, columnIndex = 0; currentIndex < _marking.Length; currentIndex++, columnIndex++)
      {
        bool currentStatus = _marking[currentIndex];

        if (columnIndex == 5)
        {
          columnIndex = 0;
          outputBuilder.AppendLine();
        }

        char statusSymbol = currentStatus ? TAKEN_CHAR : EMPTY_CHAR;
        outputBuilder.Append($"{statusSymbol} ");
      }

      return outputBuilder.ToString();
    }

    public bool TryInsertNumber(int number)
    {
      bool containsNumber = _mapping.ContainsKey(number);
      if (containsNumber)
      {
        int index = _mapping[number];

        int rowPosition = index / 5;
        int columnPosition = index - (rowPosition * 5);
        _rowCount[rowPosition]++;
        _columnCount[columnPosition]++;

        _marking[index] = true;
        LastInsertedNumber = number;
      }

      return containsNumber;
    }


    private int[] CreateNumberAsSequence()
    {
      var numbersAsSequence = new int[BOARD_SIZE];

      foreach (KeyValuePair<int, int> numberWithIndex in _mapping)
      {
        int index = numberWithIndex.Value;
        int numberValue = numberWithIndex.Key;

        numbersAsSequence[index] = numberValue;
      }

      return numbersAsSequence;
    }
  }
}
