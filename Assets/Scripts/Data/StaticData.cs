using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static data used for visualization. Population numbers, Colors array.
/// </summary>
public static class StaticData
{
    /// <summary>
    /// Color palette from https://esklarski.github.io/canada.covid19/
    /// </summary>
    public static readonly Color[] Colors = {
        new Color(1f,         0.509804f,  0.6039216f),
        new Color(0f,         0.5803922f, 0.2235294f),
        new Color(0.6470588f, 0f,         0.6705883f),
        new Color(0.454902f,  0.6901961f, 0f),
        new Color(0.6627451f, 0.4039216f, 1f),

        new Color(0.254902f,  0.4196078f, 0f),
        new Color(1f,         0.4470588f, 0.9843137f),
        new Color(0.4784314f, 0.7294118f, 0.4745098f),
        new Color(0.282353f,  0.1098039f, 0.5921569f),
        new Color(1f,         0.4431373f, 0.1254902f),

        new Color(0.0078431f, 0.5333334f, 0.9764706f),
        new Color(0.9019608f, 0f,         0.1019608f),
        new Color(0f,         0.6117647f, 0.5568628f),
        new Color(0.9333333f, 0f,         0.3764706f),
        new Color(0f,         0.2784314f, 0.682353f),

        new Color(0.6666667f, 0.4392157f, 0f),
        new Color(0.6666667f, 0.6313726f, 0.9647059f),
        new Color(0.6392157f, 0.2862745f, 0f),
        new Color(0.2f,       0.2078431f, 0.4470588f),
        new Color(0.8666667f, 0.6196079f, 0.3529412f),

        new Color(0.827451f,  0f,         0.4431373f),
        new Color(0.2352941f, 0.2431373f, 0.02745098f),
        new Color(0.9647059f, 0.5647059f, 0.3921569f),
        new Color(0.4039216f, 0.1254902f, 0.2941177f),
        new Color(0.4666667f, 0.1607843f, 0f),
    };


    /// <summary>
    /// Provinces and their respective populations, from Wikipedia.
    /// </summary>
    public static readonly Dictionary<string, int> Population = new Dictionary<string, int>()
    {
        {"Alberta",                  4067175},
        {"British Columbia",         4648055},
        {"Canada",                  37965057},
        {"Manitoba",                 1278365},
        {"New Brunswick",             747101},
        {"Newfoundland and Labrador", 519716},
        {"Northwest Territories",      41786},
        {"Nova Scotia",               923598},
        {"Nunavut",                    35944},
        {"Ontario",                 13488494},
        {"Prince Edward Island",      142907},
        {"Quebec",                   8164361},
        {"Saskatchewan",             1098352},
        {"Yukon",                      35874}
    };

    /// <summary>
    /// Enum of case count types [confirmed, recoverd, active, deaths].
    /// </summary>
    public enum CaseType
    {
        confirmed,
        recovered,
        active,
        deaths
    }
}
