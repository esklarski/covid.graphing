using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// <para>Handles csv file odwg-data.csv from:</para>
/// https://raw.githubusercontent.com/esklarski/canada.covid19/master/dataset/odwg-data.csv
/// </summary>
public static class CsvParser
{
    /// <summary>
    /// Parses csv file into Entry objects.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>List containing all entries.</returns>
    public static List<Entry> ParseCSV(string fileName)
    {
        // TODO
        // any kind of error handling...

        List<Entry> entries = new List<Entry>();

        using (var readfile = new StreamReader(fileName))
        {
            string line;
            string[] parts;

            // remove header row
            readfile.ReadLine();

            while ((line = readfile.ReadLine()) != null)
            {
                parts = line.Split(',');
                entries.Add(new Entry(parts));
            }

            Debug.Log($"entries.Length: {entries.Count}");
        }

        return entries;
    }
}
