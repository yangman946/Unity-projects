using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class exittrigger : MonoBehaviour
{
    bool exit = false;
    public bool trigger = false;
    public bool havekey = false;
    bool keyused = false;
    public bool finish = false;
    public Text infoline; //use as the same as shed trigger
    GameObject player;
    Image[] boxinvent;
    bool playedsound2 = false;
    bool playedsound1 = false;
    bool finishsoun1 = false;

    public GameObject finsihobj;
    //public GameObject credits;
    public AudioSource opengate;
    public AudioSource run;

    public Animator anim;
    public Text info;
    bool showtext = false;

    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        exit = false;
        havekey = false;
        trigger = false;
        finishsoun1 = false;

        keyused = false;
        finish = false;
        done = false;
        playedsound1 = false;
        playedsound2 = false;
        player = GameObject.FindGameObjectWithTag("Player");
        boxinvent = player.GetComponent<inventory>().boxes;
        finsihobj.SetActive(false);
        //credits.SetActive(false);
        info.text = "";
        showtext = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<inventory>().shouldshedtextshow == true)
        {
            infoline.text = "";
        }

        if (havekey == true && player.GetComponent<inventory>().useexitkey == true)
        {
            //we have a key and the key is selected by the user to be used
            havekey = false;
            keyused = true;
            finishgame();
        }


        if (trigger == true && player.GetComponent<inventory>().shouldshedtextshow == false)
        {

            if (keyused == false) //
            {
                infoline.text = "The exit is locked";
                //disabletext.text = "";
            }
        }

        if (playedsound1 == true && playedsound2 == false & finishsoun1)
        {
            anim.SetBool("start", true);
            //Debug.Log("credits");
            playedsound2 = true;
            StartCoroutine(playsound2());

        }

        if (done == true)
        {
            
            Debug.Log("you finished game!");
            StartCoroutine(waittext());
            //credits.SetActive(true);
        }

        if (showtext == true)
        {
            info.text = "Thanks for playing, press the 'escape' key to go back to the menu screen.";

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            trigger = true;
            for (int i = 0; i < player.GetComponent<inventory>().length; i++)
            {
                if (boxinvent[i].GetComponent<slot>().slotexitkey == true)
                {
                    Debug.Log("have box key");

                    havekey = true;
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            infoline.text = "";
            havekey = false;
            trigger = false;
        }
    }

    void finishgame()
    {
        Time.timeScale = 0;
        finsihobj.SetActive(true);
        finish = true;
        if (playedsound1 == false)
        {
            playedsound1 = true;
            StartCoroutine(playsound1());
        }


        

        //finish the game
    }

    IEnumerator playsound1()
    {
        opengate.Play();
        yield return new WaitForSecondsRealtime(opengate.clip.length);
        finishsoun1 = true;
    }

    IEnumerator playsound2()
    {
        run.Play();
        yield return new WaitForSecondsRealtime(run.clip.length);
        done = true;
    }

    IEnumerator waittext()
    {
        yield return new WaitForSecondsRealtime(3);
        showtext = true;
    }
}
