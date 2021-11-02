using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static StaticData;

/// <summary>
/// Class for creating 3d charts of covid-19 datas.
/// </summary>
public class Graph : MonoBehaviour
{
    [SerializeField]
    private GraphSettings settings;

    private bool dataReady = false;


    /// <summary>
    /// Selects the next color in the Color palette.
    /// </summary>
    /// <returns>A Color from the palette.</returns>
    private Color NextColor()
    {
        int temp = colorIndex;
        colorIndex++;
        if (colorIndex >= Colors.Length) { colorIndex = 0; }

        return Colors[temp];
    }
    // Track color palette location
    private int colorIndex = 0;


    /// <summary>
    /// Chart height for scaling data to fit within screen height.
    /// </summary>
    private float chartHeight;

    /// <summary>
    /// Scale to keep chart width constant as dataset grows
    /// </summary>
    private float horizontalModifier;

    /// <summary>
    /// Array for date lookup.
    /// </summary>
    private string[] days;

    /// <summary>
    /// Final storage of data for rendering use.
    /// </summary>
    private Dictionary<string, Province> provincial = new Dictionary<string, Province>(14);





    async void Awake()
    {
        // Randomly choose start position in color array
        // colorIndex = UnityEngine.Random.Range(0, Colors.Length);
        // TODO
        // find a Canadian Province color scheme

        // download current data
        await WebClient.DownloadData(delegate {DataReady();});
        // or skip
        // DataReady();
    }


    void Update()
    {
        
    }


    /// <summary>
    /// Called when data download is complete.
    /// </summary>
    public void DataReady()
    {
        List<Entry> entries = ReadData();

        CreateProvinceDictionary(entries);
        CreateProvincialNodes();

        // set data ready here?
        dataReady = true;

        // move into update, trigger with dataReady
        DrawPlots();
    }


    /// <summary>
    /// Reads csv file.
    /// </summary>
    /// <returns>List containing all entries.</returns>
    private List<Entry> ReadData()
    {
        List<Entry> entries = CsvParser.ParseCSV(WebClient.DestinationPath);
        days = entries.Select(x => x.dateString).Distinct().ToArray();

        return entries;
    }





    // ********************************** NEW PER DAY RENDER SYSTEM **********************************

    /// <summary>
    /// Starts coroutine to render plots.
    /// </summary>
    private void DrawPlots()
    {
        StartCoroutine(Render());
    }


    /// <summary>
    /// Grabs data points for the day, send them as List to CreatePoints(). Repeats for all days.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Render()
    {
        List<Entry> dailyEntries;

        SetChartDimensions();

        for (int i = 0; i < days.Count(); i++)
        {
            dailyEntries = (settings.excludeCanada)
                         ? provincial.Select(x => x.Value).Where(x => x.name != "Canada").SelectMany(y => y.points).Where(z => z.dateString == days[i]).ToList()
                         : provincial.Select(x => x.Value).SelectMany(y => y.points).Where(z => z.dateString == days[i]).ToList();

            // more?
            CreatePoints(dailyEntries, i);

            yield return new WaitForSeconds(settings.delay);
        }
    }


    /// <summary>
    /// Draws points for the given day.
    /// </summary>
    /// <param name="dayEntries">List of Entry for day.</param>
    /// <param name="dayIndex">X axis offset for day start position.</param>
    private void CreatePoints(List<Entry> dayEntries, int dayIndex)
    {
        foreach (Entry entry in dayEntries)
        {
            string dataType = settings.whichData.ToString();

            float maxValue = (chartHeight == -1)
                                ? (settings.normalizeByPopulation)
                                    ? provincial[entry.province].maxValuesNormalized[dataType]
                                    : (float)provincial[entry.province].maxValues[dataType]
                                : chartHeight;

            float verticalModifier = 335f / maxValue;

            Transform parent = provincial[entry.province].provincialNode.transform;
            Transform point = Instantiate(settings.pointPrefab);
            point.parent = parent;
            point.name = $"{entry.dateString}";
            point.GetComponent<MeshRenderer>().material.color = provincial[entry.province].color; //colorMap[entry.province];

            float xPosition = dayIndex * horizontalModifier;

            float position = (settings.normalizeByPopulation)
                ? (float)entry.values[dataType] / (float)provincial[entry.province].population * 100000f
                : entry.values[dataType];

            float yPosition = position * verticalModifier;

            int zPosition = provincial[entry.province].displayIndex;

            // if (!logarithmic)
            // {
            //     ?
            // }
            // else
            // {
                point.localPosition = new Vector3(xPosition, yPosition, zPosition);
            // }

            DataPoint entryDataPoint = point.GetComponent<DataPoint>();
            entryDataPoint.province = entry.province;
            entryDataPoint.value = position;
            entryDataPoint.date = entry.dateString;
        }
    }


    /// <summary>
    /// Create parent objects for plot points.
    /// </summary>
    private void CreateProvincialNodes()
    {
        foreach (KeyValuePair<string, Province> province in provincial)
        {
            GameObject newObject = new GameObject(province.Key);
            newObject.transform.parent = transform;
            if (settings.excludeCanada) { if (province.Key == "Canada") { newObject.SetActive(false); } }
            provincial[province.Key].provincialNode = newObject.transform;
        }
    }


    /// <summary>
    /// Processes data into Province objects within string indexed Dictionary.
    /// </summary>
    /// <param name="entries">List of all entries from csv parsing.</param>
    private void CreateProvinceDictionary(List<Entry> entries)
    {
        if (entries.Count() < 1) { Debug.Log("No data, aborting."); return; }

        int index = 0;

        foreach (KeyValuePair<string, int> province in Population)
        {
            string provinceName = province.Key;
            int provincePopulation = Population[province.Key];

            Entry[] provincialEntries =
                (Entry[])entries
                    .Where(x => x.province == province.Key.ToString())
                    .ToArray();

            Province nextProvince = new Province(
                provinceName,
                provincePopulation,
                provincialEntries
            );

            nextProvince.color = NextColor();
            nextProvince.displayIndex = index;

            provincial.Add(province.Key, nextProvince);
            index++;
        }
    }

    /// <summary>
    /// Find max chart data for scaling data.
    /// </summary>
    private void SetChartDimensions()
    {
        string dataType = settings.whichData.ToString();

        chartHeight = (settings.relativeCharting)
                    ? -1f
                    : (settings.normalizeByPopulation)
                        ? (settings.excludeCanada)
                            ? provincial.Select(x => x.Value).Where(y => y.name != "Canada").Max(x => x.maxValuesNormalized[dataType])
                            : provincial.Select(x => x.Value).Max(x => x.maxValuesNormalized[dataType])

                        : (settings.excludeCanada)
                            ? provincial.Select(x => x.Value).Where(y => y.name != "Canada").Max(x => x.maxValues[dataType])
                            : provincial.Select(x => x.Value).Max(x => x.maxValues[dataType]);

        Debug.Log($"chartHeight: {chartHeight}");

        // set height modifier
        horizontalModifier = 550f / days.Count();

        Debug.Log($"chartHeight: {horizontalModifier}");
    }
}
