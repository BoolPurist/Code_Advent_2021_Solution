using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent
{
  public record SubmarineCommand(string Command, int ArgumentValue);

  public class SubmarineV1
  {
    protected const string COMMAND_FORWARD = "forward";
    protected const string COMMAND_UP = "up";
    protected const string COMMAND_DOWN = "down";

    protected int _horizontalPosition = 0;
    protected int _verticalPosition = 0;

    public static SubmarineCommand CreateFrom(string commmandLine)
    {
      string[] commadAndArguement = commmandLine.Split(' ');
      string commmand = commadAndArguement[0];
      string argument = commadAndArguement[1];
      int arguementValue = Convert.ToInt32(argument);
      return new SubmarineCommand(commmand, arguementValue);
    }

    public virtual void ProcessCommand(string commandLine)
    {
      SubmarineCommand command = CreateFrom(commandLine);

      switch (command.Command)
      {
        case COMMAND_FORWARD:
          _horizontalPosition += command.ArgumentValue;
          break;
        case COMMAND_UP:
          _verticalPosition -= command.ArgumentValue;
          break;
        case COMMAND_DOWN:
          _verticalPosition += command.ArgumentValue;
          break;
      }
    }

    public virtual void ProcessCommand(string[] commands)
    {
      foreach (string singleCommand in commands)
      {
        ProcessCommand(singleCommand);
      }
    }

    public int HorizontalPosition => _horizontalPosition;
    public int VerticalPosition => _verticalPosition;
    public int ProductOfPosition => HorizontalPosition * VerticalPosition;

    
  }



  
}
