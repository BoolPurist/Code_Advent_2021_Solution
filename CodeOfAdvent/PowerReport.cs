using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent
{
  public class PowerReport
  {
    private int _gammaRate = 0;
    private int _epsilonRate = 0;
  
    public PowerReport(int[] binaryInput, in int bitLength = 32)
    {
      int power = 1;
      
      for (int bitIndex = 0; bitIndex < bitLength; bitIndex++)
      {
        int oneBitCount = 0;
        int zeroBitCount = 0;
        for (int indexOfArray = 0; indexOfArray < binaryInput.Length; indexOfArray++)
        {
          int currentBinary = binaryInput[indexOfArray];
          int currentBit = BinaryFun.GetBitAtFromLeft(currentBinary, bitIndex);
          if (currentBit == 0)
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
          _gammaRate += power;
        }
        else
        {
          _epsilonRate += power;
        }

        power *= 2;
      }
    }

    public PowerReport(string[] binaryInput)
    {
      int power = 1;
      int firstIndex = binaryInput[0].Length - 1;

      for (int bitIndex = firstIndex; bitIndex > -1; bitIndex--)
      {
        int oneBitCount = 0;
        int zeroBitCount = 0;
        for (int indexOfArray = 0; indexOfArray < binaryInput.Length; indexOfArray++)
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
          _gammaRate += power;
        }
        else
        {
          _epsilonRate += power;
        }

        power *= 2;
      }
    }    

    public int GammRate => _gammaRate;
    public int EpsilonRate => _epsilonRate;

    public int PowerConsumption => _gammaRate * _epsilonRate;
    
  }
}
