using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph1 = new UndirectedGraph("../../../graphs/graph5.txt");


            Console.WriteLine(graph1);

            Console.WriteLine(graph1.IsReachable("madison", "lauren"));


        }
    }
}

