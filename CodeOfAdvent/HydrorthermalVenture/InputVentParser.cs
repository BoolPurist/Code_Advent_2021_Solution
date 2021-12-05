using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.HydrorthermalVenture
{
  public static class InputVentParser
  {
    

    public static LineOfVent[] Parse(string[] input)
    {
      var readVents = new List<LineOfVent>();

      foreach (string line in input)
      {
        string[] points = line.Split("->");

        string[] componentsOfFirstPoint = points[0].Trim().Split(',');
        string[] componentsOfSecondPoint = points[1].Trim().Split(',');

        var firstPoint = new Vector2Int(
            Convert.ToInt32(componentsOfFirstPoint[0]), 
            Convert.ToInt32(componentsOfFirstPoint[1])
          );

        var secondPoint = new Vector2Int(
          Convert.ToInt32(componentsOfSecondPoint[0]),
          Convert.ToInt32(componentsOfSecondPoint[1])
        );

        var vent = new LineOfVent(firstPoint, secondPoint);
        readVents.Add(vent);
      }

      return readVents.ToArray();
    }
  }
}
