using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wood : MonoBehaviour
{
    float health = 20f;

    public AudioSource breaksound;
    public Rigidbody[] rb;
    bool play = false;
    bool sound = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rb)
        {
            rigidbody.isKinematic = true;
        }
        play = false;
        sound = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 0 && play == false && sound == true)//wood breaks
        {
            foreach (Rigidbody rigidbody in rb)
            {
                rigidbody.isKinematic = false;
            }

            play = true;
        }
        else
        {
            play = false; 
        }

        if (play == true)
        {
            sound = false;
            StartCoroutine(playsound());
        }

    }

    public void takedamage(float damage)
    {
        if (health < 0)
        {
            return;
        }
        
        health -= damage;


    }

    IEnumerator playsound()
    {
       
        Debug.Log("plaing");
        breaksound.Play();
        yield return new WaitForSeconds(breaksound.clip.length);
    }
}
