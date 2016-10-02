using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    GameObject tweet;

	// Use this for initialization
	void Start () {
        tweet = GameObject.FindGameObjectWithTag("Finish");
        tweet.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<AudioSource>().time = Mathf.Max(0.1f, GetComponent<AudioSource>().time);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            SceneManager.LoadScene("Stage");
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {
            GameObject.FindWithTag("Respawn").SetActive(false);
            tweet.SetActive(true);
        }
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("main");
    }

    public void OnGame()
    {
        SceneManager.LoadScene("Stage");
    }

    public void OnTweet()
    {
        SceneManager.LoadScene("Stage");
    }
}
