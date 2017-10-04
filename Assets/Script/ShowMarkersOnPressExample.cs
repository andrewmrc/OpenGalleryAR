using System;
using UnityEngine;

public class ShowMarkersOnPressExample : MonoBehaviour
{
    private OnlineMapsMarkerBase activeMarker;
    private OnlineMaps map;
    private GUIStyle tooltipStyle;

    private void Start()
    {
        map = OnlineMaps.instance;
        map.showMarkerTooltip = OnlineMapsShowMarkerTooltip.onPress;

        // Add an event to the markers created by the inspector
        foreach (OnlineMapsMarker marker in map.markers) marker.OnPress += OnMarkerPress;

        // Add an event to a new dynamic marker
        map.AddMarker(map.position, "Dynamic marker").OnPress += OnMarkerPress;

        // Subscribe to OnUpdateAfter
        OnlineMapsControlBase.instance.OnUpdateAfter += OnUpdateAfter;

        // Intercepts tooltip style.
        map.OnPrepareTooltipStyle += OnPrepareTooltipStyle;

        // Intercepts drawing tooltips.
        OnlineMapsMarkerBase.OnMarkerDrawTooltip += OnMarkerDrawTooltip;
    }

    private void OnPrepareTooltipStyle(ref GUIStyle style)
    {
        tooltipStyle = style;
    }

    private void OnMarkerDrawTooltip(OnlineMapsMarkerBase marker)
    {
        // Get screen position of marker
        Vector2 screenPosition = OnlineMapsControlBase.instance.GetScreenPosition(marker.position);

        // Calculate the size
        GUIContent tip = new GUIContent(marker.label);
        Vector2 size = tooltipStyle.CalcSize(tip);

        // Draw the tooltip
        GUI.Label(new Rect(screenPosition.x - size.x / 2 - 5, Screen.height - screenPosition.y - size.y - 20, size.x + 10, size.y + 5), marker.label, tooltipStyle);
    }

    /// <summary>
    /// This event occurs at end Update
    /// </summary>
    private void OnUpdateAfter()
    {
        // If activeMarker exists, restore tootip
        if (activeMarker != null)
        {
            map.tooltipMarker = activeMarker;
            map.tooltip = activeMarker.label;
        }
    }

    /// <summary>
    /// This event occurs on marker press
    /// </summary>
    /// <param name="marker">Instance of marker</param>
    private void OnMarkerPress(OnlineMapsMarkerBase marker)
    {
        // Change active marker
        activeMarker = marker;
    }
}