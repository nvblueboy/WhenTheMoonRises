using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour {

    public float speed;
    public Text countText;

    private CharacterController controller;
    private int shardCount;
    private int collectWin = 2;  //temp value - must inheret from global config

    // Use this for initialization
    void Start() {
        controller = this.GetComponent<CharacterController>();

        if (controller == null) {
            Debug.LogError("There's no Character controller on: " + transform.name);
        }

        SetCountText();
    }

    // Update is called once per frame
    void Update() {

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (Mathf.Abs(horiz) > Mathf.Abs(vert)) {
            vert = 0f;
        } else {
            horiz = 0f;
        }

        Vector3 movement = new Vector3(horiz, vert, 0);



        controller.Move(movement * speed * Time.deltaTime);
    }

    //Collect star shards
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("StarShard"))
        {
            collision.gameObject.SetActive(false);
            shardCount++;
            SetCountText();

        }
    }

    //Collectibles Counter
    void SetCountText()
    {
        countText.text = "Star shards: " + shardCount.ToString();
        if (shardCount >= collectWin)
            countText.color = Color.green;         
    }


}
