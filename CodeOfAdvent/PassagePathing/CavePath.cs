using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.PassagePathing
{
  public partial class CaveSystem
  {
    public partial class CavePathFinding
    {

      public class CavePath
      {
        public bool ReachedEnd => _currentCave.IsEnd;

        public string NameOfCurrentCave => _currentCave.Name;

        public List<CaveConnection> Connections => _currentCave.Connections;

        private List<string> WalkedCavnes = new();

        public HashSet<CaveSystem.Cave> VistidedSmallCaves { get; private set; } = new();

        public int NumberOfVisitedCaves { get; private set; }

        public bool HasSmallVisitJoker { get; private set; }

        private readonly CaveSystem.Cave _currentCave;

        public CavePath(CaveSystem.Cave cave, CavePath path, int newConnectionId)
        {
          _currentCave = cave;
          WalkedCavnes = new List<string>(path.WalkedCavnes);
          WalkedCavnes.Add(cave.Name);
          NumberOfVisitedCaves = path.NumberOfVisitedCaves + 1;

          VistidedSmallCaves = new HashSet<CaveSystem.Cave>(path.VistidedSmallCaves);
          
          HasSmallVisitJoker = path.HasSmallVisitJoker && !VistidedSmallCaves.Contains(cave);
          if (cave.IsSmall)
          {             
            VistidedSmallCaves.Add(cave);
          }
          
        }

        public CavePath(CaveSystem.Cave cave, bool startsWithSmallVisitJoker = false)
        {
          HasSmallVisitJoker = startsWithSmallVisitJoker;
          _currentCave = cave;
          WalkedCavnes.Add(cave.Name);
          NumberOfVisitedCaves = 1;
        }

        private bool IsSmallCaveAlreadyWalked(CaveSystem.CaveConnection connection)
        {
          CaveSystem.Cave cave = connection.Target;

          if (HasSmallVisitJoker) return false;

          return cave.IsSmall && VistidedSmallCaves.Contains(cave);
                  
        }

        private bool IsStart(CaveSystem.CaveConnection connection) => connection.Target.IsStart;

        public bool IsValidNewTarget(CaveSystem.CaveConnection connection)
          => !IsSmallCaveAlreadyWalked(connection) &&
          !IsStart(connection);

        public override string ToString()
        {
          var outputBuilder = new StringBuilder();

          foreach (string oneCave in WalkedCavnes)
          {
            outputBuilder.Append($"{oneCave} -> ");
          }

          return outputBuilder.ToString();
        }

        // For debugging
        public CavePath CreateNewPathWithTargetAs(string nameOfWantedTarget)
        {
          foreach (CaveConnection connection in Connections)
          {
            string nameOfCurrentTarget = connection.Target.Name;
            if (nameOfWantedTarget == nameOfCurrentTarget)
            {
              return new CavePath(connection.Target, this, connection.Id);
            }
          }

          throw new ArgumentException($"No connection with target name {nameOfWantedTarget} found.");
        }
      }
    }
  }

}
