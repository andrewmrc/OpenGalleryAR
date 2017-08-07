using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour {

    public GameObject cameraVuforia;
    public GameObject cameraGoogle;
    Camera cVuforia;
    Camera cGoogle;
    public Button buttonSwitch;
    
    bool google = true;

    private void Start()
    {
        cVuforia = cameraGoogle.GetComponent<Camera>();
        cGoogle = cameraVuforia.GetComponent<Camera>();
        
    }

    public void EnableGoogle()
    {
        if (google == false)
        {
            cVuforia.enabled = false;
            cGoogle.enabled = true;
            Image img = buttonSwitch.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("map"); 
            

            google = true;
        }
        else if (google == true)
        {
            cVuforia.enabled = true;
            cGoogle.enabled = false;
            Image img = buttonSwitch.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("murales");

            google = false;
        }
		
	}
	
	
	
}
