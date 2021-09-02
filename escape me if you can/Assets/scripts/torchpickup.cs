using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * pick up torch edition
 * or for other tools such as crowbar
 */
public class torchpickup : MonoBehaviour
{
    public string whatIsThis;
    public bool intag = false;
    bool trigger = false;
    bool trigger1 = false;
    bool trigger_a = false;
    bool trigger1_a = false;


    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        intag = false;
        trigger = false;
        trigger1 = false;

        player = GameObject.FindGameObjectWithTag("Player");
        

        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && intag == true && whatIsThis == "torch" && player.GetComponent<inventory>().toolindex == 0)//if in users range and the collided object is a torch
        {
            trigger = true;
            Debug.Log("pressed");
        }
        else if (Input.GetKey(KeyCode.E) && intag == true && whatIsThis == "torch" && player.GetComponent<inventory>().toolindex == 2)//if torch is collected but crowbar is currently active
        {
            trigger_a = true;
        }
        else if (Input.GetKey(KeyCode.E) && intag == true && player.GetComponent<inventory>().usekey == true && whatIsThis == "crowbar" && player.GetComponent<inventory>().toolindex == 0)
        {
            trigger1 = true;

        }
        else if (Input.GetKey(KeyCode.E) && intag == true && whatIsThis == "crowbar" && player.GetComponent<inventory>().toolindex == 1)//if crowbar is collected but torch is currently active
        {
            trigger1_a = true;
        }


        if (trigger == true && intag == true)
        {
            player.GetComponent<inventory>().picked= true;
            trigger = false;
            player.GetComponent<inventory>().toolindex = 1; //torch
            player.GetComponent<inventory>().torchItem = true;

            //else if (whatIsThis == "key") { player.GetComponent<inventory>().addBattery = true; }

            player.GetComponent<itemPickup>().collided = false;
            player.GetComponent<inventory>().torchreal.SetActive(true);
            
            delete(); //destroy gameobject

        }
        else if (trigger_a == true && intag == true)
        {
            trigger_a = false;
            player.GetComponent<itemPickup>().collided = false;
            player.GetComponent<inventory>().torchItem = true;
            delete();
        }

        if (trigger1 == true && intag == true) //crowbar
        {
            player.GetComponent<inventory>().crowbaritem = true;
            trigger1 = false;
            player.GetComponent<inventory>().toolindex = 2; //crowbar
            player.GetComponent<inventory>().crowbarreal.SetActive(true);
            player.GetComponent<itemPickup>().collided = false;
            delete();
        }
        else if (trigger1_a == true && intag == true)
        {
            trigger1_a = false;
            player.GetComponent<itemPickup>().collided = false;
            player.GetComponent<inventory>().crowbaritem = true;
            delete();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            intag = true;
            //Debug.Log("collidedd");
            collision.gameObject.GetComponent<itemPickup>().itemtag = whatIsThis;
            collision.gameObject.GetComponent<itemPickup>().collided = true;



        }

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            intag = false;
            collision.gameObject.GetComponent<itemPickup>().collided = false;

        }
    }

    void delete()
    {
        Destroy(transform.parent.gameObject); //destroy this parent which destroys this
    }
}
