using System.ComponentModel;
using Microsoft.Win32.SafeHandles;

namespace AocDay6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines("./test-input");

            Console.WriteLine($"Day 1: {Solve1(input)}");
            Console.WriteLine($"Day 2: {Solve2(input)}");
        }

        private static string Solve1(string[] input)
        {
            List<string> currentPath = new List<string>();
            Dir root = new Dir("/", null);

            for(int i = 0; i < input.Count(); i++)
            {
                string currentLine = input[i];
                string[] currentLineSplit = currentLine.Split(' ');

                string command = currentLineSplit[1];
                string[] args  = currentLineSplit[2..];

                if (command == "cd")
                {
                    if (args[0] == "..")
                    {
                        currentPath.RemoveAt(currentPath.Count - 1);
                    } else {
                        if (args[0] == "/")
                        {
                            currentPath = new List<string>();
                        } else {
                            currentPath.Add(args[0]);
                        }
                    }
                }

                if (command == "ls")
                {
                    for(int j = i+1; j < input.Count(); j++)
                    {
                        string line = input[j];
                        if (IsCommand(line)) {
                            i = j-1;
                            break;
                        }
                        
                        string name = line.Split(' ')[1];
                        Dir? parent = root.GetDirByPath(currentPath);

                        if (line.StartsWith("dir"))
                        {
                            if (root.GetDirByPath(currentPath.Append(name)) == null) {
                                Dir newDir = new Dir(name, parent);
                                if (parent != null)
                                {
                                    parent.AddNode(newDir);
                                }
                            }

                            continue;
                        }

                        if (parent == null) {
                            throw new Exception("parent can't be null for a File");
                        }

                        int size = int.Parse(line.Split(' ')[0]);
                        if (root.GetByPath(currentPath.Append(name)) == null)
                        {
                            File newFile = new File(name, size, parent);
                            parent.AddNode(newFile);
                        }
                    }
                }
            }

            Console.WriteLine(root.ToString());

            return "";
        }

        private static string Solve2(string[] input)
        {
            return "";
        }

        private static bool IsCommand(string line)
        {
            return line.StartsWith("$");
        }
    }

    abstract class Node
    {
        protected string _name;
        protected Dir? _parent;

        public Node(string name, Dir? parent)
        {
            _name   = name;
            _parent = parent;
        }

        public string GetPath()
        {
            if (_parent == null) {
                return _name;
            }

            return _parent.GetPath() + "/" + _name;
        }

        virtual public Node? GetByPath(string path)
        {
            if (this.GetPath() == path) {
                return this;
            }

            return null;
        }

        virtual public Node? GetByPath(IEnumerable<string> path)
        {
            if (path.Count() == 0) {
                return this.GetByPath("/");
            }

            string spath = "/" + string.Join('/', path);
            return this.GetByPath(spath);
        }

        abstract public int GetSize();
    }

    class Dir : Node
    {
        private List<Node> _children;

        public Dir(string name, Dir? parent) : base(name, parent)
        {
            _children = new List<Node>();
        }

        override public int GetSize()
        {
            int totalSize = 0;
            foreach(Node child in _children)
            {
                totalSize += child.GetSize();
            }

            return totalSize;
        }

        public void AddNode(Node n)
        {
            _children.Add(n);
        }

        override public Node? GetByPath(string path)
        {
            if (this.GetPath() == path)
            {
                return this;
            }

            foreach (Node n in _children)
            {
                Node? res = n.GetByPath(path);
                if (res != null)
                {
                    return res;
                }
            }

            return null;
        }

        public Dir? GetDirByPath(string path)
        {
            if (path == _name) return this;

            if (_parent != null) {
                string parentPath = _parent.GetPath();
                if (parentPath + "/" + _name == path) {
                    return this;
                }
            }

            foreach(Node child in _children)
            {
                Dir? dir = child as Dir;
                if (dir == null) continue;

                Dir? res = dir.GetDirByPath(path);
                if (res != null)
                {
                    return res;
                }
            }

            return null;
        }

        public Dir? GetDirByPath(IEnumerable<string> path)
        {
            if (path.Count() == 0) {
                return this.GetDirByPath("/");
            }

            string spath = "/" + string.Join('/', path);

            return this.GetDirByPath(spath);
        }
    }

    class File : Node
    {
        private int _size;

        public File(string name, int size, Dir parent) : base(name, parent)
        {
            _size = size;
        }

        override public int GetSize()
        {
            return _size;
        }
    }
}
