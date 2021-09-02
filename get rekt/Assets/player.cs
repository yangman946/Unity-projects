using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //main variables
    public int health;
    public int damage;
    public int comboDamage;
    public float speed;

    public float strength;
    public float hitRate;
    float enemyStrength;
    public bool playerLeft = false;

    bool tempbool = false;
    bool ishit = false;
    bool delaybool = false;
    bool requestHit = false;
    bool blocking = false; //for reaction
    bool beginMove = true;

    GameObject middle;
    public float MIDDIST;

    public Animator anim;
    bool inaction = false;
    string currentAction;

    bool isblock = false;
    bool farfromMiddle = false;

    public GameObject otherPlayer;

    Vector3 destination;

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
        if (beginMove)
        {
            Vector3 smoothPos = Vector3.Lerp(transform.position, destination, enemyStrength);
            transform.position = smoothPos;
        }
        float dist = Vector3.Distance(middle.transform.position, transform.position);

        if (dist > MIDDIST)
        {
            //move towards middle, you are too far away
            inaction = true;
            farfromMiddle = true;
            anim.SetBool("walk", true);
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, middle.transform.position, step);
            //Debug.Log(dist.ToString());
        }
        if (farfromMiddle == true && dist <= MIDDIST)
        {
            anim.SetBool("walk", false);
            inaction = false;
            farfromMiddle = false;
        }

        //input
        if (playerLeft)
        {
            if (Input.GetKeyDown(KeyCode.D) && inaction == false)
            {
                //punch
                combo += 1;
                if (combo == 3)
                {
                    combo = 0;
                    anim.SetBool("HIT2", true);
                    inaction = true;
                    currentAction = "HIT2";
                    otherPlayer.GetComponent<player>().takeDamage(comboDamage, strength); //normal damage
                }
                else
                {
                    anim.SetBool("HIT1", true);
                    inaction = true;
                    currentAction = "HIT1";
                    otherPlayer.GetComponent<player>().takeDamage(damage, strength); //normal damage
                }
                Invoke("reset", hitRate);


            }
            else if (Input.GetKey(KeyCode.S) && inaction == false)
            {
                anim.SetBool("block", true);
                inaction = true;
                isblock = true;
                currentAction = "block";
                Invoke("reset", UpdateAnimClipTimes("Standing Block Idle"));
            }


            if (isblock && !Input.GetKey(KeyCode.S) && blocking == false)
            {
                isblock = false;
                anim.SetBool("block", false);
                inaction = false;
                CancelInvoke();
            }
        }
        else
        {
            if (Input.GetKeyDown("left") && inaction == false)
            {
                //punch
                combo += 1;
                if (combo == 3)
                {
                    combo = 0;
                    anim.SetBool("HIT2", true);
                    inaction = true;
                    currentAction = "HIT2";
                    otherPlayer.GetComponent<player>().takeDamage(comboDamage, strength); //normal damage
                }
                else
                {
                    anim.SetBool("HIT1", true);
                    inaction = true;
                    currentAction = "HIT1";
                    otherPlayer.GetComponent<player>().takeDamage(damage, strength); //normal damage
                }

                
                Invoke("reset", hitRate);

            }
            else if (Input.GetKey("down") && inaction == false)
            {
                anim.SetBool("block", true);
                inaction = true;
                isblock = true;
                currentAction = "block";
                Invoke("reset", UpdateAnimClipTimes("Standing Block Idle"));
            }


            if (isblock && !Input.GetKey("down") && blocking == false)
            {
                isblock = false;
                anim.SetBool("block", false);
                inaction = false;
                CancelInvoke();
            }
        }

        if (ishit)
        {

            if (delaybool == false)
                delaybool = true; Invoke("activate", 0.5f);

        }

        if (requestHit && inaction == false || requestHit && currentAction == "block")
        {
            ishit = true;
            enemyStrength = strength;
            if (currentAction == "block")
            {

                blocking = true;
                anim.SetBool("blockreact", true);
                currentAction = "blockreact";
                inaction = true;
                destination = transform.position - transform.forward;
                Invoke("resetBlock", UpdateAnimClipTimes("Standing Block React Large"));
            }
            else
            {
                anim.SetBool("impact", true);
                currentAction = "impact";
                inaction = true;
                destination = transform.position - transform.forward;
                Invoke("reset", UpdateAnimClipTimes("Stomach Hit"));
            }

            requestHit = false;

        }


    }

    public void takeDamage(int damage, float strength)
    {
        if (inaction && currentAction != "block")
        {
            requestHit = true;
        }
        else
        {
            ishit = true;
            enemyStrength = strength;
            if (currentAction == "block")
            {

                anim.SetBool("blockreact", true);
                currentAction = "blockreact";
                inaction = true;
                blocking = true;
                destination = transform.position - transform.forward;
                Invoke("resetBlock", UpdateAnimClipTimes("Standing Block React Large"));
            }
            else
            {
                anim.SetBool("impact", true);
                currentAction = "impact";
                inaction = true;
                destination = transform.position - transform.forward;
                Invoke("reset", UpdateAnimClipTimes("Stomach Hit"));
            }


        }

    }

    public void activate()
    {
        //transform.Translate(-Vector3.forward * Time.deltaTime * enemyStrength);

        beginMove = true;
        if (!tempbool)
        {
            tempbool = true;
            Invoke("interval", 0.01f); //how long the guy moves back
        }
        delaybool = false;
    }

    void interval()
    {
        ishit = false;
        tempbool = false;
        beginMove = false;
    }

    void resetBlock()
    {
        anim.SetBool("blockreact", false);
        inaction = false;
        blocking = false;
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
                duration = clip.length - 0.1f;
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
