using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.WhaleAndCrabs
{

  public class CrabFormation
  {
    private int Sum(int end)
      => (end * (end + 1)) / 2;

    private Func<int, int> distanceFunction;

    public CrabFormation(IEnumerable<int> crabs, bool linear = true)
    {
      distanceFunction = linear ? distance => distance : Sum;
      GetValueToCount(crabs);
      CalcalateFuelUsage();
      CalculateMinimumFuelUsage();
    }

    
    private int[] oceanGrid;
    private Dictionary<int, int> crabsForRescue = new();

    public int SweetSpot { get; private set; }
    public int MinimalFuelUsage { get; private set; }

    private void CalcalateFuelUsage()
    {
      foreach (KeyValuePair<int, int> oneCrab in crabsForRescue)
      {
        for (int i = 0; i < oceanGrid.Length; i++)
        {
          int numberOfCrabs = oneCrab.Value;
          int positionOfCrabs = oneCrab.Key;
          int distance = distanceFunction(Math.Abs(positionOfCrabs - i));
          oceanGrid[i] += distance * numberOfCrabs;
        }
      }

    }

    private void CalculateMinimumFuelUsage()
    {
      int sweetPosition = 0;
      int minFuel = int.MaxValue;

      for (int i = 0; i < oceanGrid.Length; i++)
      {
        int fuelUsage = oceanGrid[i];
        if (fuelUsage < minFuel)
        {
          minFuel = fuelUsage;
          sweetPosition = i;
        }
      }

      MinimalFuelUsage = minFuel;
      SweetSpot = sweetPosition;
    }

    private void GetValueToCount(IEnumerable<int> crabs)
    {

      int max = crabs.Max();

      oceanGrid = new int[max + 1];
     
      foreach (int oneCrab in crabs)
      {
        if (crabsForRescue.ContainsKey(oneCrab))
        {
          crabsForRescue[oneCrab]++;
        }
        else
        {
          crabsForRescue.Add(oneCrab, 1);
        }
      }

      
    }

  }

}
