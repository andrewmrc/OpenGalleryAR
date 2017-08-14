using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murales : MonoBehaviour {

    public string name;
    public string nameOfMurales;
    public string author;
    public string year;
    public string city;

    public float lat_d = 0.0f, lon_d = 0.0f;

    void Start ()
    {
        if (nameOfMurales == "")
            nameOfMurales = "-";
	}

}
