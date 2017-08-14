using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager> {

	[HideInInspector]
	public bool locationServicesIsRunning = false;

	public GameObject mainMap;
	public GameObject newMap;

	public GameObject player;
	public GeoPoint playerGeoPosition;
	public PlayerLocationService player_loc;

    public List<GameObject> muralesButtonFull = new List<GameObject>();
    public List<GameObject> muralesButtonFound = new List<GameObject>();

    public GameObject artistInputField;
    public GameObject cityInputField;

    public GameObject retryButton;

    public GameObject menuPanel;

    bool somethingFound;

    string coordLong;
    string coordLat;

    public enum PlayerStatus { TiedToDevice, FreeFromDevice }

	private PlayerStatus _playerStatus;
	public PlayerStatus playerStatus
	{
		get { return _playerStatus; }
		set { _playerStatus = value; }
	}

	void Awake (){

		Time.timeScale = 1;
		playerStatus = PlayerStatus.TiedToDevice;

		player_loc = player.GetComponent<PlayerLocationService>();
		newMap.GetComponent<MeshRenderer>().enabled = false;
		newMap.SetActive (false);

	}

	public GoogleStaticMap getMainMapMap () {
		return mainMap.GetComponent<GoogleStaticMap> ();
	}

	public GoogleStaticMap getNewMapMap () {
		return newMap.GetComponent<GoogleStaticMap> ();
	}

	IEnumerator Start () {

		getMainMapMap ().initialize ();
		yield return StartCoroutine (player_loc._StartLocationService ());
		StartCoroutine (player_loc.RunLocationService ());

		locationServicesIsRunning = player_loc.locServiceIsRunning;
		Debug.Log ("Player loc from GameManager: " + player_loc.loc);
		getMainMapMap ().centerMercator = getMainMapMap ().tileCenterMercator (player_loc.loc);
		getMainMapMap ().DrawMap ();

		mainMap.transform.localScale = Vector3.Scale (
			new Vector3 (getMainMapMap ().mapRectangle.getWidthMeters (), getMainMapMap ().mapRectangle.getHeightMeters (), 1.0f),
			new Vector3 (getMainMapMap ().realWorldtoUnityWorldScale.x, getMainMapMap ().realWorldtoUnityWorldScale.y, 1.0f));

		player.GetComponent<ObjectPosition> ().setPositionOnMap (player_loc.loc);

		GameObject[] objectsOnMap = GameObject.FindGameObjectsWithTag ("ObjectOnMap");

		foreach (GameObject obj in objectsOnMap) {
			obj.GetComponent<ObjectPosition> ().setPositionOnMap ();
		}
    }

    void Update () {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!menuPanel.activeInHierarchy)
            {
                BackButton();
            }
            else
            {
                Application.Quit();
            }
        }


        if (!locationServicesIsRunning){

			//TODO: Show location service is not enabled error. 
			return;
		}

		// playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		playerGeoPosition = new GeoPoint();
		// GeoPoint playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		if (playerStatus == PlayerStatus.TiedToDevice) {
			playerGeoPosition = player_loc.loc;
			player.GetComponent<ObjectPosition> ().setPositionOnMap (playerGeoPosition);
		} else if (playerStatus == PlayerStatus.FreeFromDevice){
			playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		}


		var tileCenterMercator = getMainMapMap ().tileCenterMercator (playerGeoPosition);
		if(!getMainMapMap ().centerMercator.isEqual(tileCenterMercator)) {

			newMap.SetActive(true);
			getNewMapMap ().initialize ();
			getNewMapMap ().centerMercator = tileCenterMercator;

			getNewMapMap ().DrawMap ();

			getNewMapMap ().transform.localScale = Vector3.Scale(
				new Vector3 (getNewMapMap ().mapRectangle.getWidthMeters (), getNewMapMap ().mapRectangle.getHeightMeters (), 1.0f),
				new Vector3(getNewMapMap ().realWorldtoUnityWorldScale.x, getNewMapMap ().realWorldtoUnityWorldScale.y, 1.0f));	

			Vector2 tempPosition = GameManager.Instance.getMainMapMap ().getPositionOnMap (getNewMapMap ().centerLatLon);
			newMap.transform.position = new Vector3 (tempPosition.x, 0, tempPosition.y);

			GameObject temp = newMap;
			newMap = mainMap;
			mainMap = temp;

		}
		if(getMainMapMap().isDrawn && mainMap.GetComponent<MeshRenderer>().enabled == false){
			mainMap.GetComponent<MeshRenderer>().enabled = true;
			newMap.GetComponent<MeshRenderer>().enabled = false;
			newMap.SetActive(false);
		}
	}

	public Vector3? ScreenPointToMapPosition(Vector2 point){
		var ray = Camera.main.ScreenPointToRay(point);
		//RaycastHit hit;
		// create a plane at 0,0,0 whose normal points to +Y:
		Plane hPlane = new Plane(Vector3.up, Vector3.zero);
		float distance = 0; 
		if (!hPlane.Raycast (ray, out distance)) {
			// get the hit point:
			return null;
		}
		Vector3 location = ray.GetPoint (distance);
		return location;
	}


    public void BackButton()
    {
        menuPanel.SetActive(true);
    }


        public void SearchByArtist ()
    {
        retryButton.SetActive(false);
        string nameTypedIn = artistInputField.gameObject.GetComponent<InputField>().text;
        cityInputField.gameObject.GetComponent<InputField>().text = "";
        somethingFound = false;

        for (int i = 0; i < muralesButtonFull.Count; i++)
        {
            string thisArtistName = muralesButtonFull[i].GetComponent<Murales>().author.ToLower();
            Debug.Log(nameTypedIn.ToString());
            if (thisArtistName.Contains(nameTypedIn.ToString().ToLower()))
            {
                muralesButtonFull[i].SetActive(true);
                somethingFound = true;
            }
            else
            {
                muralesButtonFull[i].SetActive(false);
            }
        }

        if (!somethingFound)
        {
            retryButton.SetActive(true);
        }
    }


    public void SearchByCity()
    {
        retryButton.SetActive(false);
        string cityTypedIn = cityInputField.gameObject.GetComponent<InputField>().text;
        artistInputField.gameObject.GetComponent<InputField>().text = "";
        somethingFound = false;

        for (int i = 0; i < muralesButtonFull.Count; i++)
        {
            string thisMuralCity = muralesButtonFull[i].GetComponent<Murales>().city.ToLower();
            Debug.Log(cityTypedIn.ToString());
            if (thisMuralCity.Contains(cityTypedIn.ToString().ToLower()))
            {
                muralesButtonFull[i].SetActive(true);
                somethingFound = true;
            }
            else
            {
                muralesButtonFull[i].SetActive(false);
            }
        }


        if (!somethingFound)
        {
            retryButton.SetActive(true);
        }

    }


    public void SearchDelete()
    {
        artistInputField.gameObject.GetComponent<InputField>().text = "";
        cityInputField.gameObject.GetComponent<InputField>().text = "";
        retryButton.SetActive(false);
        somethingFound = false;
        for (int i = 0; i < muralesButtonFull.Count; i++)
        {
            muralesButtonFull[i].SetActive(true);
        }
    }


    public void OpenGoogleMaps ()
    {
        coordLong = EventSystem.current.currentSelectedGameObject.GetComponent<Murales>().lon_d.ToString();
        coordLat = EventSystem.current.currentSelectedGameObject.GetComponent<Murales>().lat_d.ToString();
        Application.OpenURL("http://maps.google.com/maps/dir/?api=1&destination="+ coordLat + "," + coordLong);
        //StartPackage("com.google.android.apps.maps");
    }


    void StartPackage(string package)
    {
        AndroidJavaClass unityClass;
        AndroidJavaObject unityObject, packageManager;
        AndroidJavaObject launch;
        unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        unityObject = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        packageManager = unityObject.Call<AndroidJavaObject>("getPackageManager");
        launch = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", package);
        unityObject.Call("startActivity", launch);
    }

}
