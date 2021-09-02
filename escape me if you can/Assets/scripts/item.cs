using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public string whatIsThis;
    public bool intag = false;
    bool trigger = false;
    public bool keycheck = false;
    public GameObject boxtrigger;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        intag = false;
        trigger = false;
        keycheck = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && intag == true && whatIsThis != "Exit key")
        {
            trigger = true;
            Debug.Log("pressed");

        }
        else if (Input.GetKey(KeyCode.E) && intag == true && whatIsThis == "Exit key" && boxtrigger.GetComponent<boxtrigger>().keyused == true) //stops you from collecting it if you havent unlocked box yet
        {
            trigger = true;
            //keycheck = true;
        }

        if (trigger == true && intag == true)
        {

            trigger = false;
            //keycheck = false;
            if (whatIsThis == "battery") { player.GetComponent<inventory>().addBattery = true; }
            else if (whatIsThis == "key") { player.GetComponent<inventory>().addKey = true; }
            else if (whatIsThis == "box key") { player.GetComponent<inventory>().addboxkey = true; }
            else if (whatIsThis == "Exit key") { player.GetComponent<inventory>().addexitkey = true; }
            else if (whatIsThis == "map") { player.GetComponent<inventory>().addmap = true; }

            player.GetComponent<itemPickup>().collided = false;
            delete(); //destroy gameobject

        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player" && player.GetComponent<inventory>().full == false && whatIsThis != "Exit key")
        {
            intag = true;
            //Debug.Log("collidedd");
            collision.gameObject.GetComponent<itemPickup>().itemtag = whatIsThis;
            collision.gameObject.GetComponent<itemPickup>().collided = true;
            


        }
        else if (collision.tag == "Player" && player.GetComponent<inventory>().full == true && whatIsThis != "Exit key")
        {
            Debug.Log("full inventory ");
            collision.gameObject.GetComponent<itemPickup>().full = true;

        }
        else if (collision.tag == "Player" && player.GetComponent<inventory>().full == false && whatIsThis == "Exit key" &&  boxtrigger.GetComponent<boxtrigger>().keyused == true)
        {
            intag = true;
            Debug.Log("collidedd");
            collision.gameObject.GetComponent<itemPickup>().itemtag = whatIsThis;
            collision.gameObject.GetComponent<itemPickup>().collided = true;
        }
        else if (collision.tag == "Player" && player.GetComponent<inventory>().full == true && whatIsThis == "Exit key" && boxtrigger.GetComponent<boxtrigger>().keyused == true)
        {
            Debug.Log("full inventory ");
            collision.gameObject.GetComponent<itemPickup>().full = true;

        }

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            intag = false;
            collision.gameObject.GetComponent<itemPickup>().collided = false;
            collision.gameObject.GetComponent<itemPickup>().full = false;
        }
    }

    void delete()
    {
        Destroy(transform.parent.gameObject); //destroy this parent which destroys this
    }
}
