/*  for future reference:
 *  - change values depending on mode:
 *  - speed
 *  - damage
 *  - distance threshold
 *  - playing sound? (maybe)
 *  
 *  change sound, add death, better jumpscares.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aiscript : MonoBehaviour
{
    
    //NavMeshAgent agent;
    public Transform victim;
    public Transform house;
    public Animator anim;
    public float speed= 5; //<--- change according to mode
    public float strength = .5f;
    public AudioSource enemynear; //disable if on hardmode
    public static bool soundplay = false;
    public AudioSource found;
    public bool chasingsound = true; 
    public bool run= false;

    public bool inrange = false;
   

    public int damage = 50;

    public healthscript healthplayer; 
    // Start is called before the first frame update
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        healthplayer = (healthscript)go.GetComponent(typeof(healthscript));
        //healthplayer = player.GetComponent<healthscript>();

        //agent.speed = 5f;
        soundplay = true;
        chasingsound = true;
        run = false;
        inrange = false;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(victim.position, transform.position);
        float housedist = Vector3.Distance(victim.position, house.position);

        if (dist <= 90 && soundplay == true) //when the user is in the enemys range
        {


            StartCoroutine(playsound()); //start the sound 
            inrange = true; //in range
            //Debug.Log("moving");
            //search
            GetComponent<wanderscript>().inhouserange = false;
            GetComponent<wanderscript>().outofrange = false;
        }
        else if (housedist < 100)
        {
            //Debug.Log("nearhouse");
            GetComponent<wanderscript>().inhouserange = true;
        }
        else if(dist > 90) //if the user isnt in the range
        {
            GetComponent<wanderscript>().outofrange = true; 
            //Debug.Log("not moving");
        }
        
       


        if (inrange)
        {
            GetComponent<wanderscript>().inhouserange = false;
            if (dist <= 50 && chasingsound == true) //chasing the user sound
            {
                StartCoroutine(chase());
                run = true;

            }
            else if (dist >= 60 && chasingsound == false) //when the sound (chase begun) finishes playing and the player is outside its 50 unit chase range:
            {
                //set enemy to first state
                chasingsound = true; //reset
                run = false;
                Debug.Log("run is false");
            }


            //very close to the character <-- attack player
            if (trigger.triggerplayer == true)  //<-- this statement is important because it ensures that the attack doesnt play at the same time as the chase animation
            {
                run = false; //disable run 
                anim.SetBool("chase", false); //set the animation back to previous animation;
                speed = 5;
                anim.SetBool("attack", true);

                healthplayer.hit = true;


                return; //hence the return
            }
            else if (run == true) // if the ai starts to chase player
            {
                anim.SetBool("chase", true); //animate the ai to running
                speed = 16; //increase speed
            }
            else if (run == false) //when the ai is out of the 50 unit chase range
            {
                anim.SetBool("chase", false); //set the animation back to previous animation;
                speed = 5;
            }


            //agent.SetDestination(victim.position);



            //default:

            anim.SetBool("attack", false);

            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, victim.position, step);


            //rotate to face us
            Quaternion targetRotation = Quaternion.LookRotation(victim.position - transform.position);
            float str = Mathf.Min(strength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);


            if (dist > 150 && soundplay == false)// when the user is out of the enemys range <-- doesnt apply when the game starts because soundplay is set to false
            {
                //StopCoroutine(playsound());
                StartCoroutine(FadeOut(enemynear, 3f)); // fade the sound 

                inrange = false;

            }
        }


    }

    IEnumerator playsound()
    {
        soundplay = false;
        enemynear.Play();
        //Debug.Log("playing at: " + enemynear.volume.ToString());
        yield return new WaitForSeconds(enemynear.clip.length);
        //while (enemynear.isPlaying)
        //{

        //yield return null;
        //}
        


    }


    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        soundplay = true;
        Debug.Log("fading");
        
    }

    IEnumerator chase()
    {
        chasingsound = false;
        found.Play();
        yield return new WaitForSeconds(found.clip.length);
        
    }
}
