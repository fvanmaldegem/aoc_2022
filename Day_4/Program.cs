using System.ComponentModel.DataAnnotations;

namespace AocDay4
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
            int fullyContainedPairs = 0;
            foreach (string line in input)
            {
                Pair pair = new Pair(line);
                if (pair.PairFullyContains()) fullyContainedPairs++;
            }

            return fullyContainedPairs.ToString();
        }

        private static string Solve2(string[] input)
        {
           int overlappingPairs = 0;
            foreach (string line in input)
            {
                Pair pair = new Pair(line);
                if (pair.PairHasOverlap()) overlappingPairs++;
            }

            return overlappingPairs.ToString();
        }
    }

    class Pair
    {
        public ElveAssignment Elve0;
        public ElveAssignment Elve1;
        public Pair(string line)
        {
                string[] pair = line.Split(',');
                Elve0 = new ElveAssignment(pair[0]);
                Elve1 = new ElveAssignment(pair[1]);
        }

        public bool PairFullyContains() => Elve0.FullyContains(Elve1) || Elve1.FullyContains(Elve0);
        public bool PairHasOverlap() => Elve0.HasOverlap(Elve1) || Elve1.HasOverlap(Elve0);
    }

    class ElveAssignment
    {
        public int From;
        public int To;
        public ElveAssignment(string assignment)
        {
            string[] range = assignment.Split('-');
            From = int.Parse(range[0]);
            To   = int.Parse(range[1]);
        }

        public bool FullyContains(ElveAssignment other) => this.From <= other.From && this.To >= other.To;
        public bool HasOverlap(ElveAssignment other) => (this.From <= other.From && this.To >= other.From) || (this.To >= other.From && this.To <= other.To);
    }
}
