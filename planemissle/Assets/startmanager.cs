using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startmanager : MonoBehaviour {

    public Text highscore;

	// Use this for initialization
	void Start () {
        highscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void play()
    {
        SceneManager.LoadScene("main");
    }

    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
