using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shed : MonoBehaviour
{
    public GameObject barrier;
    public GameObject padlock;
    public Animator anim;
    public bool activate = false;
    public bool keyused = false;
    public bool open = false;


    // Start is called before the first frame update
    void Start()
    {
        activate = false;
        keyused = false;
        padlock.GetComponent<Rigidbody>().isKinematic = true; //rigid body shouldnt work
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate == true)
        {
            activate = false;
            padlock.GetComponent<Rigidbody>().isKinematic = false;
            keyused = true;
            open = true;
            
        }

        if (open == true)
        {
            opendoor();
        }

        if (open == false)
        {
            close();
        }
    }
    void opendoor()
    {
        barrier.SetActive(false);
        anim.SetBool("open", true);
    }
    void close()
    {
        barrier.SetActive(true);
        anim.SetBool("open", false);
    }
}
