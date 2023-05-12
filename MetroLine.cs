public class MetroLine
{
    public string Name { get; }
    public List<string> Stations { get; }
    public Dictionary<(string, string), int> StationTimes { get; }

    public MetroLine(string name)
    {
        Name = name;
        Stations = new List<string>();
        StationTimes = new Dictionary<(string, string), int>();
    }

    public MetroLine(string name, List<string> stations, Dictionary<(string, string), int> stationTimes)
    {
        Name = name;
        Stations = stations;
        StationTimes = stationTimes;
    }

    public void AddStation(string station)
    {
        Stations.Add(station);
    }

    public void AddConnection(string start, string end, int time)
    {
        if (!Stations.Contains(start) || !Stations.Contains(end))
        {
            throw new ArgumentException("Both stations must be part of the line.");
        }

        if (Stations.IndexOf(end) != Stations.IndexOf(start) + 1)
        {
            throw new ArgumentException("Stations must be directly connected.");
        }

        var connection = (start, end);
        if (StationTimes.ContainsKey(connection))
        {
            throw new ArgumentException("Connection already exists.");
        }

        StationTimes.Add(connection, time);
    }

    public bool HasStation(string stationName)
    {
        foreach (var station in Stations)
        {
            if (station == stationName)
            {
                return true;
            }
        }
        return false;
    }
}