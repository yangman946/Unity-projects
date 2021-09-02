using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shedcheck : MonoBehaviour
{
    public bool dontclose = false;

    // Start is called before the first frame update
    void Start()
    {
        dontclose = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dontclose = true;
            Debug.Log("dont close");
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            dontclose = false;

        } 
    }
}
