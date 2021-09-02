using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public static bool triggerplayer = false;
    // Start is called before the first frame update
    void Start()
    {
        triggerplayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            triggerplayer = true;
            Debug.Log("touching player");
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (triggerplayer == true)
        {
            triggerplayer = false;
            //Debug.Log("not touching player");
        }
    }
}
