using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleSpecial : MonoBehaviour {

    DataContainer dContainer;
    string coordLong;
    string coordLat;

    private void Start()
    {
        
        dContainer = FindObjectOfType<DataContainer>();
    }

    public void OpenGoogleMapsTooltip()
    {
        coordLong = dContainer.lon_d.ToString();
        coordLat = dContainer.lat_d.ToString();
        //Application.OpenURL("http://maps.google.com/maps/dir/?api=1&destination="+ coordLat + "," + coordLong);
        Application.OpenURL("http://maps.google.com/maps?daddr=" + coordLat + "," + coordLong);
    }
}
