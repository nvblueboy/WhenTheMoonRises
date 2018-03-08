using UnityEngine.UI;
using UnityEngine;

public class BuildingTrigger : MonoBehaviour {
    Text promptText;
    GameObject player;
    public string scene;

    // Start
    void Start () {
        promptText = GameObject.FindGameObjectWithTag("PromptText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");		
	}

    // OnTriggerStay
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            promptText.text = "Press 'E' to enter";

            if (Input.GetButtonDown("Enter"))
            {
                if (scene.Length > 0)
                {
                    GameController.LoadScene(scene, player.transform.position);
                } 
                else
                {
                    Debug.Log("It appears to be locked");
                    // Will display some kind of feedback here. (ex. It's locked)
                }                             
            }
        }
    }

    // OnTriggerExit
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {            
            promptText.text = "";
        }
    }
}
