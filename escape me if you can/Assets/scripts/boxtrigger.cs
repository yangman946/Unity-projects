using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boxtrigger : MonoBehaviour
{
    public Text infoline; //use as the same as shed trigger
    public bool opentrigger = false;
    public GameObject player;
    public bool havekey = false;
    public bool keyused = false;
    bool open = false;

    public bool nomorekey = false;
    public Animator anim;

    public bool inkeyrange = false;
    public GameObject shedtrigger;

    Image[] boxinvent;
    // Start is called before the first frame update
    void Start()
    {
        opentrigger = false;
        player = GameObject.FindGameObjectWithTag("Player");
        boxinvent = player.GetComponent<inventory>().boxes;

        havekey = false;
        open = false;
        keyused = false;
        nomorekey = false;

        inkeyrange = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (player.GetComponent<inventory>().shouldshedtextshow == true)
        {
            infoline.text = "";
        }

        if (havekey == true && player.GetComponent<inventory>().useboxkey == true)
        {
            //we have a key and the key is selected by the user to be used
            havekey = false;
            activatebox();
        }



        if (opentrigger == true && player.GetComponent<inventory>().shouldshedtextshow == false && player.GetComponent<itemPickup>().triggered == false)
        {
            if (keyused == false) //
            {
                infoline.text = "The box is locked";
                //disabletext.text = "";
            }
        }
        else if (player.GetComponent<itemPickup>().triggered == true)
        {
            infoline.text = "";
        }
            
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            opentrigger = true;
            for (int i = 0; i < player.GetComponent<inventory>().length; i++)
            {
                if (boxinvent[i].GetComponent<slot>().slotboxKey == true)
                {
                    Debug.Log("have box key");

                    havekey = true;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            infoline.text = "";
            havekey = false;
            opentrigger = false;
        }
    }

    void activatebox()
    {
        inkeyrange = true;
        keyused = true;
        open = true;
        openbox();
    }

    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(5);

        infoline.text = "";
    }

    void openbox()
    {
        anim.SetBool("open", true);
    }

    void unopenbox()
    {
        anim.SetBool("open", false);
    }
}
