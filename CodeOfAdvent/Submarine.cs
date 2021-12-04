using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent
{
  public class Submarine : SubmarineV1
  {
    protected int _aim = 0;

    public override void ProcessCommand(string commandLine)
    {
      SubmarineCommand command = CreateFrom(commandLine);

      switch (command.Command)
      {
        case COMMAND_FORWARD:
          _horizontalPosition += command.ArgumentValue;
          _verticalPosition += _aim * command.ArgumentValue;
          break;
        case COMMAND_UP:
          _aim -= command.ArgumentValue;
          break;
        case COMMAND_DOWN:
          _aim += command.ArgumentValue;
          break;
      }
    }


    public int Aim => _aim;
  }
}
