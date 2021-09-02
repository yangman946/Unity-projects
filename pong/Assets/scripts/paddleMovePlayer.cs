using UnityEngine;
public class paddleMovePlayer : MonoBehaviour
{
    //Global Variables
    Rigidbody2D rigidbody;
    //Public variables are visible in the inspector
    public float speed;
    public string verticalAxis = "Vertical";
    public float yBoundary = 4;
    public GameObject puck;
    public float botspeed = 0.5f;

    private void OnDrawGizmos()
    {
        //Set the colour of the line to be drawn
        Gizmos.color = Color.yellow;
        //Draw a line from the top boundary to the bottom boundary
        Gizmos.DrawLine(new Vector3(transform.position.x, yBoundary), new
        Vector3(transform.position.x, -yBoundary));
    }
    void Start()
    {
        //Assign the objects Rigidbody2D Component
        rigidbody = GetComponent<Rigidbody2D>();
        puck = GameObject.FindGameObjectWithTag("puck");
    }
    //Always used Fixed Update for any physics scripting
    void FixedUpdate()
    {
        //Use the Input library to get input from the player.
        //Using an Axis rather than direct keystroke allows players to change
        float direction = Input.GetAxis(verticalAxis);
        float direction2 = puck.transform.position.y;

        if (verticalAxis == "Vertical1") 
        {
            //Debug.Log("dir: " + direction + " dir1:" + direction2);
            if (transform.position.y < direction2)
            {
                direction += botspeed;
            }else if (transform.position.y > direction2)
            {
                direction += -botspeed;
            }
            else if (transform.position.y == direction2)
            {
                direction = 0;
            }
        }
        //Wipe the direction if it will move the object out of the Boundary
        if (transform.position.y >= yBoundary && direction > 0)
        {
            direction = 0;
        }
        else if (transform.position.y <= -yBoundary && direction < 0)
        {
            direction = 0;
        }
        //Set the velocity of the paddle to be the direction given by the player
 
        rigidbody.velocity = new Vector2(0, direction * speed);
    }
}
