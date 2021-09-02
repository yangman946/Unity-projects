using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckControl : MonoBehaviour
{
    public float speed = 7;
    Rigidbody2D rigidbody;
    bool initialBurst;
    Vector2 direction = new Vector2();
    Vector2 StartPosition = new Vector2(0, 0);
    public void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    //A custom function that will be used to shoot the puck towards the player that

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.tag == "player1")
        {
            //speed += 1;
            //Debug.Log("hit " + speed);
            //initialBurst = true;
        }
    }

    public void ShootPuck(int player)
    {

        //Randomise the Y value the puck will shoot at
        direction.y = Random.Range(-1.0f, 1.0f);
        //If the last player to score was left, shoot the puck right and vice versa
        if (player == 0)
        {
            direction.x = 1;
        }
        else
        {
            direction.x = -1;
        }
        initialBurst = true;
    }
    //Move the puck back to the starting position
    public void ResetPuck()
    {
        speed = 7;
        //Remove the force on the puck
        rigidbody.velocity = new Vector2(0, 0);
        //Move the Puck to the starting position
        rigidbody.position = StartPosition;
    }
    private void FixedUpdate()
    {
        if (initialBurst)
        {
            //Add force to the puck to shoot it in the specified direction. Apply
         
            rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
            initialBurst = false;
        }
        //Stops the speed of the puck from going over the maximum value
        rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -speed,
        speed), Mathf.Clamp(rigidbody.velocity.y, -speed, speed));

    }
}
