using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject toFollow;

    private Vector3 oldLocation;

    public Vector3 offset;

    public Vector3 field;

	// Use this for initialization
	void Start () {
		if (toFollow == null) {
            Debug.LogWarning("There is no object for the camera to follow.");
        }
	}

    Vector3 vecAbs(Vector3 vec) {
        return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), 0); //This is to project everything on the plane z = 0
    }

	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 diff = toFollow.transform.position - oldLocation;
        transform.position = toFollow.transform.position + offset;



        oldLocation = toFollow.transform.position;
	}
}
