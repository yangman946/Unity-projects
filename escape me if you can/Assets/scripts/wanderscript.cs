using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderscript : MonoBehaviour
{
    public bool outofrange;
    public Transform[] wanderpoints;
    public Transform player;
    aiscript ai;
    public int wanderPointIndex;
    public bool arrived = false;
    public bool inhouserange = false;
    
    // Start is called before the first frame update
    void Start()
    {
        outofrange = true;
        ai = GetComponent<aiscript>();

        wanderPointIndex = Random.Range(0, wanderpoints.Length);
        arrived = false;
        inhouserange = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if (inhouserange == true && ai.inrange == false)
        {
            moveplayer();
        }
        else if (outofrange == true && ai.inrange == false && inhouserange == false)
        {
            move();


        }
        else
        {
            return;
        }

    }

    void moveplayer()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        // The step size is equal to speed times frame time.
        float step = ai.speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);


        //rotate to face point
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        float str = Mathf.Min(ai.strength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }

    public void move() //move to random point
    {
        //Debug.Log("wandering");

        float dist = Vector3.Distance(wanderpoints[wanderPointIndex].position, transform.position);

        // The step size is equal to speed times frame time.
        float step = ai.speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, wanderpoints[wanderPointIndex].position, step);


        //rotate to face point
        Quaternion targetRotation = Quaternion.LookRotation(wanderpoints[wanderPointIndex].position - transform.position);
        float str = Mathf.Min(ai.strength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);

        if (dist <= 10 && arrived == false)
        {
            //choose new point
            arrived = true;
            newpoint();
        }
    }

    void newpoint()
    {
        wanderPointIndex = Random.Range(0, wanderpoints.Length);
        arrived = false;
        move();
            
    }
}
