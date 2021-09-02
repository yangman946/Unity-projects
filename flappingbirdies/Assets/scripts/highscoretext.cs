﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class highscoretext : MonoBehaviour {

    Text highscore;

    void OnEnable()
    {
        highscore = GetComponent<Text>();
        highscore.text = "HIGH SCORE:" + PlayerPrefs.GetInt("HighScore").ToString();
    }


}
