using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyMovement : MonoBehaviour
{

    private bool dirRight = true;
    public float speed = 2.0f;
    private int countRight = 0;
    private bool dirUp = true;
    private Vector3 pos;
    private int sum = 0;

    public int countUp = 0;
    public float xDistance, zDistance;

    public float disableTriggerTime = 5f;
    public float disableStartTime;
    private bool triggerEnabled = true;
    // Use this for initialization
    void Start()
    {
        pos = transform.position;
        triggerEnabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hitting something");
        if(collision.CompareTag("Player") && triggerEnabled)
        {
            SceneSwitchController ssc = GameObject.Find("Scene Switcher").GetComponent<SceneSwitchController>();            
            ssc.passingGameObject = this.gameObject;
            ssc.passingObject = this.gameObject.GetComponent<Enemy>();
            ssc.save = true;

        }
    }

    public void tempDisableTrigger() {
        //Called when the player runs from a fight.
        triggerEnabled = false;
        disableStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (!triggerEnabled) {
            if (Time.time - disableStartTime >= disableTriggerTime) {
                triggerEnabled = true;
            }
        }
        if(sum == countRight)
        {
            if(dirRight)
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            else
                transform.Translate(-Vector2.right * speed * Time.deltaTime);

            if(transform.position.x - pos.x >= xDistance)
            {
                dirRight = false;
                countRight++;
                sum++;
            }

            if(transform.position.x <= pos.x)
            {
                dirRight = true;
                countRight++;
            }

        } else
        {

            if(dirUp)
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            else
                transform.Translate(-Vector2.up * speed * Time.deltaTime);
            if(transform.position.z - pos.z >= zDistance)
            {
                dirUp = false;
            }

            if(transform.position.z <= pos.z)
            {
                dirUp = true;
                sum++;
            }


        }
    }

}