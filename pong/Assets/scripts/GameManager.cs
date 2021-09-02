using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int[] playerScores = new int[2];
    //By creating a static variable we store the result in the class itself rather
    public PuckControl puck;
    public float shootTimer = 3;
    int scored;
    float timer;
    bool enableTimer;
    static private GameManager instance;
    //Allow the GameManager to be accessed from other classes without being declared
    public Text[] playersUI;
    public Text winMessage;
    public int requiredScore = 5;
    static public GameManager Instance
    {
        get { return instance; }
    }
    //Awake is called before any Start functions within the scene
    void Awake()
    {
        //Check to see if an instance already exists
        if (instance == null)
            //if not, set to this instance
            instance = this;
        //If one does exist and it isn't this



      
        else if (instance != this)
            //Then destroy this instance
            Destroy(gameObject);
        //Sets this instance to not be destroyed when reloading the scene
        DontDestroyOnLoad(gameObject);
        //Calls the SetupGame function
        SetupGame();
    }
    //A custom method used to complete all the pre-game setup functions
    void SetupGame()
    {
        timer = shootTimer;
        enableTimer = true;
        scored = 1;
        //reset the player scores
        playerScores = new int[2];
        for (int i = 0; i < playersUI.Length; i++)
        {
            playersUI[i].text = playerScores[i].ToString();
        }
    }
    //Will be called when goals are scored
    public void GoalScored(int player)
    {
        //increment the score of the player who scored
        playerScores[player]++;
        puck.ResetPuck();
        //enable the countdown timer to shoot the puck
        timer = shootTimer;
        enableTimer = true;
        scored = player;
        //Update the Player's Score
        playersUI[player].text = playerScores[player].ToString();
        //Check if enough goals have been scored
        if (playerScores[player] >= requiredScore)
        {
            //Set the win message text
            winMessage.text = "Player " + (player + 1) + " Wins!";
            //Stop the game
            Time.timeScale = 0;
        }
    }
    //Return score of the specifed player
    public int GetPlayerScore(int player)
    {
        return playerScores[player];
    }

    private void Update()
    {
        //If the timer has been enabled
        if (enableTimer)
        {
            //Reduce the timer by the amount of seconds it took to get from the last
    
            timer -= Time.deltaTime;
            //If the timer runs out
            if (timer <= 0)
            {
                //Shoot the Puck
                puck.ShootPuck(scored);
                enableTimer = false;
            }
        }
    }



}
