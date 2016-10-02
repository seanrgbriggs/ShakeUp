using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public Material[] mats;
    public int id;

    int collisions;
    public int max_collisions = 3;

    Vector3 direction;
    public GameObject boom;
    public AudioClip bam;

	// Use this for initialization
	void Start () {
        collisions++;

        direction = transform.up;

    }
	
    public void SetID(int id)
    {
        this.id = id;
        GetComponent<Renderer>().material = mats[id];
    }

	// Update is called once per frame
	void Update () {
        Vector3 dir_x = (direction.x < 0) ? Vector3.left : Vector3.right;
        Vector3 dir_y = (direction.y < 0) ? Vector3.down : Vector3.up;

        direction = (dir_x + dir_y).normalized;

        GetComponent<Rigidbody2D>().velocity =  15f * direction;

        if (collisions > max_collisions)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                player.GetComponent<PlayerScript>().projSignal(id);
            }
            Destroy(this.gameObject);
        }

        
        float angle = transform.rotation.eulerAngles.z;
        if (angle % 45 != 0)
        {
            float target_angle = 45 * Mathf.RoundToInt(angle / 45f);

            if(target_angle % 90 == 0)
            {
                if(angle < target_angle)
                {
                    target_angle -= 45;
                }else
                {
                    target_angle += 45;
                }
            }
            transform.rotation = Quaternion.Euler(0, 0, target_angle);
        }

        


	}

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerScript>().id != id)
        {
            DestroyAllProjectiles();
            Destroy(col.gameObject);
            Destroy(this.gameObject);
            GameObject.FindGameObjectWithTag("Finish").GetComponent<Text>().text = "WINNER\nPress Space/Start To Repeat";

        }

        collisions++;
        Vector3 normal = col.contacts[0].normal;
        for(int i=1; i < col.contacts.Length; i++)
        {
            normal += (Vector3) col.contacts[i].normal;
        }
        normal /= col.contacts.Length;
 
        direction = Vector3.Reflect(direction, normal);
        transform.up = direction;

        GameObject boomb = ((GameObject)Instantiate(boom));
        boomb.transform.position = transform.position;
        boomb.transform.localScale /= 500;

        GetComponent<AudioSource>().Play();

    }

    void OnBecameInvisible()
    {
        if (!this.isActiveAndEnabled)
            return;
        Camera camera = Camera.main;
        float left_x = camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float right_x = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, 0)).x;

        if (transform.position.x > right_x)
        {
            transform.position += Vector3.left * (1 + right_x - left_x);
        }
        else
        {
            transform.position += Vector3.right * (1 + right_x - left_x);
        }
    }

    void DestroyAllProjectiles()
    {
        GameObject[] projs = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject g in projs)
        {
            if(g != this.gameObject)
            {
                Destroy(g);
            }
        }
    }
}
