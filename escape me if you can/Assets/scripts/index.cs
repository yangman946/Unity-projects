using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class index : MonoBehaviour
{
    public int id;
    public GameObject player;
    inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = player.GetComponent<inventory>();
        

        if (inventory.index == id)
        {
            
            Debug.Log(id + " is active");
        }
        else if (inventory.index != id)
        {
            gameObject.GetComponent<RawImage>().enabled=false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (inventory.index == id && inventory.optionsenabled == false)
        {
            gameObject.GetComponent<RawImage>().enabled = true;

        }
        else if (inventory.index != id && inventory.optionsenabled == false)
        {
            gameObject.GetComponent<RawImage>().enabled = false;
        }
    }
}
