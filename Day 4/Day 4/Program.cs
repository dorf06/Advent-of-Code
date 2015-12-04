using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            String input = "yzbqklnj";
            long winner = 0;
            bool found = false;

            long iteration = 0;

            while (!found)
            {
                // Get byte array representation
                byte[] encoded = new UTF8Encoding().GetBytes(input + iteration);

                // Get MD5
                byte[] hash = new MD5CryptoServiceProvider().ComputeHash(encoded);

                // Convert to string and format it without dashes and lowercase
                String output = BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();

                // Check first five digits for 0s
                String firstFive = output.Substring(0, 5);

                Console.WriteLine(input + iteration + ": Hash: " + output + " First 5: " + firstFive);

                if (firstFive == "00000")
                {
                    found = true;

                    winner = iteration;
                }

                iteration++;
            }

            Console.WriteLine("Winner is " + winner);

            Console.ReadLine();
        }
    }
}
