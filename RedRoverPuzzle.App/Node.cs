﻿namespace RedRoverPuzzle
{
    public class Node
    {
        public string Name { get; set; }
        public List<Node> Children { get; } = new();

        public Node(string name) => Name = name;
    }
}
