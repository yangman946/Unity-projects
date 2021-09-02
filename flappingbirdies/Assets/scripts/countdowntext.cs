﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class countdowntext : MonoBehaviour {

    public delegate void CountdownFinished();
    public static event CountdownFinished OnCountDownFinished;

    Text countdown;

    void OnEnable()
    {
        countdown = GetComponent<Text>();
        countdown.text = "3";
        StartCoroutine("Countdown");

    }

    IEnumerator Countdown()
    {
        int count = 3;
        for (int i = 0; i < count; i++)
        {
            countdown.text = (count - i).ToString();
            yield return new WaitForSeconds(1);


        }

        OnCountDownFinished();
    }
}
