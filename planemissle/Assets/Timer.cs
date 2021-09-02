using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text timerText;
    private float startTime;

    public Text highscoretext;

    public int timers = 0;
    private int count = 1;
    private int totaltimes;

    public static bool gotpoint = false;

    // Use this for initialization
    void Start()
    {
        gotpoint = false;

        StartCoroutine(Timesr());

        startTime = Time.time;
        totaltimes = timers;


        highscoretext.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (hit.isdead == true)
        {
            StopAllCoroutines();
            return;
        }

        //float t = Time.time - startTime;

        //string seconds = (t % 60).ToString("f2");


        //int realnumber;
        //int.TryParse(seconds, out realnumber);
        int totaltime = totaltimes;
        timerText.text = totaltime.ToString();

        if (totaltime > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", totaltime);
            highscoretext.text = totaltime.ToString();
        }


    }

    IEnumerator Timesr()
    {
        gotpoint = true;
        yield return new WaitForSeconds(1);
        timers = totaltimes + count;
        totaltimes = timers;
        StartCoroutine(Timesr()); //dont know  if this will work :(
        gotpoint = false; // referenced in homing missles script, still not sure if this works!

    }
}