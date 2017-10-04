using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerActivator : OnlineMapsMarkerBase{

    public GameObject target;
	
	void Start () {
		
	}
	
	
	override public void Update () {

        
            if (inMapView)
        {
            OnlineMaps.instance.ShowMarkersTooltip(position);
        }

    }

    
}
