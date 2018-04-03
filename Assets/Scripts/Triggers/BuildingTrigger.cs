using UnityEngine.UI;
using UnityEngine;

public class BuildingTrigger : MonoBehaviour {
    Text promptText, nameText, descriptionText;
    Image backgroundImage;
    GameObject buildingUI, player;
    DialogueController dialogueController;
    public string scene, buildingName, buildingDescription;

    // Start
    void Start () {
        // initialize UI elements
        buildingUI = GameObject.FindGameObjectWithTag("BuildingUI");
        backgroundImage = buildingUI.GetComponent<Image>();
        backgroundImage.enabled = false;
        promptText = GameObject.FindGameObjectWithTag("PromptText").GetComponent<Text>();
        nameText = buildingUI.transform.GetChild(0).GetComponent<Text>();
        descriptionText = buildingUI.transform.GetChild(1).GetComponent<Text>();        
        dialogueController = GameObject.FindGameObjectWithTag("DialogueController")
            .GetComponent<DialogueController>();

        player = GameObject.FindGameObjectWithTag("Player");      
        	
	}

    // OnTriggerStay
    void OnTriggerStay(Collider other)
    {        
        if(other.gameObject.tag == "Player" && !dialogueController.getDialogueActive())
        {            
            setUIActive(true);
            if (Input.GetButtonDown("Enter"))
            {
                if (scene.Length > 0)
                {
                    if(scene == "TownHall")
                    {
                        dialogueController.Show(7);
                        return;
                    }
                    GameController.LoadScene(scene, player.transform.position);
                } 
                else
                {                    
                    // Dialogue for all buildings that can't be entered
                    dialogueController.Show(new DialogueComponent(
                        0, 0, "Sunny", "It appears to be locked."));                    
                }                             
            }
        }
        else
        {
            setUIActive(false);
        }
    }

    // OnTriggerExit
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {           
            setUIActive(false);            
        }
    }

    // activateUI
    void setUIActive(bool active)
    {
        if(active)
        {
            backgroundImage.enabled = true;
            nameText.text = buildingName;
            descriptionText.text = buildingDescription;
            promptText.text = "Press 'E' to enter";
        }
        else
        {
            backgroundImage.enabled = false;
            nameText.text = "";
            descriptionText.text = "";
            promptText.text = "";
        }        
    }
}
