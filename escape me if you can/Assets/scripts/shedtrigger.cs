using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shedtrigger : MonoBehaviour
{
    public Text infoline;
    public Text disabletext;
    GameObject player;
    public GameObject shed;
    Image[] boxinvent;
    public bool havekey;

    public GameObject shedcheck;

    public bool trigger = false;


    bool dooropen;
    bool doorclose;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boxinvent = player.GetComponent<inventory>().boxes;
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<inventory>().shouldshedtextshow == true)
        {
            infoline.text = "";
        }

        if (havekey == true && player.GetComponent<inventory>().usekey == true)
        {
            //we have a key and the key is selected by the user to be used
            havekey = false;
            shed.GetComponent<shed>().activate = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && dooropen == true)
        {
            //openshed
            shed.GetComponent<shed>().open = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && doorclose == true && shedcheck.GetComponent<shedcheck>().dontclose == false)
        {
            //closeshed
            shed.GetComponent<shed>().open = false;
        }

        if (trigger == true && player.GetComponent<inventory>().shouldshedtextshow == false)
        {
            if (shed.GetComponent<shed>().keyused == true && shed.GetComponent<shed>().open == false )
            {
                infoline.text = "Press 'E' to open shed.";
                dooropen = true;
                doorclose = false;

            }
            else if (shed.GetComponent<shed>().keyused == true && shed.GetComponent<shed>().open == true && shedcheck.GetComponent<shedcheck>().dontclose == false)
            {
                infoline.text = "Press 'E' to close shed.";
                doorclose = true;
                dooropen = false;

            }
            else if (shed.GetComponent<shed>().keyused == false && shedcheck.GetComponent<shedcheck>().dontclose == false) //
            {
                infoline.text = "The shed is locked";
                //disabletext.text = "";
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            trigger = true;
            for (int i = 0; i < player.GetComponent<inventory>().length; i++)
            {
                if (boxinvent[i].GetComponent<slot>().slotKey == true)
                {
                    Debug.Log("have key");
                    
                    havekey = true;
                }

            }


            

        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            infoline.text = "";
            havekey = false;
            trigger = false;
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(5);

        infoline.text = "";
    }


}
