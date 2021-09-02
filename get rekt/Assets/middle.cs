using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class middle : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 temp = new Vector3(0, 16.9f, 0);
        transform.position += temp;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player1pos = p1.transform.position;
        Vector3 player2pos = p2.transform.position;

        Vector3 center = ((player1pos + player2pos) * 0.5f);
        
        transform.position = center;
    }
}
