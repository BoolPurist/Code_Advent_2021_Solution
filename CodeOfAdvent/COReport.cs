using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent
{
  public record BitCounting(char CommenBit, char UncommenBit, bool IsBalanced);

  public class COReport
  {
    public int CO2ScrubberRating => _cO2ScrubberRating;
    public int OxygenGeneratorRating => _oxygenGeneratorRating;

    public int Product => CO2ScrubberRating * OxygenGeneratorRating;

    private int _cO2ScrubberRating;
    private int _oxygenGeneratorRating;

    public COReport(string[] binaryInput)
    {
      _oxygenGeneratorRating = Convert.ToInt32(SearchForRating(binaryInput, OxygenGeneratorRatingFiltering), 2);
      _cO2ScrubberRating = Convert.ToInt32(SearchForRating(binaryInput, CO2ScrubberRatingFiltering), 2);
    }

    private string SearchForRating(string[] binaryInput, Action<BitCounting, List<string>, int> filtering)
    {
      List<string> poolToSearchThrough = binaryInput.ToList();
      
      int length = binaryInput[0].Length;
      for (int bitIndex = 0; bitIndex < length && poolToSearchThrough.Count > 1; bitIndex++)
      {
        BitCounting countingForCurrentBit = GetMostCommonAndUnCommenBit(bitIndex, poolToSearchThrough);

        filtering(countingForCurrentBit, poolToSearchThrough, bitIndex);

      }

      return poolToSearchThrough[0];
    }

    private void CO2ScrubberRatingFiltering(
      BitCounting countingForCurrentBit, 
      List<string> poolToSearchThrough,
      int bitIndex
      )
    {
      if (countingForCurrentBit.IsBalanced)
      {
        poolToSearchThrough.RemoveAll(element => element[bitIndex] == '1');
      }
      else
      {
        poolToSearchThrough.RemoveAll(element => element[bitIndex] == countingForCurrentBit.CommenBit);
      }
    }

    private void OxygenGeneratorRatingFiltering(
      BitCounting countingForCurrentBit,
      List<string> poolToSearchThrough,
      int bitIndex
      )
    {
      if (countingForCurrentBit.IsBalanced)
      {
        poolToSearchThrough.RemoveAll(element => element[bitIndex] == '0');
      }
      else
      {
        poolToSearchThrough.RemoveAll(element => element[bitIndex] == countingForCurrentBit.UncommenBit);
      }
    }


    private BitCounting GetMostCommonAndUnCommenBit(int bitIndex, IList<string> binaryInput)
    {
      int oneBitCount = 0;
      int zeroBitCount = 0;
      for (int indexOfArray = 0; indexOfArray < binaryInput.Count; indexOfArray++)
      {
        string currentBinary = binaryInput[indexOfArray];

        if (currentBinary[bitIndex] == '0')
        {
          zeroBitCount++;
        }
        else
        {
          oneBitCount++;
        }
      }

      if (oneBitCount > zeroBitCount)
      {
        return new BitCounting('1', '0', false);
      }
      else if (oneBitCount == zeroBitCount)
      {
        return new BitCounting(' ', ' ', true);
      }
      else
      {
        return new BitCounting('0', '1', false);
      }
    }


  }
}
