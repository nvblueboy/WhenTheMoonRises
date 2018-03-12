using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Name: Rabah Habiss
ID: 2268381
Email: habis102@mail.chapman.edu
Course: CPSC-340, CPSC-440
Assignment: Semester Project

Description: Deer Interaction Script.
Attach to deer and add a star to its tail as a child object, and don't forget to set the collider.

*/

public class DeerInteraction : MonoBehaviour
{

    private Collider col;
    private int counter;
    private LevelManager level;
    private Rigidbody rb;
    private bool runningBack = false;
    private GameObject player;
    private Rigidbody prb;
    private SpriteRenderer deerRender;
    private GameObject star;
    private Vector3 startPos;
    private Animator animator;
    private string animationPrefix;

    public Feedback[] dialogue;
    private FeedbackController feedbackController;

    // Use this for initialization
    void Start()
    {
        feedbackController = GameObject.FindGameObjectWithTag(
            "FeedbackController").GetComponent<FeedbackController>(); 

        startPos = transform.position;
        deerRender = GetComponent<SpriteRenderer>();
        star = this.gameObject.transform.GetChild(0).gameObject;

        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");

        prb = player.GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
        animationPrefix = "AnimationControllers/Deer/";

    }

    // Update is called once per frame
    void Update()
    {
        if (runningBack)
        {
            float step = 5 * Time.deltaTime;
            //Debug.Log("stopped: " + startPos);

            deerRender.flipX = true;
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);

            if (transform.position == startPos)
            {
                runningBack = false;
                deerRender.flipX = false;
                star.SetActive(true);
                //idle animation
                animator.runtimeAnimatorController = Resources.Load(
                animationPrefix + "t_deer") as RuntimeAnimatorController;

                //enable colliders
                foreach (Collider c in GetComponents<Collider>())
                {
                    c.enabled = true;
                }

            }
        }
    }



    private IEnumerator OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //get player speed
            float playerSpeed = prb.velocity.magnitude;

            //run away
            rb.velocity = transform.right * 5f;
            deerRender.flipX = false;
            feedbackController.showFeedback(dialogue);

            //animation for running right
            animator.runtimeAnimatorController = Resources.Load(
            animationPrefix + "t_deer 1") as RuntimeAnimatorController;

            //sorry, you can't collect the star at this time.
            star.SetActive(false);

            //disable colliders while running away
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }

            //wait before returning back
            yield return new WaitForSeconds(5f);


            //back to start
            runningBack = true;
            rb.velocity = new Vector3(0, 0, 0);

        }


    }
}
