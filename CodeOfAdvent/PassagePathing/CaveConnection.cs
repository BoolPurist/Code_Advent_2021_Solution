using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.PassagePathing
{
  public partial class CaveSystem
  {
    public class CaveConnection
    {
      private static int _nextId = 0;


      private Cave _startCave;
      public string NameOfStartCave => _startCave.Name;
      public Cave Target { get; private set; }
      public int Id { get; private set; }

      public CaveConnection(Cave start, Cave target)
      {
        _startCave = start;
        Target = target;
        Id = _nextId++;
      }

      public override string ToString()
        => $"{NameOfStartCave} -> {Target.Name} width Id: {Id}";
    }
  }
}
