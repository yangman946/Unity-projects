using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowbar : MonoBehaviour
{
    public Animator crowbaranim;
    public int damage = 5;
    public float range = 5f;
    public AudioSource hitsound;

    public Camera fpscam;

    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            crowbaranim.SetBool("hit", true);
            hit();
        }
        else
        {
            crowbaranim.SetBool("hit", false);
        }
    }

    void hit()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "wood")
            {
                hitsound.Play();

                child child = hit.transform.GetComponent<child>();

                if (child != null)
                {
                    child.transfer(damage);

                }
                
            }
        }
    }
}
