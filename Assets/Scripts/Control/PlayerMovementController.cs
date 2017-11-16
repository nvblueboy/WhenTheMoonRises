using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public float speed;

    private CharacterController controller;

    private float yPosition;

	// Use this for initialization
	void Start () {
        controller = this.GetComponent<CharacterController>();

        yPosition = this.transform.position.y;

        if(controller==null) {
            Debug.LogError("There's no Character controller on: " + transform.name);
        }
	}
	
	// Update is called once per frame
	void Update () {

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horiz, vert).normalized;

        
        float angle = Mathf.Atan2(vert, horiz) * Mathf.Rad2Deg;

        if (angle > -22.5 && angle <= 22.5)
        {
            movement = new Vector2(1, 0).normalized;
        } else if (angle > -67.5 && angle <= -22.5)
        {
            movement = new Vector2(1, -1).normalized;
        } else if (angle > -112.5 && angle <= -67.5)
        {
            movement = new Vector2(0, -1).normalized;
        } else if (angle > -157.5 && angle <= -112.5)
        {
            movement = new Vector2(-1, -1).normalized;
        } else if (angle > 157.5 || angle < -157.5)
        {
            movement = new Vector2(-1, 0).normalized;
        } else if (angle > 112.5 && angle <= 157.5)
        {
            movement = new Vector2(-1, 1).normalized;
        } else if (angle > 67.5 && angle <= 112.5)
        {
            movement = new Vector2(0, 1).normalized;
        } else if (angle > 22.5 && angle <= 67.5)
        {
            movement = new Vector2(1, 1).normalized;
        }


        if (horiz == 0 && vert == 0)
        {
            movement = new Vector2(0, 0);
        }

        Vector3 movement3d = new Vector3(movement.x, 0, movement.y);

        controller.Move(movement3d * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
	}
}
