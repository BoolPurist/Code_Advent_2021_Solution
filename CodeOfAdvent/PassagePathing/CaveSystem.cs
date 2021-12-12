using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent.PassagePathing
{
  public partial class CaveSystem
  {
    private const string CAVE_PARSE_SEPARATOR = "-";
    public const string START_CAVE_ID = "start";
    public const string END_CAVE_ID = "end";


    public int NumberOfCaves { get; private set; } 

    private readonly Cave _startCave;

    private readonly Dictionary<string, Cave> _cavesInSystem = new();

    public CaveSystem(string[] cavesToConnect)
    {
      
      foreach (string caveAsPair in cavesToConnect)
      {
        string[] cavePairTornApart = caveAsPair.Split(CAVE_PARSE_SEPARATOR);

        string firstCaveName = cavePairTornApart[0];
        string secondCaveName = cavePairTornApart[1];

        Cave firstCave = GetOrCreateCave(firstCaveName);
        Cave secondCave = GetOrCreateCave(secondCaveName);

        firstCave.AddNeighbour(secondCave, true);

        Cave GetOrCreateCave(in string caveName)
        {
          Cave cave;
          if (_cavesInSystem.TryGetValue(caveName, out cave))
          {
            cave = _cavesInSystem[caveName];
          }
          else
          {
            cave = new Cave(caveName);
            _cavesInSystem.Add(caveName, cave);
          }
          return cave;
        }        
      }

      _startCave = _cavesInSystem[START_CAVE_ID];
      NumberOfCaves = _cavesInSystem.Count;
    }

    public int GetNumberOfPathsToEnd(bool withJoker)
    {      
      var caveSystem = new CavePathFinding(_startCave, withJoker);
      return caveSystem.NumberOfPathsToEnd;
    }


    public override string ToString()
    {
      var outputBuilder = new StringBuilder();

      outputBuilder.AppendLine($"Number of Caves: {NumberOfCaves}");

      foreach (KeyValuePair<string, Cave> nameToCave in _cavesInSystem)
      {
        List<CaveConnection> connections = nameToCave.Value.Connections;


        string caveName = nameToCave.Key;

        outputBuilder.Append($"{caveName} -> (");

        foreach (CaveConnection oneConnection in connections)
        {
          outputBuilder.Append($"{oneConnection.Target.Name}: {oneConnection.Id}, ");
        }

        outputBuilder.AppendLine("\b\b)");
      }

      return outputBuilder.ToString();
    }
  }
}
