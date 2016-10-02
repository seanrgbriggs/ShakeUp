using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {

    float step = 0;
    float maxstep = 1;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        step += Time.deltaTime;

        if (step > maxstep)
        {
            step = 0;

            Vector3 col_vec = new Vector3(Random.value, Random.value, Random.value);
            col_vec *= 255;
            GetComponent<Light>().color = new Color(col_vec.x, col_vec.y, col_vec.z);
        }
    }
}
