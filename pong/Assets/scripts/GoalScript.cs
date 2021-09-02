using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    // Start is called before the first frame update
    //The player that will get points for scoring
    public int player;
    //Registers as soon as an object enters the trigger area
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Call the GoalScored function in the GameManager script
        GameManager.Instance.GoalScored(player);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
