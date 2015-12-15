using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_14
{
    class Reindeer
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
    }

    class Program
    {
        static void ReindeerStats (List<Reindeer> input)
        {
            // Status
            Console.WriteLine("Listing racers...");

            // Print out reindeer and stats
            foreach (Reindeer current in input)
            {
                Console.WriteLine("{0} Speed:{1} Stamina:{2} Rest Time:{3}", current.Name, current.Speed, current.Stamina, current.RestTime);
            }

            Console.WriteLine("Done");
        }

        static void RaceStatus (List<Reindeer> racers, int elapsedTime)
        {
            Console.Clear();

            Console.WriteLine("Elasped Race Time: {0}", elapsedTime);
            Console.WriteLine("");

            foreach (Reindeer current in racers)
            {
                Console.WriteLine("Name: {0}\t Resting: {1}\t Distance: {2}\t Rest Marker: {3}\t", current.Name, current.Resting, current.Distance, current.RestMarker); 
            }
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

            // Simulation settings
            const int EndTime = 2503;

            // Inititalize collection of reindeer
            List<Reindeer> racers = new List<Reindeer>();

            // Read file input for reindeer
            StreamReader sr = new StreamReader(@"C:\Users\cordell.wagendorf\Documents\GitHubVisualStudio\Advent-of-Code\Day 14\Day 14\input");

            while ((input = sr.ReadLine()) != null)
            {
                racers.Add(CreateReindeer(input));
            }
           
            // Simulation loop
            while (raceTime <= EndTime)
            {
                // Status
                RaceStatus(racers, raceTime);

                // Update racers
                foreach (Reindeer current in racers)
                {
                    current.Update(raceTime);
                }

                // Advance clock
                raceTime++;

                // Debug pause
                //Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}
