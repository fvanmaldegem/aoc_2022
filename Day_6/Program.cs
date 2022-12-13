using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AocDay6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines("./input");

            Console.WriteLine($"Day 1: {Solve1(input)}");
            Console.WriteLine($"Day 2: {Solve2(input)}");
        }

        private static string Solve1(string[] input)
        {
            string datastream = input[0];
            char[] buffer = new char[4];

            int index = 0;
            foreach (char c in datastream)
            {
                index++;
                buffer = ShiftL(buffer, c);
                if (AllDifferent(buffer)) {
                    break;  
                }
            }

            return index.ToString();
        }

        private static string Solve2(string[] input)
        {
            string datastream = input[0];
            char[] buffer = new char[14];

            int index = 0;
            foreach (char c in datastream)
            {
                index++;
                buffer = ShiftL(buffer, c);
                if (AllDifferent(buffer)) {
                    break;
                }
            }

            return index.ToString();
        }

        private static char[] ShiftL(char[] buffer, char newChar)
        {
            for (int i = 0; i < buffer.Count() - 1; i++)
            {
                buffer[i] = buffer[i+1];
            }

            buffer[buffer.Count() - 1] = newChar;
            return buffer;
        }

        private static bool AllDifferent(char[] buffer)
        {
            for (int i = 0; i < buffer.Count(); i++)
            {
                for (int j = i+1; j < buffer.Count(); j++)
                {
                    if (buffer[i] == '\0' || buffer[j] == '\0' || buffer[i] == buffer[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
