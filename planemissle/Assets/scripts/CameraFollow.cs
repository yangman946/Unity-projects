using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	GameObject targetToFollow;

	// Update is called once per frame
	void Update () {
        if (targetToFollow == null && hit.isdead == true)
        {
            return;
        }
        else
        {
            transform.position = new Vector3(targetToFollow.transform.position.x,
    targetToFollow.transform.position.y - 1f,
    transform.position.z);
        }

	}
}
