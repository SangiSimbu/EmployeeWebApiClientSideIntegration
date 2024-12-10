using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperModule
{
    public static class HelperModules
    {
        public static int GetIntegerInput(string prompt)
        {
            int result;
            Console.Write(prompt);

            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                Console.Write(prompt);
            }
            return result;
        }

        public static decimal GetDecimalInput(string prompt)
        {
            decimal result;
            Console.Write(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write(prompt);
            }
            return result;
        }

        public static string GetStringInput(string prompt)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(input = Console.ReadLine()))
            {
                Console.WriteLine("Invalid. Please try again.");
                Console.Write(prompt);
            }
            return input;
        }
    }
}
