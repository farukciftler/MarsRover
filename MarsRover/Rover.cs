using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class Rover
    {
        public int[] Coordinates { get; set; }
        public string Directions { get; set; }
    }

    public class RoverLogic
    {
      #region Rover Logic Functions
      public void MainLogic(string input)
        {
            /*
             It is the main function that controls all the
            operations that Rover will perform on Mars. It
            runs all intermediate functions within it. 
            These functions are;

            1. Separating the input into lines.

            2. Convert the non-numerical values in the first
            row to numerical values and create the limits.

            3. Converting incoming non-numerical rover coordinates
            to numerical rover coordinates. (Using Direction enum)

            4. Calculating the target point and
            checking whether the limit is exceeded in this process.

            5. Converting the calculated target
            point back to non-numeric output as desired.

            6. To do all the above functions in a loop. 
             
             */
            string[] inputArray = input.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var border = CreateBorder(inputArray[0]);
            for (int i = 1; i < inputArray.Length / 2 + 1; i++)
            {            
                RoverLogic logic = new RoverLogic();
                Rover rover = new Rover();
                int[] coordinates= ConvertToCoordinates(inputArray[i * 2 - 1]);
                int[] result = { 0, 0, 0 };
                rover.Coordinates = coordinates;
                rover.Directions = inputArray[i * 2];
                result = logic.GetRoute(rover.Coordinates, rover.Directions, border);
                Console.WriteLine(ConvertToResult(result));
            }
        }
      public int[] GetRoute(int[] coordinates, string directions, int[] border)
        {
            var reachedExceededLimit = IsLimit(coordinates, border); //Plateu limits are controlled by this function.
            int[] newCoordinates = coordinates;
            var rotate = directions.ToCharArray();
            foreach (var item in rotate)
            {
                if (newCoordinates[2] < 0)//The 3rd element of the coordinate vector should not be negative. For detailed information, you can look at the Direction enum.
                {
                    newCoordinates[2] = newCoordinates[2] + 4;
                }
                else if (newCoordinates[2] > 3)//The 3rd element of the coordinate vector must always be less than 4. For detailed information, you can look at the Direction enum.
                {
                    newCoordinates[2] = newCoordinates[2] - 4;
                }
                if (item == 'L')//This is rotate situation. For detailed information, you can look at the Heading enum.
                {
                    newCoordinates[2] = (int)(newCoordinates[2] + Heading.L);
                }
                else if (item == 'R')//This is rotate situation. For detailed information, you can look at the Heading enum.
                {
                    newCoordinates[2] = (int)(newCoordinates[2] + Heading.R);
                }
                else if (item == 'M')//All situations inside this if else pattern are "Forward One Grid Point" situations.  For detailed information, you can look at the Direction enum.
                {
                    if (newCoordinates[2] == (int)Direction.N && reachedExceededLimit[3] != 1)
                    {
                        newCoordinates[1] = (int)(newCoordinates[1] + DirectionHeading.N);
                    }
                    else if (newCoordinates[2] == (int)Direction.S && reachedExceededLimit[2] != 1)
                    {
                        newCoordinates[1] = (int)(newCoordinates[1] + DirectionHeading.S);
                    }
                    else if (newCoordinates[2] == (int)Direction.W && reachedExceededLimit[0] != 1)
                    {
                        newCoordinates[0] = (int)(newCoordinates[0] + DirectionHeading.W);
                    }
                    else if (newCoordinates[2] == (int)Direction.E && reachedExceededLimit[0] != 1)
                    {
                        newCoordinates[0] = (int)(newCoordinates[0] + DirectionHeading.E);
                    }
                }
            };
            return newCoordinates;
        }
      public string ConvertToResult(int[] coordinates)
        {
            /*
            Converts the numerical result values
            to the results requested from us and
            non-numerical.

            For example:
            Numerical output: 1 3 0
            Non-numeric and requested output from us: 1 3 N 
             */
            var directionCoefficient = coordinates[2] % 4;
            char direction = 'N';
            switch (directionCoefficient)
            {
                case (int)Direction.N:
                    direction = 'N';
                    break;
                case (int)Direction.E:
                    direction = 'E';
                    break;
                case (int)Direction.S:
                    direction = 'S';
                    break;
                case (int)Direction.W:
                    direction = 'W';
                    break;
            }
            var convertedCoordinates = $"{coordinates[0]} {coordinates[1]} {direction}";
            return convertedCoordinates;
        }
      public int[] ConvertToCoordinates(string input)
        {
            /*
             It makes non-numerical coordinate inputs
            numerical in order to perform the necessary
            operations. Here it makes use of the Direction enum.

            For Example:
            Non-numerical input: 1 2 N
            Numerical version: 1 2 0 
             */
            string trimmedInput = input.Replace(" ", "");
            var inputArray = trimmedInput.ToCharArray();
            int[] coordinates = new int[3];
            for (int i = 0; i < inputArray.Length; i++)
            {
                if (i == 2)
                {
                    switch (inputArray[2])
                    {
                        case 'N':
                            coordinates[i] = (int)Direction.N;
                            break;
                        case 'E':
                            coordinates[i] = (int)Direction.E;
                            break;
                        case 'S':
                            coordinates[i] = (int)Direction.S;
                            break;
                        case 'W':
                            coordinates[i] = (int)Direction.W;
                            break;
                    }
                    break;
                }


                coordinates[i] = (int)Char.GetNumericValue(inputArray[i]);

            }
            return coordinates;
        }
      public int[] CreateBorder(string borderStringLine)
        {
            /*
            It is the function that takes the coordinates of
            the boundaries from the first line of the input to
            numeric and record them in order to ensure that Rover
            does not go beyond the plateau and generate the necessary warnings. 
             */
            int[] border = new int[2];
            var iterator = 0;
            foreach (var item in borderStringLine.Split(null))
            {
               
                border[iterator] = Convert.ToInt32(item);
                iterator++;
            }
            return border;
        }
      public int[] IsLimit(int[] coordinates, int[] border)
        {

            /*
            The isLimit is a function that checks whether
            the rover has crossed the plateau boundaries
            and prevents it from passing. It is used in
            GetRoute function. 
             */

            int[] reachedExceededLimit = new int[4];
            //reachedExceededLimit { minx, maxx, miny, maxy }

            if (coordinates[0]<=0)
            {
                reachedExceededLimit[0] = 1;
            }
            else if(coordinates[0] >= border[0])
            {
                reachedExceededLimit[1] = 1;
            }
            else if(coordinates[1] <= 0)
            {
                reachedExceededLimit[2] = 1;
            }
            else if (coordinates[1] >= border[0])
            {
                reachedExceededLimit[3] = 1;
            }
            else
            {
                for (int i = 0; i < reachedExceededLimit.Length; i++)
                {
                    reachedExceededLimit[i] = 0;
                }
            }
            return reachedExceededLimit;
        }
      #endregion
    }

    #region Enums
    enum DirectionHeading
    {
        N = 1,
        W = -1,
        S = -1,
        E = 1
        /*
         These enums are for manipulations on values
        in the coordinate plane. For example, if the
        M command comes while the direction is in the
        N direction; If we are to call the coordinates
        [x, y, N], the new coordinate becomes [x, y + 1, N]. 
         */
    }
    enum Heading
    {
        L = -1,
        R = 1,
        M = 0
        /*
         The necessary explanation is made in the Direction enum section.
         */
    }
    enum Direction
    {
        N,  //North             -8 -4 0 4
        E,  //East              -7 -3 1 5
        S,  //South            -6 -2 2 6
        W,  //West             -5 -1 3 7 

        /*
         The heading part is the part where the commands
         that determine the rover's actions are listed.

        For example, when a rover in the N direction receives
        the L command, it takes the W direction. In order to
        represent these directions numerically, from 0 to 3 in
        the clockwise direction, they are listed as N-E-S-W. 
        These numerical values represent the 3rd element in the 
        3-element vector indicating the coordinates and direction of Rover. 
         
        Also, the 3rd element in this vector is always
        valued in mode 4. For example, when it is 4, it
        automatically turns to 0. Or when it is -1, 
        it automatically returns to 3. 
         */
    }
    #endregion
}