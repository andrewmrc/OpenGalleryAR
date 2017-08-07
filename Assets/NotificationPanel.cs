using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPanel : MonoBehaviour {

	
	public void Show()
    {
        this.GetComponent<CanvasGroup>().alpha = 1;
    }
	
	
	public void Hide ()
    {
        this.GetComponent<CanvasGroup>().alpha = 0;
    }
}
