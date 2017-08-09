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
    
    public bool activateMap = true;

    private void Start()
    {
        cVuforia = cameraGoogle.GetComponent<Camera>();
        cGoogle = cameraVuforia.GetComponent<Camera>();
        EnableGoogle();
    }

    public void EnableGoogle()
    {
        if (activateMap == false)
        {
            cVuforia.enabled = false;
            cGoogle.enabled = true;
            Image img = buttonSwitch.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("map"); 
            

            activateMap = true;
        }
        else if (activateMap == true)
        {
            cVuforia.enabled = true;
            cGoogle.enabled = false;
            Image img = buttonSwitch.GetComponent<Image>();
            img.sprite = Resources.Load<Sprite>("murales");

            activateMap = false;
        }
		
	}
	
	
	public void ActivateMode(bool mode)
    {
        activateMap = mode;
        EnableGoogle();
    }

}
