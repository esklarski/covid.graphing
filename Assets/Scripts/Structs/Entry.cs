using System;
using System.Collections.Generic;

/// <summary>
/// Object to store individual entry data.
/// </summary>
public struct Entry
{
    // province name
    public string province;

    // case values
    public Dictionary<string, int> values;
    public float mortality;

    // date
    public DateTime date;
    public string dateString;

    public Entry(string[] parts)
    {
        this.province = parts[1];

        this.values = new Dictionary<string, int>
        {
            {"confirmed", int.Parse(parts[2])},
            {"recovered", int.Parse(parts[3])},
            {"active", int.Parse(parts[4])},
            {"deaths", int.Parse(parts[5])}
        };

        this.mortality = float.Parse(parts[6]);
        this.date = DateTime.Parse(parts[7]);
        this.dateString = this.date.ToShortDateString();
    }
}
