using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
    //main variables
    public int health;
    public int damage;
    public float speed;

    public float strength; //push
    float enemyStrength;

    GameObject middle;
    public float MIDDIST;

    public float hitRate;

    Vector3 destination;

    public Animator anim;
    bool inaction = false;
    string currentAction;

    bool isblock = false;
    bool farfromMiddle = false;

    bool ishit = false;
    bool tempbool = false;
    bool delaybool = false;
    bool requestHit = false;

    public GameObject otherPlayer;

    //combo
    int combo = 0;


    // Start is called before the first frame update
    void Start()
    {
        middle = GameObject.FindGameObjectWithTag("middle");
        destination = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(middle.transform.position, transform.position);

        if (dist > MIDDIST)
        {
            //move towards middle, you are too far away
            inaction = true;
            farfromMiddle = true;
            anim.SetBool("walk", true);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, middle.transform.position, step);
        }
        if (farfromMiddle == true && dist <= MIDDIST)
        {
            anim.SetBool("walk", false);
            
            farfromMiddle = false;
            inaction = false;
        }

        

        if (ishit)
        {

            if (delaybool == false)
                delaybool = true; Invoke("activate", 0.5f); 
            
        }
        if (requestHit && inaction == false)
        {
            requestHit = false;
            ishit = true;
            enemyStrength = strength;
            anim.SetBool("impact", true);
            currentAction = "impact";
            inaction = true;
            destination = transform.position - transform.forward;
            Invoke("reset", UpdateAnimClipTimes("Stomach Hit"));
            

        }


    }

    public void activate()
    {
        //transform.Translate(-Vector3.forward * Time.deltaTime * enemyStrength);
        
        transform.position = Vector3.Lerp(transform.position, destination, enemyStrength * Time.deltaTime);
        if (!tempbool)
        {
            tempbool = true;
            Invoke("interval", 0.01f); //how long does the guy move back
        }
        delaybool = false;
    }

    public void takeDamage(int damage, float strength)
    {
        if (inaction)
        {
            requestHit = true;
        }
        else
        {
            ishit = true;
            enemyStrength = strength;
            anim.SetBool("impact", true);
            currentAction = "impact";
            inaction = true;
            destination = transform.position - transform.forward;
            Invoke("reset", UpdateAnimClipTimes("Stomach Hit"));
        }
    }

    void interval()
    {
        ishit = false;
        tempbool = false;
    }

    private string WaitForSecondsThenExecute(Func<object> p, float v)
    {
        throw new NotImplementedException();
    }

    public float UpdateAnimClipTimes(string clipName)
    {
        float duration = 0;
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                duration = clip.length;
                //Debug.Log(clip.length.ToString());
            }

        }


        return duration;
    }

    public void reset()
    {
        anim.SetBool(currentAction, false);
        //Debug.Log("done");
        inaction = false;
    }
}