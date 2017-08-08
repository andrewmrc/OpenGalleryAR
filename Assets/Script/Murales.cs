using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murales : MonoBehaviour {

    public string name;
    public string nameOfMurales;
    public string author;
    public string year;

	void Start ()
    {
        if (nameOfMurales == "")
            nameOfMurales = "-";
	}
	
	
	void Update () {
		
	}
}
