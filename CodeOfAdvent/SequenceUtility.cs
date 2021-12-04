using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent
{
  public static class SequenceUtility
  {
    public static void PrintSequence<T>(IEnumerable<T> array)
    {

      foreach (T element in array)
      {
        Console.WriteLine(element);
      }
    }

    public static int[] CastToWholeNumbers(string[] array)
    {
      var result = new int[array.Length];
      for (int i = 0; i < result.Length; i++)
      {
        result[i] = Convert.ToInt32(array[i]);
      }
      return result;
    }
  }
}
