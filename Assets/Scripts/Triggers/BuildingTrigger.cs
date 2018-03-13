using UnityEngine.UI;
using UnityEngine;

public class BuildingTrigger : MonoBehaviour {
    Text promptText;
    GameObject player;
    DialogueController dialogueController;
    public string scene;

    // Start
    void Start () {
        promptText = GameObject.FindGameObjectWithTag("PromptText").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueController = GameObject.FindGameObjectWithTag("DialogueController")
            .GetComponent<DialogueController>();		
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
                    // Dialogue for all buildings that can't be entered
                    dialogueController.Show(new DialogueComponent(
                        999, 0, "Sunny", "It appears to be locked."));                    
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
