public static class MetroParser
{
    public static List<MetroLine> ParseCsv(string csvFilePath, char separator = ',')
    {
        var metroLines = new List<MetroLine>();
        using (var reader = new StreamReader(csvFilePath))
        {
            var lineName = "";
            var stations = new List<string>();
            var stationTimes = new Dictionary<(string, string), int>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(separator);

                var currentLineName = values[0];
                var startStation = values[1];
                var endStation = values[2];
                var timeNeeded = int.Parse(values[3]);

                if (currentLineName != lineName)
                {
                    if (stations.Count > 1 && stationTimes.Count > 0)
                    {
                        var metroLine = new MetroLine(lineName, stations, stationTimes);
                        metroLines.Add(metroLine);
                    }
                    lineName = currentLineName;
                    stations = new List<string>();
                    stationTimes = new Dictionary<(string, string), int>();
                }

                if (!stations.Contains(startStation))
                {
                    stations.Add(startStation);
                }
                if (!stations.Contains(endStation))
                {
                    stations.Add(endStation);
                }

                stationTimes[(startStation, endStation)] = timeNeeded;
            }

            if (stations.Count > 1 && stationTimes.Count > 0)
            {
                var metroLine = new MetroLine(lineName, stations, stationTimes);
                metroLines.Add(metroLine);
            }
        }
        return metroLines;
    }
}

