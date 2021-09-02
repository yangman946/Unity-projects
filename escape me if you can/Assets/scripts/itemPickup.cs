using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemPickup : MonoBehaviour
{
    //add these tags to the objects :

    public string itemtag = "";
    public bool collided = false;
    public bool triggered = false;

    public Text infoline;

    public GameObject shedtrigger;
    public GameObject boxtrigger;
    public GameObject exittrigger;
  
    inventory invent;


    string collect = "Press 'E' to collect ";

    public bool full = false;

    // Start is called before the first frame update
    void Start()
    {
        infoline.text = "";
        itemtag = "";
        collided = false;
        triggered = false;
        full = false;
        invent = gameObject.GetComponent<inventory>();

    }

    // Update is called once per frame
    void Update()
    {
        if (shedtrigger.GetComponent<shedtrigger>().trigger == true)
        {
            infoline.text = "";
        }



        if (collided == true && invent.trigger == false && shedtrigger.GetComponent<shedtrigger>().trigger == false && exittrigger.GetComponent<exittrigger>().trigger == false) //this makes sure that both texts dont display
        {
            triggered = true;
            infoline.text = collect + itemtag + "."; // simple sentence for instruction

            
        }
        else if (full == true)
        {
            infoline.text = "Inventory is full";
        }
        else if (collided == false || invent.trigger == true )
        {
            infoline.text = ""; //fix this
            triggered = false;
        }


        


        
    }





}
