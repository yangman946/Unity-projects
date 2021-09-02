using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class hit : MonoBehaviour {

    public GameObject effect;
    public static bool isdead = false;
    public GameObject controller;
    public GameObject restart;
    public AudioSource blowup;

    void OnEnable()
    {
        controller.SetActive(true);
        isdead = false;
        restart.SetActive(false);
        homingmissles.hits = false;
    }
    // Update is called once per frame
    public void Update () {
		if (homingmissles.hits == true)
        {
            death();
        }

	}

    void death()
    {
        homingmissles.hits = false;
        blowup.Play();
        isdead = true;
        Destroy(gameObject);
        Instantiate(effect, transform.position, transform.rotation);
        controller.SetActive(false);
        restart.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "endzone")
        {
            blowup.Play();
            death();
        }
    }


}
