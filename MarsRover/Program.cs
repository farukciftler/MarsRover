using System;

namespace MarsRover
{
    class Program

    {
        enum DirectionHeading
        {
            N = 1,
            W = -1,
            S = -1,
            E = 1
        }

        enum Heading
        {
            L = -1,
            R = 1,
            M = 0
        }
        enum Direction
        {
            N,//North             -8 -4 0 4
            E, //East              -7 -3 1 5
            S,//South            -6 -2 2 6
            W,//West             -5 -1 3 7
            
            
        }




        public class Rover
        {
            public int[] Coordinates { get; set; }
            public string Directions { get; set; }


        }
        static void Main(string[] args)
        {

            int[] GetRoute(int[] coordinates, string directions, int[] border)
            {
                int[] cd = coordinates;
                var rotate = directions.ToCharArray();
              
                foreach (var item in rotate)
                {
                    if (cd[2] < 0)
                    {
                        cd[2] = cd[2] + 4;
                    }
                    else if (cd[2] > 3)
                    {
                        cd[2] = cd[2] - 4;
                    }
                    if (cd[2]<0)
                    {
                        cd[2] = cd[2] + 4;
                    }
                    if (item == 'L')
                    {
                        cd[2] = (int)(cd[2] + Heading.L);
                    }
                    else if (item == 'R')
                    {
                        cd[2] = (int)(cd[2] + Heading.R);
                    }
                    else if (item == 'M')
                    {
                        if (cd[2] == (int)Direction.N)
                        {
                            cd[1] = (int)(cd[1] + DirectionHeading.N);

                        }
                        else if (cd[2] == (int)Direction.S)
                        {
                            cd[1] = (int)(cd[1] + DirectionHeading.S);
                        }
                        else if (cd[2] == (int)Direction.W)
                        {
                            cd[0] = (int)(cd[0] + DirectionHeading.W);
                        }
                        else if (cd[2] == (int)Direction.E)
                        {
                            cd[0] = (int)(cd[0] + DirectionHeading.E);
                        }
                    }

                };
                return cd;

            }

            string ConvertResult(int[] coordinates)
            {
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
            int[] ConvertToCoordinates(string input)
            {
                string trimmedInput= input.Replace(" ", "");
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


            string input =
                @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
";
            string[] inputArray = input.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int[] border = new int[2];

            foreach (var item in inputArray[0].Split(null))
            {
                var c = 0;
                border[c] = Convert.ToInt32(item);
                c++;
            }

            for (int i = 1; i < inputArray.Length/2+1; i++)
            {
                Rover testRover = new Rover();
                int[] numbers = ConvertToCoordinates(inputArray[i*2-1]);
                int[] result = { 0, 0, 0 };
                testRover.Coordinates = numbers;
                testRover.Directions = inputArray[i * 2];
                result = GetRoute(testRover.Coordinates, testRover.Directions, border);
                Console.WriteLine(ConvertResult(result));
            }
        }
    }
}
