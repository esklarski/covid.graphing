using UnityEngine;
using static StaticData;

/// <summary>
/// Settings for graphing.
/// </summary>
[CreateAssetMenu()]
public class GraphSettings : ScriptableObject
{
    [Tooltip("Prefab to use for chart points.")]
    public Transform pointPrefab;

    [Tooltip("Delay between drawing points when animating plots. In seconds.")]
    public float delay = 0.05f;

    [Tooltip("What data to plot?")]
    public CaseType whichData = CaseType.active;

    [Tooltip("Scale plots to fill vertical height?")]
    public bool relativeCharting;

    [Tooltip("Exclude Canada plot from charts?")]
    public bool excludeCanada;

    [Tooltip("Normalize plots by population?")]
    public bool normalizeByPopulation;
}
