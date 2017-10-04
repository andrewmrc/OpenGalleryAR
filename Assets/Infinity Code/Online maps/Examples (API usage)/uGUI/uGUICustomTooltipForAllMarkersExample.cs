/*     INFINITY CODE 2013-2017      */
/*   http://www.infinity-code.com   */

using UnityEngine;
using UnityEngine.UI;

namespace InfinityCode.OnlineMapsExamples
{
    /// <summary>
    /// Example of how to make a tooltip using uGUI for all markers
    /// </summary>
    [AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/uGUICustomTooltipForAllMarkersExample")]
    public class uGUICustomTooltipForAllMarkersExample : MonoBehaviour
    {
        /// <summary>
        /// Prefab of the tooltip
        /// </summary>
        public GameObject tooltipPrefab;

        /// <summary>
        /// Container for tooltip
        /// </summary>
        public Canvas container;

        private GameObject tooltip;
        public CopyInTheDataContainer tooltipX;
        OnlineMapsMarker tooltipMarker;

        private void Start()
        {
            //OnlineMaps.instance.AddMarker(Vector2.zero, "Marker 1");
            //OnlineMaps.instance.AddMarker(new Vector2(1, 1), "Marker 2");
            //OnlineMaps.instance.AddMarker(new Vector2(2, 1), "Marker 3");
            OnlineMapsMarkerBase.OnMarkerDrawTooltip = delegate { };

            OnlineMaps.instance.OnUpdateLate += OnUpdateLate;

        }

        private void OnUpdateLate()
        {
            tooltipMarker = OnlineMaps.instance.tooltipMarker as OnlineMapsMarker;
           
            if (tooltipMarker != null)
            {
                if (tooltip == null)
                {
                    tooltip = Instantiate(tooltipPrefab) as GameObject;
                    (tooltip.transform as RectTransform).SetParent(container.transform);
                }
                Vector2 screenPosition = OnlineMapsControlBase.instance.GetScreenPosition(tooltipMarker.position);
                screenPosition.y += tooltipMarker.height;
                Vector2 point;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(container.transform as RectTransform, screenPosition, null, out point);
                (tooltip.transform as RectTransform).localPosition = point;
                tooltip.GetComponentInChildren<Text>().text = "Show Direction";
                tooltip.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Murales_Preview/" + tooltipMarker.label);
                tooltip.GetComponent<CopyInTheDataContainer>().coordLat = tooltipMarker.latitude.ToString();
                tooltip.GetComponent<CopyInTheDataContainer>().coordLong = tooltipMarker.longitude.ToString();

                // tooltip.transform.GetComponent<ObjectPosition>().lon_d = 

            }
            else
            {
                OnlineMapsUtils.DestroyImmediate(tooltip);
                tooltip = null;
            }
        }

        public void DestroyTooltip()
        {
            Debug.LogWarning("CHIAMATO");
            //tooltipX = FindObjectOfType<CopyInTheDataContainer>();
            foreach (OnlineMapsMarker marker in OnlineMaps.instance.markers)
            {
                OnlineMaps.instance.RemoveAllDrawingElements();
            }
                
            //OnlineMapsUtils.DestroyImmediate(tooltipX);
            //tooltipX.gameObject.SetActive(false);

        }
    }
}