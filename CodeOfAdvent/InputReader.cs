using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOfAdvent
{
  public static class InputReader
  {
    public static string[] ReadFromLocalFile(in string path)
    {
      string workingDirectory = Directory.GetCurrentDirectory();
      string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
      string basePathFolder = Path.Combine(projectDirectory, NAME_OF_INPUT_FOLDER, path);
      return ConvertToArray(File.ReadAllText(basePathFolder));
    }
    private static string[] ConvertToArray(in string content)
    {
      return content.Split(Environment.NewLine);
    }

    #region In memory data
    public const string DAY4_TASK_B_FILENAME = "4_day_input_B.txt";
    public const string DAY4_TASK_A_FILENAME = "4_day_input_instruction.txt";
    public const string DAY2_TASK1_FILENAME = "2_day_task_1.txt";
    public const string DAY2_TASK2_FILENAME = "2_day_task_2.txt";
    public const string DAY3_INPUT = "3_day_input.txt";
    public const string DAY5_INPUT_0 = "5_day_input_0.txt";
    public const string DAY5_INPUT_A = "5_day_input_A.txt";

    public readonly static string[] DAY2_TASK12_START_INPUT = new string[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };
    public readonly static int[] DAY1_TASK12_START_INPUT = new int[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
    
    public readonly static int[] DAY3_TASK1_START_INPUT = new int[] 
    { 
      0b_00100, 
      0b_11110, 
      0b_10110, 
      0b_10111, 
      0b_10101, 
      0b_01111, 
      0b_00111, 
      0b_11100, 
      0b_10000, 
      0b_11001, 
      0b_00010, 
      0b_01010 
    };

    

    public readonly static int[] DAY3_TASK1_A_TEST = new int[]
      {
        0b_0000_0111_0001,
        0b_0000_0111_1101,
        0b_0100_1100_0000,
        0b_0000_0000_0011,
        0b_0100_0010_0111,
        0b_1010_0010_0001
      };


    private const string NAME_OF_INPUT_FOLDER = "inputFiles";

    #endregion





  }
}
