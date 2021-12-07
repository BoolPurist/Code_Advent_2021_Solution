using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace CodeOfAdvent.lanternfish
{
  public static class FishInterpolation
  {
    private const int MaxDegreeOfParallelismLimit = 10;

    public static Dictionary<int, int> CreateFishStartCount(IEnumerable<int> fishes)
    {
      var result = new Dictionary<int, int>();

      foreach (int fish in fishes)
      {
        if (result.ContainsKey(fish))
        {
          result[fish]++;
        }
        else
        {
          result.Add(fish, 1);
        }
      }

      return result;
    }

    public static ulong GetInterpolatedPopulationCount(
      int days, 
      IEnumerable<int> fishes, 
      int normalSpawnRate, 
      int startSpawnRate,
      int MaxDegreeOfParallelismLimit = 1
      )
    {
      Dictionary<int, int> fishesOnStartDays = CreateFishStartCount(fishes);
      int[] possibleStartDays = fishesOnStartDays.Keys.ToArray();
      int numberOfPossibleStartDays = possibleStartDays.Length;

      bool startDaysGoBeyondParallelism = numberOfPossibleStartDays > MaxDegreeOfParallelismLimit;
      int maxDegreeOfParallelismLimit = startDaysGoBeyondParallelism ? 
        MaxDegreeOfParallelismLimit : numberOfPossibleStartDays;

      var performenceLimiter = new ParallelOptions() 
      { 
        MaxDegreeOfParallelism = maxDegreeOfParallelismLimit
      };

      var startDayToBirthCount = new ConcurrentDictionary<int, ulong>();

      Parallel.ForEach(possibleStartDays, performenceLimiter, ParallelCalc);
    
      ulong result = 0L;

      foreach (KeyValuePair<int, ulong> startDayToOneGrowth in startDayToBirthCount)
      {
        ulong numberOfThisStartDay = (ulong)fishesOnStartDays[startDayToOneGrowth.Key];
        ulong growthResultOfOne = startDayToOneGrowth.Value;
        result += numberOfThisStartDay * growthResultOfOne;
      }

      return result;

      void ParallelCalc(int fish)
      {
        ulong populationCount = 0;
        InterpolateOn(days, fish);

        void InterpolateOn(int leftDays, int offset)
        {
          populationCount++;
          leftDays -= offset;
          while (leftDays > 0)
          {
            offset = startSpawnRate;
            InterpolateOn(leftDays, offset);
            leftDays -= normalSpawnRate;
          }
        }

        startDayToBirthCount.TryAdd(fish, populationCount);
      }

    }


    


  }
}
