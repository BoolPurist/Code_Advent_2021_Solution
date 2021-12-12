using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeOfAdvent.PassagePathing;

namespace CodeOfAdvent.PassagePathing
{
  public partial class CaveSystem
  {
    public class Cave
    {

      public bool IsSmall { get; private set; }
      public bool IsStart { get; private set; }

      public string Name { get; private set; }

      public bool IsEnd { get; private set; }


      public List<CaveConnection> Connections { get; private set; } = new();


      

      public Cave(string name)
      {
        Name = name;
        
        IsSmall = Char.IsLower(name[0]);

        IsEnd = IsSmall && name == CaveSystem.END_CAVE_ID;
        IsStart = IsSmall && name == CaveSystem.START_CAVE_ID;
        
      }

      public void AddNeighbour(Cave newNeighbour, bool isInitiator = false)
      {
        var newConnection = new CaveConnection(this, newNeighbour);
        Connections.Add(newConnection);
        if (isInitiator)
        {
          newNeighbour.AddNeighbour(this, false);
        }
      }

    }
  }
}
