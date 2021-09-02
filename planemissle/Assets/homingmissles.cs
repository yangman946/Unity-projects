using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class homingmissles : MonoBehaviour {

    private GameObject target;

    private Rigidbody2D rb;

    public float speed = 5f;
    public float rotatespeed = 200f;

    public GameObject Explosion;

    public static bool hits = false;
    private GameObject explosion;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>(); 
        hits = false;
        target = GameObject.Find("plane");
        explosion = GameObject.Find("explod");

	}



    // Update is called once per frame
    void FixedUpdate () {
        
        if (hit.isdead == true)
        {
            return;
        }

        

        Vector2 direction = (Vector2)target.transform.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotatespeed;


        rb.velocity = transform.up * speed;

        
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "explodable")
        {
            dead();
        }
        else if (col.gameObject.tag == "Player")
        {
            hits = true;
            dead();
        }
        else if (col.gameObject.tag == "endzone")
        {
            dead();
        }
    }

    void dead()
    {
        explosion.GetComponent<AudioSource>().Play();
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
