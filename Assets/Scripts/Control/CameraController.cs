using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject toFollow;

    private Vector3 oldLocation;

    public Vector3 offset;

    public Vector3 field;

    public float resetSpeed;
    public float threshold;

	// Use this for initialization
	void Start () {
		if (toFollow == null) {
            Debug.LogWarning("There is no object for the camera to follow.");
        }

        transform.position = toFollow.transform.position + offset;
    }

    Vector2 vecAbs(Vector2 vec) {
        return new Vector2(Mathf.Abs(vec.x), Mathf.Abs(vec.y)); //This is to project everything on the plane z = 0
    }

	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 diff = toFollow.transform.position - oldLocation; //The amount toFollow has moved in the last frame.
        Vector2 cameraOffset = transform.position - toFollow.transform.position; //The difference between the camera and toFollow (on the plane z = 0)
        //Note: cameraOffset is the vector pointing TOWARDS the player.

        //If toFollow goes outside of the "field" boundaries, move the camera the same speed as toFollow.
        if (cameraOffset.x > field.x || cameraOffset.y > field.y || cameraOffset.x < -field.x || cameraOffset.y < -field.y) {
            transform.position += diff;
        }

        //If toFollow is moving slowly enough, move the camera to bring it to center.
        if (diff.magnitude<threshold) {
            //Because cameraOffset points towards the player, multiply it by -1 to flip the vector.
            Vector2 direction = cameraOffset * -1 * resetSpeed * Time.deltaTime;
            transform.position += new Vector3(direction.x, direction.y);
        }

        //This is to track the difference between frames -- keep this at the end of the function.
        oldLocation = toFollow.transform.position;
	}
}
