using System;
using System.Collections.Generic;

namespace Lab4
{
	public enum Color
    {
		White, Gray, Black
    }

	public class Node
	{
        public string Name { get; set; }
        public List<Node> Neighbors { get; set; }
		public Color Color { get; set; }

        public Node(string name = "", Color color = Color.White)
		{
			Neighbors = new List<Node>();

			Name = name;
			Color = color;
		}
	}
}

