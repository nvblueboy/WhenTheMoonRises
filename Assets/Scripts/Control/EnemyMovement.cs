using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private bool dirRight = true;
    public float speed = 2.0f;
    private int countRight = 0;
    private bool dirUp = true;
    private Vector3 pos;
    public int countUp = 0;
    // Use this for initialization
    void Start () {
        pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(countRight % 2 != 0)
        {
            if (dirRight)
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            else
                transform.Translate(-Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= 1.0f)
            {
                dirRight = false;
                countRight++;
            }

            if (transform.position.x <= -1)
            {
                dirRight = true;
                countRight++;
            }
        }
        else
        {

            if (dirUp)
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            else
                transform.Translate(-Vector2.up * speed * Time.deltaTime);
            if (transform.position.z >= 1.0f)
            {
                dirUp = false;
                countRight++;
            }

            if (transform.position.z <= -1)
            {
                dirUp = true;
                countRight++;
            }

        }
    }
}
