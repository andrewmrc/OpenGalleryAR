using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyInTheDataContainer : MonoBehaviour {

    public string coordLong;
    public string coordLat;

    public void Copy()
    {

        //Application.OpenURL("http://maps.google.com/maps/dir/?api=1&destination="+ coordLat + "," + coordLong);
        Application.OpenURL("http://maps.google.com/maps?daddr=" + coordLat + "," + coordLong);

    }
}
