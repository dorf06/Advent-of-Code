using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day_14
{
    class Score
    {
        // Variables
        public String Name { get; }
        public int Value { get; set; }

        // Constructor
        public Score (String name)
        {
            Name = name;

            Value = 0;
        }

        // Compares values
        public static int CompareTo (Score a, Score b)
        {
            return -a.Value.CompareTo(b.Value);
        }
    }
    class Reindeer : IComparable<Reindeer>
    {
        // Stats
        // Name
        public String Name { get; }
        // Speed
        public int Speed { get; }
        // Time til rest is needed
        public int MaxStamina { get; }
        // Time needed to rest
        public int RestTime { get; }

        // Status
        // Distance flown
        public int Distance { get; private set; }
        public int Stamina { get; private set; }
        
        // Counters/Flags
        // Resting flag
        public bool Resting { get; private set; }
        // Time when current rest is over
        public int RestMarker { get; private set; }
        

        // Constructor
        public Reindeer (String name, int speed, int stamina, int rest)
        {
            // Set properties
            Name = name;
            Speed = speed;
            MaxStamina = stamina;
            RestTime = rest;

            // Initialize values
            Distance = 0;
            Resting = false;
            RestMarker = 0;
            Stamina = MaxStamina;
        }
        
        // Update method
        public void Update (int SimTime)
        {
            // Check if resting or not
            if (Resting)
            {
                // Check if rest is over
                if (RestMarker == SimTime)
                {
                    Resting = false;
                    Stamina = MaxStamina;
                }
            }

            // Not resting
            else
            {
                // Deplete stamina
                Stamina--;

                // Set resting values if out of stamina
                if (Stamina == 0)
                {
                    Resting = true;
                    RestMarker = SimTime + RestTime;
                }

                // Add to distance
                Distance += Speed;
            }
        }

        public override String ToString()
        {
            // "Name: {0}\t Resting: {1}\t Distance: {2}\t Rest Marker: {3}\t", current.Name, current.Resting, current.Distance, current.RestMarker
            String output = "Name: " + Name + "\t";

            if (!Resting)
                output += "Stamina:" + Stamina + "\t\t";
            else
            {
                if (RestMarker < 100)
                    output += "Rest Marker: " + RestMarker + "\t\t";
                else
                    output += "Rest Marker: " + RestMarker + "\t";
            }

            output += "Distance: " + Distance;

            return output;
        }

        public int CompareTo(Reindeer other)
        {
            // Sorts by Distance, highest first
            return -Distance.CompareTo(other.Distance);
        }
    }

    class Program
    {
        static void ReindeerStats (List<Reindeer> input)
        {
            // Status
            Console.WriteLine("Listing racers...");
            Console.WriteLine("");

            // Print out reindeer and stats
            foreach (Reindeer current in input)
            {
                Console.WriteLine("{0}\t Speed:{1}\t Stamina:{2}\t Rest Time:{3}", current.Name, current.Speed, current.Stamina, current.RestTime);
            }

            Console.WriteLine("");
        }

        static void RaceStatus (List<Reindeer> racers, int elapsedTime, List<Score> scoreboard)
        {
            Console.Clear();

            Console.WriteLine("Elasped Race Time: {0}", elapsedTime);
            Console.WriteLine("");

            foreach (Reindeer current in racers)
            {
                Console.WriteLine(current.ToString()); 
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Name \t Score");
            
            foreach (var item in scoreboard)
            {
                Console.WriteLine(item.Name + "\t" + item.Value);
            }
        }

        public static void UpdateRace(int raceTime, List<Reindeer> racers, List<Score> scoreboard)
        {
            int count = 0;

            // Update racers
            foreach (Reindeer current in racers)
            {
                current.Update(raceTime);
            }

            // Sort race standings
            racers.Sort();

            // Update scores
            // Grab highest distance
            Reindeer lead = racers[count];

            // Assign point
            scoreboard.Find(x => x.Name.Equals(lead.Name)).Value++;

            // Inc count to check next reindeer
            count++;

            // Check if there is a tie
            while (racers[count].Distance == lead.Distance)
            {
                scoreboard.Find(x => x.Name.Equals(racers[count].Name)).Value++;

                count++;
            }

            scoreboard.Sort(Score.CompareTo);
        }

        static Reindeer CreateReindeer (String input)
        {
            // Get params
            String[] fields = input.Split(' ');

            // Print name
            Console.WriteLine("{0} added to the race", fields[0]);

            return new Reindeer(fields[0], int.Parse(fields[3]), int.Parse(fields[6]), int.Parse(fields[13]));
        }

        static void Main(string[] args)
        {
            // Variables
            String input = null;
            int raceTime = 0;

            // Scoreboard
            List<Score> scoreBoard = new List<Score>();

            // Simulation settings
            const int EndTime = 2503;
            const int SimulationPace = 100;

            // Inititalize collection of reindeer
            List<Reindeer> racers = new List<Reindeer>();

            // Read file input for reindeer
            StreamReader sr = new StreamReader(@"C:\Users\cordell.wagendorf\Documents\GitHubVisualStudio\Advent-of-Code\Day 14\Day 14\input");

            // Create reindeer and add them to race
            while ((input = sr.ReadLine()) != null)
            {
                // Create reindeer
                Reindeer temp = CreateReindeer(input);

                // Add reindeer to race
                racers.Add(temp);

                // Add reindeer to scoreboard
                scoreBoard.Add(new Score(temp.Name));
            }

            Console.WriteLine("");

            // Stats
            ReindeerStats(racers);

            Console.WriteLine("\n\nPress any key to start the race...");
            Console.ReadLine();
           
            // Simulation loop
            while (raceTime <= EndTime)
            {
                // Status
                RaceStatus(racers, raceTime, scoreBoard);

                // Update race
                UpdateRace(raceTime, racers, scoreBoard);

                // Advance clock
                raceTime++;

                // Simulation Pace
                Thread.Sleep(SimulationPace);

                // Debug pause
                //Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}
