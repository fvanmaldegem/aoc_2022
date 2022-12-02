namespace AocDay1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines("./input");

            Console.WriteLine($"Day 1: {Solve1(input)}");
            Console.WriteLine($"Day 2: {Solve2(input)}");
        }

        static string Solve1(string[] input)
        {
            int sum = 0;
            int highest = 0;

            foreach (string line in input)
            {
                if(line == "") {
                    if (sum > highest) highest = sum;
                    sum = 0;
                    continue;
                }

                sum += int.Parse(line);
            }

            return highest.ToString();
        }

        static string Solve2(string[] input)
        {
            int sum = 0;
            List<int> topThree = new List<int>() { 0, 0, 0 };

            foreach (string line in input) {
                if(line == "")
                {
                    // Sort the list, so the lowest value will always be on index 0
                    topThree.Sort();
                    
                    if (topThree[0] < sum) topThree[0] = sum;
                    sum = 0; continue;
                }

                sum += int.Parse(line);
            }

            return topThree.Sum().ToString();
        }
    }
}