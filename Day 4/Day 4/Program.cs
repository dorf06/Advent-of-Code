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
            // Input
            String input = "yzbqklnj";

            // Hash subset length
            int subLength = 6;

            // Loop variables
            long winner = 0;
            long iteration = 0;
            bool found = false;

            // Hash conversion variables
            byte[] encoded, hash;
            String output, first;

            while (!found)
            {
                // Get byte array representation
                encoded = new UTF8Encoding().GetBytes(input + iteration);

                // Get MD5
                hash = new MD5CryptoServiceProvider().ComputeHash(encoded);

                // Convert to string and format it without dashes and lowercase
                output = BitConverter.ToString(hash).Replace("-", String.Empty).ToLower();

                // Check first digits for 0s
                first = output.Substring(0, subLength);

                //Console.WriteLine(input + iteration + ": Hash: " + output + " First : " + first);

                if (first == "000000")
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
