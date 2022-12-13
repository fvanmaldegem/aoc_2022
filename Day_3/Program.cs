using System.ComponentModel.DataAnnotations;

namespace AocDay3
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
            int sum = 0;
            foreach(string line in input)
            {
                Rucksack r = new Rucksack(line);
                var intersections = r.IntersectCompartements();
                if (intersections.Count > 0)
                {
                    sum += intersections[0].GetPriority();
                }
            }

            return sum.ToString();
        }

        private static string Solve2(string[] input)
        {
            int sum = 0;
            List<Rucksack> group = new List<Rucksack>();
            foreach(string line in input)
            {
                group.Add(new Rucksack(line));
                if (group.Count() == 3)
                {
                    foreach (Item i in group[0].GetAllItems())
                    {
                        if (!group[1].ContainsItem(i)) continue;
                        if (!group[2].ContainsItem(i)) continue;
                        sum += i.GetPriority();
                        break;
                    }

                    group = new List<Rucksack>();
                }
            }
            return sum.ToString();
        }
    }

    class Rucksack
    {
        private Compartement c1;
        private Compartement c2;

        public Rucksack(string items)
        {
            int length = items.Count();
            string c1 = items.Substring(0, items.Length/2);
            string c2 = items.Substring(items.Length/2);

            this.c1 = new Compartement(c1);
            this.c2 = new Compartement(c2);
        }

        public List<Item> IntersectCompartements()
        {
            List<Item> intersections = new List<Item>();
            var itemsC1 = c1.GetItems();
            var itemsC2 = c2.GetItems();
            foreach (Item i in itemsC1)
            {
                foreach(Item j in itemsC2)
                {
                    if (i.Equals(j))
                    {
                        intersections.Add(i);
                    }
                }
            }

            return intersections;
        }

        public List<Item> GetAllItems()
        {
            List<Item> l = new List<Item>();
            l.AddRange(c1.GetItems());
            l.AddRange(c2.GetItems());
            return l;
        }

        public bool ContainsItem(Item i)
        {
            List<Item> l = this.GetAllItems();
            return l.Contains(i);
        }
    }

    class Compartement
    {
        private List<Item> items = new List<Item>();
        public Compartement(string items)
        {
            foreach(char c in items)
            {
                this.items.Add(new Item(c));
            }
        }

        public List<Item> GetItems(){
            return items;
        }
    }

    class Item
    {

        private char character;
        private int priority;

        public Item(char c)
        {
            int index = 0;
            if (char.IsUpper(c))
            {
                index = c - 64 + 26;
            }

            if (char.IsLower(c))
            {
                index = c - 96;
            }

            this.character = c;
            this.priority  = index;
        }

        public char GetCharacter()
        {
            return character;
        }

        public int GetPriority()
        {
            return priority;
        }

        public override int GetHashCode() => base.GetHashCode();
        public override bool Equals(object? obj) => this.Equals(obj as Item);
        public bool Equals(Item? i)
        {
            if (i == null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, i))
            {
                return true;
            }

            if (this.GetType() != i.GetType())
            {
                return false;
            }

            return (this.GetCharacter() == i.GetCharacter() && this.GetPriority() == i.GetPriority());
        }

    }
}