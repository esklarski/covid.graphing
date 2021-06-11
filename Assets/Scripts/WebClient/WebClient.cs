using System;
using System.Threading.Tasks;
using net = System.Net;

/// <summary>
/// Very simple download class.
/// </summary>
public static class WebClient
{
    // hardcoded resource location
    static readonly string URL = "https://raw.githubusercontent.com/esklarski/canada.covid19/master/dataset/odwg-data.csv";

    // where should this be put when in webgl app?
    // public static readonly string DestinationPath = String.Format("{0}/odwg-data.csv", Application.persistentDataPath);
    public static readonly string DestinationPath = "Assets/Data/odwg-data.csv";

    /// <summary>
    /// <para>Downloads data csv, then calls passed Method.</para>
    /// https://raw.githubusercontent.com/esklarski/canada.covid19/master/dataset/odwg-data.csv
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
