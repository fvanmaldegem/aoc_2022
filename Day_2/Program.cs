using System.Collections.Generic;

namespace AocDay2
{
    class Program
    {
        private static int ROCK     = 0;
        private static int PAPER    = 1;
        private static int SCISSORS = 2;

        private static int LOSE = -1;
        private static int DRAW = 0;
        private static int WIN = 1;

        private static Dictionary<string, int> OPP_SHAPE_MAP = new Dictionary<string, int>() {
            {"A", ROCK},
            {"B", PAPER},
            {"C", SCISSORS}
        };

        private static Dictionary<string, int> PLAYER_SHAPE_MAP = new Dictionary<string, int>() {
            {"X", ROCK},
            {"Y", PAPER},
            {"Z", SCISSORS}
        };

        private static Dictionary<int, int> SHAPE_SCORE_MAP = new Dictionary<int, int>() {
            {ROCK,     1},
            {PAPER,    2},
            {SCISSORS, 3}
        };

        private static Dictionary<int, int> POINT_RESULT_MAP = new Dictionary<int, int> {
            {LOSE,  0},
            {DRAW,  3},
            {WIN,   6}
        };

        private static Dictionary<string, int> PLAYER_WANTED_MAP = new Dictionary<string, int> {
            {"X", LOSE},
            {"Y", DRAW},
            {"Z", WIN}
        };

        private static int[,] WIN_MAP = new int[3,3] {
            { 0,  1, -1},
            {-1,  0,  1},
            { 1, -1,  0}
        };


        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines("./input");

            Console.WriteLine($"Day 1: {Solve1(input)}");
            Console.WriteLine($"Day 2: {Solve2(input)}");
        }

        private static string Solve1(string[] input)
        {
            int points = 0;

            foreach (string round in input) {
                string[] shapes = round.Split(' ');

                int opponent = OPP_SHAPE_MAP[shapes[0]];
                int player   = PLAYER_SHAPE_MAP[shapes[1]];
                int result   = WIN_MAP[opponent, player];

                points += SHAPE_SCORE_MAP[player];
                points += POINT_RESULT_MAP[result];
            }

            return points.ToString();
        }

        private static string Solve2(string[] input)
        {
            int points = 0;

            foreach (string round in input) {
                string[] shapes = round.Split(' ');

                int opponent = OPP_SHAPE_MAP[shapes[0]];
                int wantedResult = PLAYER_WANTED_MAP[shapes[1]];

                int chosenShape = ROCK;
                for (int i = 0; i < 3; i++)
                {
                    if (WIN_MAP[opponent, i] == wantedResult)
                    {
                        chosenShape = i;
                        break;
                    }
                }

                points += SHAPE_SCORE_MAP[chosenShape];
                points += POINT_RESULT_MAP[wantedResult];
            }

            return points.ToString();

        }

        private static int SolveForShape(int opponent, int wantedOutcome)
        {
            return 0;
        }
    }
}