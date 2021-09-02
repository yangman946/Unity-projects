using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class tapcontroller : MonoBehaviour {

    public delegate void playerDelegate();
    public static event playerDelegate OnPlayerDied;
    public static event playerDelegate OnPlayerScored;


    public float tapForce = 10;
    public float tiltSmooth = 5;
    public Vector3 startPos;

    public AudioSource tapaudio;
    public AudioSource scoreaudio;
    public AudioSource dieaudio;

    Rigidbody2D rigidbody;
    Quaternion downRotation;
    Quaternion forwardRotation;

    gamemanager game;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = gamemanager.Instance;
        rigidbody.simulated = false;
    }

    void OnEnable()
    {
        gamemanager.OnGameStarted += OnGameStarted;
        gamemanager.OnGameoverconfirmed += OnGameoverconfirmed;
    }

    void OnDisable()
    {
        gamemanager.OnGameStarted -= OnGameStarted;
        gamemanager.OnGameoverconfirmed -= OnGameoverconfirmed;
    }

    void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;

    }

    void OnGameoverconfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (game.gameover) return;
        if (Input.GetMouseButtonDown(0))
        {
            tapaudio.Play();
            transform.rotation = forwardRotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "scorezone")
        {
            //register score 
            OnPlayerScored(); //event sent to gamemanager
            //playsound
            scoreaudio.Play(); ;
        
        }

        if (col.gameObject.tag == "deadzone")
        {
            rigidbody.simulated = false;
            //register dead event
            OnPlayerDied();
            //playsound
            dieaudio.Play();
        }
    }
}
