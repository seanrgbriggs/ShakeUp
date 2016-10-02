using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    float speed = 2f;
    public int id;

    public GameObject proj;
    private int projectiles = 1;

    private float jump_cooldown = 0.00f;

    // Use this for initialization
    void Start () {
        this.gameObject.name = "Player " + id;
    }

    // Update is called once per frame
    void Update () {
        Move();
        Jump();
        Shoot();


        jump_cooldown -= Time.deltaTime;
    }

    void Move()
    {
        /*
        float axis_val = 0;

        KeyCode move_left = (id == 0) ? KeyCode.S : KeyCode.LeftArrow;
        KeyCode move_right = (id == 0) ? KeyCode.F : KeyCode.RightArrow;

        axis_val += (Input.GetKey(move_left)) ? -1 : 0;
        axis_val += (Input.GetKey(move_right)) ? 1 : 0;
        /*/

        float axis_val = (id == 0) ? Input.GetAxis("Horizontal1") : Input.GetAxis("Horizontal2");
        transform.position += Vector3.right * speed * axis_val;
        return;
    }

    void Jump()
    {
        if(jump_cooldown > 0)
        {
            return;
        }

        KeyCode jump_button = (id == 0) ? KeyCode.E : KeyCode.UpArrow;

        bool will_jump = (id == 0) ? Input.GetButtonDown("Jump1") : Input.GetButtonDown("Jump2");
        if (will_jump)
        {
            jump_cooldown = 0.2f;
            GetComponent<Rigidbody2D>().gravityScale *= -1;
        }
    }

    void Shoot()
    {
        
        if (projectiles <= 0)
        {
            return;
        }

        bool shoot_left = (id == 0) ? Input.GetButtonDown("FireLeft1") : Input.GetButtonDown("FireLeft2");
        bool shoot_right = (id == 0) ? Input.GetButtonDown("FireRight1") : Input.GetButtonDown("FireRight2");

        float width = GetComponent<LoopWalkScript>().width;

        GameObject projectile = null;
        Vector3 spawn_pos = transform.position + (width * getGravitySign() * Vector3.up);
        Quaternion spawn_rot = Quaternion.identity;

        if (shoot_right) {

            spawn_pos += width * Vector3.right;
            projectile = (GameObject) Instantiate(proj, spawn_pos, spawn_rot);
            projectile.transform.up = Vector3.right + getGravitySign() * Vector3.up;
            
        }else if (shoot_left)
        {
            spawn_pos += width * Vector3.left;
            projectile = (GameObject)Instantiate(proj, spawn_pos, spawn_rot);
            projectile.transform.up = Vector3.left + getGravitySign() * Vector3.up;

        }

        if (projectile != null)
        {
            projectile.GetComponent<ProjectileScript>().SetID(id);
            projectiles--;
        }
    }

    private int getGravitySign()
    {
        float input = GetComponent<Rigidbody2D>().gravityScale;
        return (int) (input / Mathf.Abs(input));
    }

    public void projSignal(int id)
    {
        if(this.id == id)
        {
            projectiles++;
        }
    }
}
