using System;
using System.Threading.Tasks;
using UnityEngine;
using net = System.Net;

/// <summary>
/// <para>Very simple download class.</para>
/// <para>Downloads odwg-data.csv dataset from:</para>
/// https://github.com/esklarski/canada.covid19
/// </summary>
public static class WebClient
{
    // hardcoded resource location
    static readonly string URL = "https://raw.githubusercontent.com/esklarski/canada.covid19/master/dataset/odwg-data.csv";

    // where should this be put when in webgl app?
    public static readonly string DestinationPath = Application.persistentDataPath + "/odwg-data.csv";

    /// <summary>
    /// <para>Downloads data csv, then calls passed Method.</para>
    /// </summary>
    /// <param name="callback">Data ready method.</param>
    public static async Task DownloadData(Action callback)
    {
        using (net.WebClient client = new net.WebClient())
        {
            await Task.Run(delegate { client.DownloadFile(URL, DestinationPath); });
        }

        callback();
    }
}
