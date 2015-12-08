using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_5
{
    class Program
    {
        #region Testing Methods
        public static int VowelCount (String input)
        {
            // Vowel list
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

            int vowelCount = 0;

            // Count vowels in input
            for (int i = 0; i < input.Length; i++)
            {
                // Check if char is in the vowel hashset
                if (vowels.Contains(input[i]))
                    vowelCount++;
            }

            // Return result
            return vowelCount;
        }

        public static bool DoubleLetter (String input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                // Check if current char equals next char
                if (input[i] == input[i + 1])
                    return true;
            }

            // Default case
            return false;
        }

        public static bool secondHalfRuleOne(String input)
        {
            String keyString = null;
            String searchString = null;

            // Pair of two letters appear at least twice without overlap
            for (int i = 0; i < input.Length - 1; i++)
            {
                // Run through all the two letter substrings
                keyString = input.Substring(i, 2);

                searchString = input.Substring(i + 2);
                
                for (int ii = 0; ii < searchString.Length - 1; ii++)
                {
                    // Search for substring within searchString
                    if (searchString.Substring(ii, 2) == keyString)
                        return true;
                }
            }

            return false;
        }

        public static bool secondHalfRuleTwo (String input)
        {
            // One letter repeats with one letter between them
            for (int i = 0; i < input.Length - 2; i++)
            {
                // If current letter matches letter after next
                if (input[i] == input[i + 2])
                    return true;
            }

            // Default case
            return false;
        }

        public static bool CheckString (String input)
        {
            if (secondHalfRuleOne(input) && secondHalfRuleTwo(input))
                return true;
            else
                return false;
        }

        public static bool CheckStringOld (String input)
        {
            // True = Nice string
            // Check for Naughty strings
            // Check for Naughty substrings (ab, cd, pq, xy)
            if ((input.Contains("ab")) || (input.Contains("cd")) || (input.Contains("pq")) || (input.Contains("xy")))
                return false;

            // Contains at least 3 vowels (aeiou)
            if (VowelCount(input) < 3)
                return false;

            // Contains at least one double letter
            if (!DoubleLetter(input))
                return false;
                        
            // Default case - Nice
            return true;
        }
        #endregion

        #region Modes
         // Manual Mode
        // Single string testing
        public static void ModeManual ()
        {
            while (true)
            {
                String input = null;

                // Get input
                Console.WriteLine("Type 'Main' to go back to the menu");
                Console.WriteLine("Please type a string to check:");
                Console.WriteLine("");
                input = Console.ReadLine().ToLower();

                Console.Clear();

                // Back to main menu
                if (input == "main")
                    break;

                // Check input
                if (CheckString(input))
                    Console.WriteLine("String is Nice");
                else
                    Console.WriteLine("String is Naughty");

                Console.WriteLine("");
            }
        }

        // Batch Mode
        // Submit multiple strings from user
        public static void ModeBatch ()
        {
            while (true)
            {
                Stack<String> inputStack = new Stack<string>();
                String input = null;

                // Count variables
                int countNice = 0;
                int countNaughty = 0;
                int countTotal = 0;

                // Get input
                Console.WriteLine("Type 'Main' to go back to the menu");
                Console.WriteLine("Please type strings to check and enter 'Done' to process:");
                Console.WriteLine("");

                input = Console.ReadLine().ToLower();

                while (!input.Equals("done") && !input.Equals("main"))
                {
                    // Push input to stack
                    inputStack.Push(input);

                    input = Console.ReadLine().ToLower();
                }

                while (inputStack.Count() > 0)
                {
                    // Get input to process
                    input = inputStack.Pop();

                    // Check input
                    if (CheckString(input))
                        countNice++;
                    else
                        countNaughty++;

                    countTotal++;
                }

                // Back to main menu
                if (input == "main")
                {
                    Console.Clear();
                    break;
                }

                // Totals
                Console.WriteLine("{0} total strings", countTotal);
                Console.WriteLine("{0} nice Strings", countNice);
                Console.WriteLine("{0} naughty strings", countNaughty);

                Console.WriteLine("");

                Console.WriteLine("Press any key to continue...");

                Console.ReadLine();

                Console.Clear();
            }
        }

        // File Mode
        // Select file for input
        public static void ModeFile ()
        {
            while (true)
            {
                Stack<String> inputStack = new Stack<string>();
                String input = null;

                // Count variables
                int countNice = 0;
                int countNaughty = 0;
                int countTotal = 0;

                // Get input
                Console.WriteLine("Type 'Main' to go back to the menu");
                Console.WriteLine("Please type the path of the input file:");
                Console.WriteLine("");

                input = Console.ReadLine();

                //if (input == "input")
                //input = @"C:\Users\cordell.wagendorf\Documents\GitHubVisualStudio\Advent-of-Code\Day 5\Day 5\puzzleInput";

                // Back to main menu
                if (input == "main")
                {
                    Console.Clear();
                    break;
                }

                // Make new SR for file
                StreamReader sr = new StreamReader(input);

                // Read file into stack
                while ((input = sr.ReadLine()) != null)
                    inputStack.Push(input);
                
                while (inputStack.Count() > 0)
                {
                    // Get input to process
                    input = inputStack.Pop();

                    // Check input
                    if (CheckString(input))
                        countNice++;
                    else
                        countNaughty++;

                    countTotal++;
                }

                // Totals
                Console.WriteLine("{0} total strings", countTotal);
                Console.WriteLine("{0} nice Strings", countNice);
                Console.WriteLine("{0} naughty strings", countNaughty);

                Console.WriteLine("");

                Console.WriteLine("Press any key to continue...");

                Console.ReadLine();

                Console.Clear();
            }
        }
        #endregion

        static void Main(string[] args)
        {
            String input = null;
            bool result = false;

            // Main menu loop
            while (true)
            {
                // Main text menu with string checking, also include a batch mode and file mode
                Console.WriteLine("Welcome to the Naughty or Nice String Checker...");
                Console.WriteLine("");
                Console.WriteLine("Modes:");
                Console.WriteLine("");

                // Available Modes
                Console.WriteLine("\t[M]anual");
                Console.WriteLine("\t[B]atch");
                Console.WriteLine("\t[F]ile");
                Console.WriteLine("");

                Console.WriteLine("Please enter which mode you would like:");
                Console.WriteLine("");

                // Read mode selection
                input = Console.ReadLine().ToLower();

                Console.Clear();

                // Change into selected mode
                switch (input[0])
                {
                    case 'm':
                        ModeManual();
                        break;

                    case 'b':
                        ModeBatch();
                        break;

                    case 'f':
                        ModeFile();
                        break;

                    default:
                        Console.WriteLine("Invalid selection, please try again");
                        break;
                }
            }
        }
    }
}
