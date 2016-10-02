using UnityEngine;
using System.Collections;

public class LoopWalkScript : MonoBehaviour {

    private float left_x;
    private float right_x;

    public float width;
    public GameObject clone;


    Camera camera;

	// Use this for initialization
	void Start () {
        camera = Camera.main;
        left_x = camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        right_x = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, 0)).x;
    }

    // Update is called once per frame
    void Update () {
	    if(clone == null)
        {
            if(transform.position.x - width < left_x)
            {
                clone = (GameObject)Instantiate(this.gameObject, transform.position + Vector3.right * (right_x - left_x), transform.rotation);
                clone.GetComponent<LoopWalkScript>().SetClone(this.gameObject);
            }
            if (transform.position.x + width > right_x)
            {
                clone = (GameObject)Instantiate(this.gameObject, transform.position - Vector3.right * (right_x - left_x), transform.rotation);
                clone.GetComponent<LoopWalkScript>().SetClone(this.gameObject); 
            }
        }
        else
        {
            if (transform.position.x + width < left_x)
            {
                OnBecameInvisible();
            }
            if (transform.position.x - width > right_x)
            {
                OnBecameInvisible();
            }
        }

        //GhettoDestroy();
	}

    void OnBecameInvisible()
    {
        if (clone != null)
        {
            clone.GetComponent<LoopWalkScript>().SetClone(null);
            Destroy(this.gameObject);                                                                                         
        }
    }

    void GhettoDestroy()
    {
        GameObject[] otherplayers = GameObject.FindGameObjectsWithTag(this.gameObject.tag);
        foreach (GameObject g in otherplayers)
        {
            if (g != this.gameObject && (g.transform.position - transform.position).magnitude < 6)
            {
                Destroy(g);
            }
        }
    }
    public void SetClone(GameObject clone)
    {
        this.clone = clone;
    }

 

}
