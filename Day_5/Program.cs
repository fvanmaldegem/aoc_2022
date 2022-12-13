namespace AocDay5
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
            int endOfMap = 0;
            foreach(string line in input)
            {
                if (line.Equals("")) break;
                endOfMap++;
            }

            string[] map = new string[endOfMap];
            string[] instructionStrings = new string[input.Count() - endOfMap -1];

            Array.Copy(input, map, endOfMap);
            Array.Copy(input, endOfMap + 1, instructionStrings, 0, input.Count() - endOfMap - 1);
            
            List<Stack> stacks = Stack.FromMap(map);
            List<Instruction> instructions = new List<Instruction>();

            foreach(string instructionString in instructionStrings)
            {
                Instruction instruction = Instruction.FromString(instructionString);
                instruction.Perform1(stacks);
            }

            string topCrateMarks = "";
            foreach(Stack s in stacks)
            {
                topCrateMarks += s.GetTopCrate().GetContent();
            }

            return topCrateMarks;
        }

        private static string Solve2(string[] input)
        {
            int endOfMap = 0;
            foreach(string line in input)
            {
                if (line.Equals("")) break;
                endOfMap++;
            }

            string[] map = new string[endOfMap];
            string[] instructionStrings = new string[input.Count() - endOfMap -1];

            Array.Copy(input, map, endOfMap);
            Array.Copy(input, endOfMap + 1, instructionStrings, 0, input.Count() - endOfMap - 1);
            
            List<Stack> stacks = Stack.FromMap(map);
            List<Instruction> instructions = new List<Instruction>();

            foreach(string instructionString in instructionStrings)
            {
                Instruction instruction = Instruction.FromString(instructionString);
                instruction.Perform2(stacks);
            }

            string topCrateMarks = "";
            foreach(Stack s in stacks)
            {
                topCrateMarks += s.GetTopCrate().GetContent();
            }

            return topCrateMarks;
        }
    }

    class Crate
    {
        public char Mark;
        public Crate(char mark)
        {
            Mark = mark;
        }

        public char GetContent()
        {
            return Mark;
        }
    }

    class Stack
    {
        public int Index;
        public List<Crate> _crates;

        public Stack(int index, List<Crate> crates)
        {
            Index  = index;
            _crates = crates;
        }

        public void AddCrate(Crate c)
        {
            this._crates = this._crates.Append(c).ToList();
        }

        public Crate RemoveCrate()
        {
            Crate topCrate = this._crates.Last();
            this._crates.Remove(topCrate);
            return topCrate;
        }

        public void AddCrates(List<Crate> crates)
        {
            foreach (Crate c in crates)
            {
                this._crates = this._crates.Append(c).ToList();
            }
        }

        public List<Crate> RemoveCrates(int amount)
        {
            List<Crate> crates = this._crates.GetRange(this._crates.Count() - amount, amount);
            this._crates.RemoveRange(this._crates.Count() - amount, amount);
            return crates;
        }

        public static List<Stack> FromMap(string[] map)
        {
            List<Stack> stacks = new List<Stack>();

            foreach (char c in map.Last()) {
                if (c != ' ') {
                    stacks.Add(new Stack(
                        int.Parse(c.ToString()),
                        new List<Crate>()
                    ));
                }
            }

            for (int stackIndex = 0; stackIndex <= map[0].Count() / 4; stackIndex++)
            {
                int columnIndex = stackIndex * 4 + 1;
                for (int cratePos = map.Count() - 2; cratePos >= 0; cratePos--)
                {
                    char crateMark = map[cratePos][columnIndex];
                    if (crateMark == ' ') continue;
                    Crate crate = new Crate(crateMark);
                    stacks[stackIndex].AddCrate(crate);
                }
            }

            return stacks;
        }

        public Crate GetTopCrate()
        {
            return this._crates.Last();
        }
    }

    class Instruction
    {
        public int Amount, From, To;
        public Instruction(int amount, int from, int to)
        {
            Amount = amount;
            From = from;
            To = to;
        }

        public static Instruction FromString(string s)
        {
            string[] splittedString = s.Split(' ');
            int amount = int.Parse(splittedString[1]);
            int from   = int.Parse(splittedString[3]);
            int to     = int.Parse(splittedString[5]);
            return new Instruction(amount, from, to);
        }

        public void Perform1(List<Stack> stacks)
        {
            int i = 0;
            while (i < Amount)
            {
                Stack? from = stacks.Find(s => s.Index == From);
                Stack? to   = stacks.Find(s => s.Index == To);

                if (from == null || to == null)
                {
                    throw new Exception("Couldn't find index number");
                }

                Crate movingCrate = from.RemoveCrate();
                to.AddCrate(movingCrate);
                i++;
            } 
        }

        internal void Perform2(List<Stack> stacks)
        {
            Stack? from = stacks.Find(s => s.Index == From);
            Stack? to   = stacks.Find(s => s.Index == To);

            if (from == null || to == null)
            {
                throw new Exception("Couldn't find index number");
            }

            List<Crate> movingCrates = from.RemoveCrates(Amount);
            to.AddCrates(movingCrates);
        }
    }
}