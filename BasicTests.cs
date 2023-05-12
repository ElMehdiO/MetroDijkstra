static class BasicTests

{
    public static void Test()
    {
        // Basic Testing
        // create the metro lines
        var line1 = new MetroLine("Line 1");
        var line2 = new MetroLine("Line 2");
        var line3 = new MetroLine("InterChange12");

        // add the stations and connections to line 1
        line1.AddStation("A");
        line1.AddStation("B");
        line1.AddStation("C");
        line1.AddStation("D");
        line1.AddStation("E");
        line1.AddStation("F");

        line1.AddConnection("A", "B", 2);
        line1.AddConnection("B", "C", 3);
        line1.AddConnection("C", "D", 1);
        line1.AddConnection("D", "E", 4);
        line1.AddConnection("E", "F", 2);


        // add the stations and connections to line 2
        line2.AddStation("G");
        line2.AddStation("H");
        line2.AddStation("I");
        line2.AddStation("J");
        line2.AddStation("K");

        line2.AddConnection("G", "H", 3);
        line2.AddConnection("H", "I", 2);
        line2.AddConnection("I", "J", 5);
        line2.AddConnection("J", "K", 1);

        line3.AddStation("E");
        line3.AddStation("J");
        line3.AddConnection("E", "J", 5);

        var lines = new List<MetroLine> { line1, line2, line3 };


        var adjGraph = new AdjListMetroGraph(lines);
        
        // var matGraph = new AdjMatrixMetroGraph(lines);

        var adjPath = adjGraph.GetShortestPath("A", "J");
        // var matPath = matGraph.GetShortestPath("A", "J");

        // Print Graph
        foreach (KeyValuePair<string, List<(string, int)>> vertex in adjGraph.AdjacencyList)
        {
            Console.Write(vertex.Key + ": \n");
            foreach ((string destination, int time) in vertex.Value)
            {
                Console.Write("     " + vertex.Key + ": ");
                Console.Write(destination + "(" + time + ")" + "\n");
            }
            Console.WriteLine();
        }

        // Print Shortest Path

        Console.WriteLine("Shortest Path using Adjacency List");
        foreach (string station in adjPath)
        {
            Console.WriteLine(station);
        }

        /*
        Console.WriteLine("Shortest Path using Adjacency Matrix");
        foreach (string station in matPath)
        {
            Console.WriteLine(station);
        }
        */
    }
}



