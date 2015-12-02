using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day_2___Puzzle_1
{
    class Present
    {
        // Dimensions
        int width;
        int height;
        int length;

        // Calculated values
        int smallestArea;
        int totalArea;
        int requiredPaper;

        public int getRequiredPaper()
        {
            return requiredPaper;
        }
              
        public Present ( int width, int height, int length )
        {
            // Assign local variables
            this.width = width;
            this.height = height;
            this.length = length;

            Console.WriteLine(this.width + " " + this.height + " " + this.length);

            int temp;

            // 2 * l * w
            temp = 2 * length * width;

            totalArea += temp;

            smallestArea = temp / 2;

            // 2 * w * h
            temp = 2 * width * height;

            if ((temp / 2) < smallestArea)
                smallestArea = temp / 2;

            totalArea += temp;

            // 2 * h * l
            temp = 2 * height * length;

            if ((temp / 2) < smallestArea)
                smallestArea = temp / 2;

            totalArea += temp;

            // Add slack
            requiredPaper = totalArea + smallestArea;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Tally varialbes
            long totalRequiredPaper = 0;

            // Containers
            Stack<String> input = new Stack<String>();
            Stack<Present> presents = new Stack<Present>();

            // Line string
            String line;

            // Open file for reading
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\cordell.wagendorf\Documents\GitHubVisualStudio\Advent-of-Code\Day 2 - Puzzle 1\Day 2 - Puzzle 1\input.txt");

            // Fill the stack
            while ((line = file.ReadLine()) != null)
            {
                //Console.WriteLine(line);

                input.Push(line);
            }

            //Console.WriteLine("\n\nStack Size: " + input.Count());

            // Close input file
            file.Close();

            // Temp string processing variables
            String temp;
            String[] tempArray = new String[3];


            // Pop top string from input stack
            while (input.Count() != 0)
            {
                // Pop top string from input stack
                temp = input.Pop();

                Console.WriteLine("Original String" + temp);

                // Split dimensions
                tempArray = temp.Split('x');

                Console.WriteLine("Split array: " + tempArray[0] + " " + tempArray[1] + " " + tempArray[2]);

                // Make new present object and parse in dimensions
                Present tempPresent = new Present(int.Parse(tempArray[0]), int.Parse(tempArray[1]), int.Parse(tempArray[2]));

                // Push present to stack, because reasons
                presents.Push(tempPresent);

                // Add to total
                totalRequiredPaper += tempPresent.getRequiredPaper();

                Console.WriteLine(totalRequiredPaper);

                //Thread.Sleep(1000);
            }

            Console.WriteLine("Total Required Wrapping Paper: " + totalRequiredPaper);

            Console.ReadLine();
        }
    }
}
