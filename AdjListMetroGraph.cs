public class AdjListMetroGraph
{
    private List<string> _stations;
    private Dictionary<string, List<(string station, int time)>> _adjacencyList;

    public AdjListMetroGraph(List<MetroLine> metroLines)
    {
        // Create a list of all the stations in the metro system
        _stations = new List<string>();
        foreach (var metroLine in metroLines)
        {
            _stations.AddRange(metroLine.Stations);
        }
        _stations = _stations.Distinct().ToList();

        // Create an empty adjacency list for each station
        _adjacencyList = new Dictionary<string, List<(string station, int time)>>();
        foreach (var station in _stations)
        {
            _adjacencyList[station] = new List<(string station, int time)>();
        }

        // Populate the adjacency list with time needed to travel between stations on each metro line
        foreach (var metroLine in metroLines)
        {
            var lineStations = metroLine.Stations;
            var lineStationTimes = metroLine.StationTimes;

            for (int i = 0; i < lineStations.Count - 1; i++)
            {
                var startStation = lineStations[i];
                var endStation = lineStations[i + 1];
                var timeNeeded = lineStationTimes[(startStation, endStation)];

                _adjacencyList[startStation].Add((endStation, timeNeeded));
                _adjacencyList[endStation].Add((startStation, timeNeeded));
            }
        }
    }

    public List<string> Stations => _stations;

    public Dictionary<string, List<(string station, int time)>> AdjacencyList => _adjacencyList;

    public List<string> GetShortestPath(string startStation, string endStation)
    {
        // Initialize the distances dictionary with a large value representing infinity
        int infinity = int.MaxValue;
        Dictionary<string, int> distances = new Dictionary<string, int>();
        foreach (string station in _stations)
        {
            distances[station] = infinity;
        }

        // Initialize the visited dictionary to false for all stations
        Dictionary<string, bool> visited = new Dictionary<string, bool>();
        foreach (string station in _stations)
        {
            visited[station] = false;
        }

        // Set the distance to the starting station to 0
        distances[startStation] = 0;

        // Dijkstra's algorithm
        while (true)
        {
            // Find the vertex with the minimum distance that has not been visited yet
            int minDistance = infinity;
            string minDistanceStation = null;
            foreach (KeyValuePair<string, int> kvp in distances)
            {
                if (!visited[kvp.Key] && kvp.Value < minDistance)
                {
                    minDistance = kvp.Value;
                    minDistanceStation = kvp.Key;
                }
            }

            if (minDistanceStation == null)
            {
                break; // All reachable vertices have been visited
            }

            // Mark the vertex as visited
            visited[minDistanceStation] = true;

            // Update the distances of all adjacent vertices
            foreach ((string neighborStation, int distance) in _adjacencyList[minDistanceStation])
            {
                int newDistance = distances[minDistanceStation] + distance;
                if (newDistance < distances[neighborStation])
                {
                    distances[neighborStation] = newDistance;
                }
            }
        }

        // Construct the shortest path
        List<string> shortestPath = new List<string>();
        if (distances[endStation] == infinity)
        {
            return shortestPath; // No path exists between start and end stations
        }
        else
        {
            shortestPath.Add(endStation);
            string currentStation = endStation;
            while (currentStation != startStation)
            {
                foreach ((string neighborStation, int distance) in _adjacencyList[currentStation])
                {
                    if (distances[neighborStation] == distances[currentStation] - distance)
                    {
                        shortestPath.Add(neighborStation);
                        currentStation = neighborStation;
                        break;
                    }
                }
            }
            shortestPath.Reverse();
            return shortestPath;
        }
    }




}