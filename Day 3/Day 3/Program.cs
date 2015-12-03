using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day_3
{
    class Program
    {
        // Class for coordinate object to store position
        class Coords
        {
            // Position
            int x, y;

            public Coords (int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int getX ()
            {
                return this.x;
            }

            public int getY ()
            {
                return this.y;
            }

            public String toString()
            {
                return this.x + "," + this.y;
            }

            // Update position
            public void update (char direction)
            {
                switch (direction)
                {
                    // North
                    case '^':
                        this.y++;
                        break;

                    // South
                    case 'v':
                        this.y--;
                        break;

                    // East
                    case '>':
                        this.x++;
                        break;

                    // West
                    case '<':
                        this.x--;
                        break;

                    // Default
                    default:
                        Console.WriteLine("Bad direction");
                        break;
                }
            }
        }
        class CoordsComparer : IEqualityComparer<Coords>
        {
            public bool Equals (Coords c1, Coords c2)
            {
                if ((c1.getX() == c2.getX()) && (c1.getY() == c2.getY()))
                {
                    //Console.WriteLine(c1.getX() + ", " + c1.getY() + " : " + c2.getX() + ", " + c2.getY());

                    //Console.WriteLine("House found");

                    return true;
                }
                else
                {
                    //Console.WriteLine("House not found");

                    return false;
                }
            }

            public int GetHashCode(Coords obj)
            {
                int hCode = obj.getX() ^ obj.getY();
                return hCode.GetHashCode();
            }
        }

        class Santa
        {
            // Position
            Coords position;

            // Constructor
            public Santa ()
            {
                // Set position to origin
                position = new Coords(0, 0);
            }

            // Returns current position
            public Coords getPosition ()
            {
                return this.position;
            }

            // Move Santa
            public void move (char direction)
            {
                Console.WriteLine("Moving {0}..", direction);

                // Pass to the Coords update
                this.position.update(direction);

                //Console.WriteLine("New position: " + this.position.getX() + ", " + this.position.getY());
            }
        }

        static void Main(string[] args)
        {
            /*
                Gonna have to probably use a coordinate system and store visited houses in a seperate container.
                Should be able to use a key search to check for originals and then grab the size at the end.
                Could make a Santa object and update his position based on the input, then grab current position and store it.
            */

            //CoordsComparer coordComp = new CoordsComparer();

            // Containers
            HashSet<String> visitedList = new HashSet<String>();

            // Grab input
            StreamReader sr = new StreamReader(@"C:\Users\cordell.wagendorf\Documents\GitHubVisualStudio\Advent-of-Code\Day 3\Day 3\input");
            String input = sr.ReadLine();

            // Close file
            sr.Close();

            // Create an instance of Santa and Robo-Santa
            Santa santa = new Santa();
            Santa roboSanta = new Santa();

            // Push origin position to stack
            visitedList.Add(santa.getPosition().toString());

            // Current chars from input
            char[] directions = input.ToCharArray();

            // Run through directions
            for ( int i = 0; i < directions.Length; i++)
            {
                // Current santa
                Santa current;

                // Update Santas, santa even, robo odd
                if ((i % 2) == 0)
                    current = santa;
                else
                    current = roboSanta;

                current.move(directions[i]);

                // Grab position
                Coords temp = current.getPosition();

                Console.WriteLine(temp.getX() + ", " + temp.getY());

                // Add into hashset
                visitedList.Add(temp.toString());

                //Console.WriteLine("Count: " + visitedList.Count());

                // Pause
                //Thread.Sleep(1000);
            }

            Console.WriteLine("Visisted {0} unique houses", visitedList.Count());

            Console.ReadLine();
        }
    }
}
