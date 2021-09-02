using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class menu : MonoBehaviour
{
    public GameObject pause;
    GameObject player;
    public bool paused = false;
    public GameObject exittrigger;
    public GameObject optionsmenu;


    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);

        paused = false;
        player = GameObject.FindGameObjectWithTag("Player");
        optionsmenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Escape) && paused == false && player.GetComponent<healthscript>().deadd == false && exittrigger.GetComponent<exittrigger>().finish == false)
        {
            player.GetComponent<FirstPersonController>().enablecursor = true;
            player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            paused = true;
            if (Time.timeScale == 1 || !player.GetComponent<inventory>().trigger) //pause
            {
                Time.timeScale = 0;

            }

            pause.SetActive(true);
        }
        else if (player.GetComponent<healthscript>().deadd == true || exittrigger.GetComponent<exittrigger>().finish == true)
        {
            player.GetComponent<FirstPersonController>().enablecursor = true;
            player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        //{
            
            //player.GetComponent<FirstPersonController>().enablecursor = false;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            //paused = false;
            //if (player.GetComponent<inventory>().trigger == false)
            //{
                //Time.timeScale = 1;
            //}
            //pause.SetActive(false);
        //}


    }

    public void resume()
    {
        Debug.Log("resume");
        player.GetComponent<FirstPersonController>().enablecursor = false;
        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        if (player.GetComponent<inventory>().trigger == false)
        {
            Time.timeScale = 1;
        }
        pause.SetActive(false);
        paused = false;
    }

    public void options()
    {
        Debug.Log("options");
        optionsmenu.SetActive(true);
    }

    public void mainmenu()
    {
        Debug.Log("mainmenu");
        paused = false;
        if (player.GetComponent<inventory>().trigger == false)
        {
            Time.timeScale = 1;
        }
        pause.SetActive(false);
        SceneManager.LoadScene(0);
        
    }

    public void back()
    {
        optionsmenu.SetActive(false);
    }

    public void restart()
    {
        SceneManager.LoadScene(1);

    }

    public void quit()
    {
        Application.Quit();
    }

}
