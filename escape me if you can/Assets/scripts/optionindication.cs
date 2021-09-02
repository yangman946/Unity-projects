using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionindication : MonoBehaviour
{

    public int id;
    public GameObject player;
    inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = player.GetComponent<inventory>();


        if (inventory.newindex == id)
        {

            //Debug.Log(id + " is active");
        }
        else if (inventory.newindex != id)
        {
            gameObject.GetComponent<RawImage>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (inventory.newindex == id && inventory.optionsenabled == true)
        {
            gameObject.GetComponent<RawImage>().enabled = true;

        }
        else if (inventory.newindex != id && inventory.optionsenabled == true)
        {
            gameObject.GetComponent<RawImage>().enabled = false;
        }
    }
}
