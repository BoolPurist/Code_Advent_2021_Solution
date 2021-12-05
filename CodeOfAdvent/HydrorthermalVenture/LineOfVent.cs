using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeOfAdvent.HydrorthermalVenture
{
  public record Vector2Int(int X, int Y);

  public class LineOfVent
  {
    public Vector2Int FirstPoint { get; private set; }
    public Vector2Int SecondPoint { get; private set; }

    public LineOfVent(Vector2Int firstPoint, Vector2Int secondPoint)
      => (FirstPoint, SecondPoint) = (firstPoint, secondPoint);

    public bool IsNotDiagonal 
      => IsVertical || IsHorizontal;

    public bool IsVertical
      => FirstPoint.X == SecondPoint.X;

    public bool IsHorizontal
      => FirstPoint.Y == SecondPoint.Y;

    public override string ToString()
      => $"{FirstPoint.X},{FirstPoint.Y} -> {SecondPoint.X},{SecondPoint.Y}";
  }
}
