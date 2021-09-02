using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour {

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameoverconfirmed;

    public static gamemanager Instance;

    public GameObject startpage;
    public GameObject gameoverpage;
    public GameObject countdownPage;
    public Text scoreText;

    enum PageState
    {
        None,
        Start,
        GameOver,
        Countdown
    }

    int score = 0;
    bool Gameover = true;

    public bool gameover { get { return Gameover; } }
    public int Score { get { return score; } }

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        countdowntext.OnCountDownFinished += OnCountDownFinished;
        tapcontroller.OnPlayerDied += OnPlayerDied;
        tapcontroller.OnPlayerScored += OnPlayerScored;

    }

    void OnDisable()
    {
        countdowntext.OnCountDownFinished -= OnCountDownFinished;
        tapcontroller.OnPlayerDied -= OnPlayerDied;
        tapcontroller.OnPlayerScored -= OnPlayerScored;
    }

    void OnCountDownFinished()
    {
        Setpagestate(PageState.None);
        OnGameStarted();
        score = 0;
        Gameover = false;
    }

    void OnPlayerDied()
    {
        Gameover = true;
        int savedScore = PlayerPrefs.GetInt("HighScore");
        if (score > savedScore)
        {
            PlayerPrefs.SetInt("HighScore", score);

        }
        Setpagestate(PageState.GameOver);
    }

    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();

    }

    void Setpagestate(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startpage.SetActive(false);
                gameoverpage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.Start:
                startpage.SetActive(true);
                gameoverpage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startpage.SetActive(false);
                gameoverpage.SetActive(true);
                countdownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startpage.SetActive(false);
                gameoverpage.SetActive(false);
                countdownPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        //activated when replay button is hit
        OnGameoverconfirmed();
        scoreText.text = "0";
        Setpagestate(PageState.Start);
    }

    public void StartGame()
    {
        //activated when play butting hit
        Setpagestate(PageState.Countdown);
    }
}
