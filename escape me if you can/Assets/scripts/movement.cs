/*
 * resposible for stamina 
 * responsible for crouching
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class movement : MonoBehaviour
{
    FirstPersonController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
