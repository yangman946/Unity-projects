using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public GameObject loading;
    public Slider slider;
    public GameObject optionsmenu;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        optionsmenu.SetActive(false);
    }

    public void play(int sceneindex)
    {
        Debug.Log("play");
        StartCoroutine(loadasync(sceneindex));
    }

    IEnumerator loadasync(int sceneindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneindex);

        loading.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }

    public void options()
    {
        Debug.Log("options");
        optionsmenu.SetActive(true);
    }

    public void back()
    {
        optionsmenu.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
