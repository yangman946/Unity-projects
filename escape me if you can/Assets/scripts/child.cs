using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child : MonoBehaviour
{
    wood parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<wood>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transfer (float damage)
    {
        parent.takedamage(damage);
    }
}
