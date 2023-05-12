static class ParseTests
{
    public static void Test()
    {
        // Parse the CSV file and get a list of MetroLine objects
        List<MetroLine> lines = MetroParser.ParseCsv("metro_lines.csv", ';');

        // Create a new instance of the MetroGraph class and pass in the list of MetroLine objects
        var adjGraph = new AdjListMetroGraph(lines);
        
        //var matGraph = new AdjMatrixMetroGraph(lines);
       
        // Find the shortest path between Station 1 and Station 10
        List<string> adjShortestPath = adjGraph.GetShortestPath("Station 1", "Station 10");
        
        //List<string> matShortestPath = matGraph.GetShortestPath("Station 1", "Station 10");

        // Print out the shortest path
        Console.WriteLine("Shortest path from Station 1 to Station 10 using AdjacencyList:");
        foreach (string station in adjShortestPath)
        {
            Console.WriteLine(station);
        }
        /*
        Console.WriteLine("Shortest path from Station 1 to Station 10 using AdjacencyMatrix:");
        foreach (string station in matShortestPath)
        {
            Console.WriteLine(station);
        }
        */

    }
}



