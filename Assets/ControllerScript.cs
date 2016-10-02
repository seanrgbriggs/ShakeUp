using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ControllerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            SceneManager.LoadScene("Stage");
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            SceneManager.LoadScene("Menu");
        }
	}
}
