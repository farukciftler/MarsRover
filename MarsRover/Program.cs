using System;
using MarsRover;
namespace MarsRover
{
    class Program

    {

        static void Main(string[] args)
        {
            string input =
                @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
";
            RoverLogic logic = new RoverLogic();
            logic.MainLogic(input);
          
        }
    }
}
