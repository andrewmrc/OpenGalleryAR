/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.UI;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        Canvas uiCanvas;

        void Awake()
        {
            uiCanvas = FindObjectOfType<Canvas>();
        }

        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            
            
            uiCanvas.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Theme : " + this.GetComponent<Murales>().name;
            uiCanvas.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
            
            uiCanvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = "Author : " + this.GetComponent<Murales>().author;
            uiCanvas.transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 1;

            uiCanvas.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "Year : " + this.GetComponent<Murales>().year;
            uiCanvas.transform.GetChild(2).GetComponent<CanvasGroup>().alpha = 1;

            uiCanvas.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "Name : " + this.GetComponent<Murales>().nameOfMurales;
            uiCanvas.transform.GetChild(3).GetComponent<CanvasGroup>().alpha = 1;
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

            uiCanvas.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "";
            uiCanvas.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;

            uiCanvas.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = "";
            uiCanvas.transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 0;

            uiCanvas.transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "";
            uiCanvas.transform.GetChild(2).GetComponent<CanvasGroup>().alpha = 0;

            uiCanvas.transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "";
            uiCanvas.transform.GetChild(3).GetComponent<CanvasGroup>().alpha = 0;
        }

        #endregion // PRIVATE_METHODS
    }
}
