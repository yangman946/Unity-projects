using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour {

    public GameObject pauseobject;


    void Start()
    {
        Time.timeScale = 1;
        pauseobject.SetActive(false);
    }

    public void restarts()
    {
        SceneManager.LoadScene("main");
    }

    public void pause()
    {
        Time.timeScale = 0;
        pauseobject.SetActive(true);
    }

    public void unpause()
    {
        Time.timeScale = 1;
        pauseobject.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void menu()
    {
        SceneManager.LoadScene("startscene");
    }
}
