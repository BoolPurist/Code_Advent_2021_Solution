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
      public int NumberOfPathsToEnd { get; private set; } = 0;
      


      private Queue<CavePath> _pendingPaths = new();


      public CavePathFinding(Cave startCave, bool withSmallVisitJoker)
      {
        var firstPath = new CavePath(startCave, withSmallVisitJoker);
        _pendingPaths.Enqueue(firstPath);
        WalkAllPossiblePaths();
      }

      public CavePathFinding(Cave rootCave, string[] namesOfCaves, bool withSmallVisitJoker)
      {
        var startCave = new CavePath(rootCave, withSmallVisitJoker);
        foreach (string caveName in namesOfCaves)
        {
          startCave = startCave.CreateNewPathWithTargetAs(caveName);
        }

        _pendingPaths.Enqueue(startCave);
        WalkAllPossiblePaths();
      }

      private void WalkAllPossiblePaths()
      {
        do
        {
          CavePath currentPath = _pendingPaths.Dequeue();

          if (currentPath.ReachedEnd)
          {
            NumberOfPathsToEnd++;
          }
          else
          {
            foreach (CaveConnection possibleNextPath in currentPath.Connections)
            {

              if (currentPath.IsValidNewTarget(possibleNextPath))
              {
                var newPath = new CavePath(possibleNextPath.Target, currentPath, possibleNextPath.Id);
                _pendingPaths.Enqueue(newPath);
              }
            }
          }
          

        } while (_pendingPaths.Count > 0);
      }

      
    }
  }
}
