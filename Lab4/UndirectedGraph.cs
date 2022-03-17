using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab4
{
	public class UndirectedGraph
	{
		public List<Node> Nodes { get; set; }

		public UndirectedGraph()
		{
			Nodes = new List<Node>();
		}

		public UndirectedGraph(string path)
		{
			Nodes = new List<Node>();

			List<string> lines = new List<string>();

			try
			{
				using (StreamReader streamReader = new StreamReader(path))
				{

					string line;
					while ((line = streamReader.ReadLine()) != null)
					{
						line = line.Trim();
						if (line == "" || line[0] == '#')
						{
							continue;
						}

						lines.Add(line);
					}
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return;
			}

			// process the lines

			if (lines.Count < 1)
			{
				Console.WriteLine("Empty graph file");
				return;
			}

			string[] nodeNames = Regex.Split(lines[0], @"\W+");
			foreach (var name in nodeNames)
			{
				Nodes.Add(new Node(name));
			}

			for (int i = 1; i < lines.Count; i++)
            {
				// extract the node names

				 nodeNames = Regex.Split(lines[i], @"\W+");
				if( nodeNames.Length < 2)
                {
					throw new Exception("Two nodes are required for each edge.");
                }

				// add edge between those nodes
				AddEdge(nodeNames[0], nodeNames[1]);
			}

		}

		public void AddEdge(string node1name, string node2name)
        {
			// find the 2 node objects
			Node node1 = FindNode(node1name);
			Node node2 = FindNode(node2name);

			if( node1 == null || node2 ==null)
            {
				throw new Exception("Invalid node name");
            }

			// add node2 as a neighbor to node 1
			node1.Neighbors.Add(node2);

			// add node 1 as a neighbor to node 2
			node2.Neighbors.Add(node1);

		}

		protected Node FindNode(string name)
        {
			var node = Nodes.Find(node => node.Name == name);

			return node;
        }


		// TODO
		public bool IsReachable(string node1name, string node2name)
        {
			// find the 2 node objects
			Node node1 = FindNode(node1name);
			Node node2 = FindNode(node2name);

			if (node1 == null || node2 == null)
			{
				throw new Exception("Invalid node name");
			}

			ResetNodeColor();

			var result = IsReachableExplore(node1, node2);

            Console.WriteLine("result: " + result);

			return node2.Color != Color.White;
        }

		protected bool IsReachableExplore(Node currentNode, Node endingNode)
        {
			currentNode.Color = Color.Gray;

			if (currentNode==endingNode)
            {
				return true;
            }

			foreach (var neighbor in currentNode.Neighbors)
			{
				if(neighbor.Color == Color.White)
                {
					var result = IsReachableExplore(neighbor, endingNode);
					if (result)
                    {
						return result;
                    }
                }
			}

			currentNode.Color = Color.Black;

			return false;
		}

		public void ResetNodeColor()
		{
			foreach(var node in Nodes)
            {
				node.Color = Color.White;
            }

		}



        public override string ToString()
        {
			string str="";


			foreach( Node node in Nodes)
            {
				str += node.Name;
				str += " has neighbors: ";

				foreach( Node neighbor in node.Neighbors)
                {
					str += neighbor.Name;
					str += ", ";
                }

				str += ".";
				str += Environment.NewLine;

            }


            return str;
        }

    }
}

