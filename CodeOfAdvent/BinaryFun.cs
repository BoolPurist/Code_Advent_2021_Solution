using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent
{
  public static class BinaryFun
  {
    public static int GetBitAtFromLeft(int number, int positionOfBit)
    {
      Debug.Assert(positionOfBit >= 0);
      int bitMask = 1 << positionOfBit;
      return (bitMask & number) > 0 ? 1 : 0;
    }


    public static int GetBitLengthOfInput(string[] input) => input[0].ToString().Length;
  }
}
