using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static StaticData;

/// <summary>
/// Stores all data for a province.
/// </summary>
public class Province
{
    public string name;
    public int population;
    public Entry[] points;

    // reference to parent object for a provinces entries
    public Transform provincialNode;
    public Color color;
    public int displayIndex;

    /// <summary>
    /// Storage of maximum values to simplifly later queries for charting height.
    /// </summary>
    public Dictionary<string, int> maxValues;
    public Dictionary<string, float> maxValuesNormalized;

    public Province(string _name, int _population, Entry[] _entries)
    {
        this.name = _name;
        this.population = _population;
        this.points = _entries;
        this.maxValues = new Dictionary<string, int>(4);
        this.maxValuesNormalized = new Dictionary<string, float>(4);
        FindMaxValues();
    }

    /// <summary>
    /// Finds maximum values and stores them in a Dictionary. Also calculates population normalized values.
    /// </summary>
    private void FindMaxValues()
    {
        foreach ( string type in Enum.GetNames( typeof(CaseType) ) )
        {
            maxValues.Add(
                type,
                this.points.Max(x => x.values[type]));

            maxValuesNormalized.Add(
                type,
                (float)this.maxValues[type] / (float)this.population * 100000f);
        }
    }
}
