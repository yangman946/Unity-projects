/* for future reference: 
 *  - change values according to mode:
 *  - health 
 *  - attack rate
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class healthscript : MonoBehaviour
{
    public static int health = 100;
    public Text healthtext_debug;
    public bool hit = false;
    public Transform camerauser;
    public Transform enemyview;
    public float speed = 4f;
    bool enableblack;
    public GameObject camera2;
    public GameObject camera1;

    public Animator anim;
    public GameObject deathblood;
    public GameObject gamemenu;
    public AudioSource jumpscare;

    bool die = false;
    bool deaths = false;
    bool playedsound = false;

    public bool deadd = false;
    public bool deadd1 = false;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        health = 100;
        hit = false;
        deathblood.SetActive(false);
        anim.SetBool("die", false);
        gamemenu.SetActive(false);
        camera1.SetActive(true);
        camera2.SetActive(false);
        die = false;
        deaths = false;
        playedsound = false;
        deadd = false;
        deadd1 = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (hit == true)
        {
            
            //death
            hit = false;
            deadd1 = true;
            if (playedsound == false)
            {
                playedsound = true;
                StartCoroutine(playsound());
            }
            gameObject.GetComponent<FirstPersonController>().enabled = false;//stopmoving

            Vector3 dir = enemyview.position - transform.position;
            dir.y = 0; // keep the direction strictly horizontal
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);

            
            StartCoroutine(waiter());
            

            Debug.Log("you are dead");
        }

        if (die)
        {
            Die();
        }

        if (deaths)
        {
            dead();
        }
        

    }

    void Die()
    {
        die = false;
        camera1.SetActive(false);
        camera2.SetActive(true);
        anim.SetBool("die", true);
        deathblood.SetActive(true);
        StartCoroutine(death());
    }

    void dead()
    {
        deadd = true;
        Time.timeScale = 0f;
        gamemenu.SetActive(true);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f); //attack rate <--- change according to mode
        die = true;
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(2f);
        deaths = true;
    }

    IEnumerator playsound()
    {
        jumpscare.Play();
        yield return new WaitForSeconds(jumpscare.clip.length);
    }

    //IEnumerator wait()
    //{
        //camerauser.LookAt(enemyview);
        //yield return null;
       
    //}
}
