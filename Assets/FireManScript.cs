using UnityEngine;
using System.Collections;

public class FireManScript : MonoBehaviour {

    Transform[] extremeties;
    public GameObject parti;

	// Use this for initialization
	void Start () {
        extremeties = GetComponentsInChildren<Transform>();
        Debug.Log(extremeties.Length);
        for (int i = 0; i < extremeties.Length; i++)
        {
            GameObject fire =  (GameObject) Instantiate(parti, extremeties[i]);
            fire.transform.position = fire.transform.parent.position;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
