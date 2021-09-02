using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    Image inventobj;
    public int indexID;

    public bool removesprite = false;
    public GameObject player;
    Sprite torch;
    Sprite battery;
    Sprite Key;
    Sprite crowbar;
    Sprite boxkey;
    Sprite exitkey;
    Sprite map;

    //objects
    public bool slotTorch = false; //set in inventory script
    public bool slotbattery = false;
    public bool slotKey = false;
    public bool slotcrowbar = false;
    public bool slotboxKey = false;
    public bool slotexitkey = false;
    public bool slotmap = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        inventobj = gameObject.GetComponent<Image>(); //get the image component off this object
        torch = player.GetComponent<inventory>().torchs; //get the torch sprite off the inventory script
        battery = player.GetComponent<inventory>().battery;
        Key = player.GetComponent<inventory>().Key;
        crowbar = player.GetComponent<inventory>().crowbarpic;
        boxkey = player.GetComponent<inventory>().boxkey;
        exitkey = player.GetComponent<inventory>().exitkey;
        map = player.GetComponent<inventory>().map;
        slotTorch = false;
        slotbattery = false;
        slotKey = false;
        slotboxKey = false;
        slotcrowbar = false;
        slotexitkey = false;
        slotmap = false;
        removesprite = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (slotTorch == true) //if the player possesses a torch and this slot is free
        {
            
            inventobj.sprite = torch; //set the slot image to a torch
            
        }
        else if (slotbattery == true)
        {
            inventobj.sprite = battery;
            
        }
        else if (slotKey == true)
        {
            inventobj.sprite = Key;

        }
        else if (slotboxKey == true)
        {
            inventobj.sprite = boxkey;
        }
        else if (slotcrowbar == true)
        {
            inventobj.sprite = crowbar;
        }
        else if (slotexitkey == true)
        {
            inventobj.sprite = exitkey;
        }
        else if (slotmap == true)
        {
            inventobj.sprite = map;
        }
        if (removesprite == true)
        {
            inventobj.sprite = null;
            slotTorch = false;
            slotKey = false;
            slotboxKey = false;
            slotbattery = false;
            slotcrowbar = false;
            slotexitkey = false;
            slotmap = false;
            removesprite = false;
        }
    }
}
