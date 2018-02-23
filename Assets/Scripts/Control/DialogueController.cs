using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    private static GameObject uiDialogue, canDialogue;
    private static Text txtSpeaker, txtDialogue;   
    private static DialogueComponent currentDialogue;
    private static float lastSkipTime, lastShowChoiceTime, oldSkip;
    private static bool canSkip;
    private static Dictionary<int, DialogueComponent> dialogue;

    // Start
    void Start () {
        canSkip = false;

        // Initialize all parent GameObjects for hiding and showing dialogue UI
        uiDialogue = GameObject.FindGameObjectWithTag("DialogueUI");
        canDialogue = uiDialogue.transform.GetChild(1).gameObject;        

        // Initialize dialogue text
        txtSpeaker = canDialogue.transform.GetChild(1).GetComponent<Text>();
        txtDialogue = canDialogue.transform.GetChild(0).GetComponent<Text>();        
        
        lastSkipTime = -999f;  // Used to prevent skipping of dialogue following choice 
        lastShowChoiceTime = -999f;              

        dialogue = DialogueUtils.initDialogueForCurrentScene();

        Show(63);             
    }
	
	// Update
	void Update () {
        float newSkip = Input.GetAxis("Jump");        
        if(oldSkip < 1 && newSkip > 0 && Time.time - lastSkipTime > .2f && canSkip)
        {
            Skip();            
        }
        oldSkip = newSkip;	
	}

    // Skip
    private void Skip()
    {
        int next = currentDialogue.Next();
        Debug.Log("Next: " + next);
        if(next == 0)
        {
            Debug.Log("Next is 0, return");
            canDialogue.SetActive(false);
            uiDialogue.SetActive(false);
            return;
        }
        else if(next > 0)
        {
            Show(next);
        }
        else
        {
            canDialogue.SetActive(false);            
            ChoiceSelector.ShowChoices(currentDialogue.choiceWrapper.choices);
            canSkip = false;                                                
        }
    }

    // Select
    public void Select(int choice)
    {
        // Show appropriate dialogue and perform correct action for choice
        Debug.Log("Choice: " + choice);        
        Choice selectedChoice = currentDialogue.choiceWrapper.choices[choice];
        Debug.Log("Selected choice next: " + selectedChoice.next);
        Show(selectedChoice.next);
        Debug.Log("Selected choice name: " + selectedChoice.text);
        ActionController.performAction(selectedChoice.actionCode);
        lastSkipTime = Time.time;
    }

    // Show
    public static void Show(int key)
    {
        if(key < 1)
        {
            uiDialogue.SetActive(false);
            return;
        }

        canSkip = true;
        uiDialogue.SetActive(true);  // activate dialogue UI components 
        canDialogue.SetActive(true);          
        lastSkipTime = Time.time;
        currentDialogue = dialogue[key];
        txtDialogue.text = currentDialogue.text;
        txtSpeaker.text = currentDialogue.speaker;
    }    
}
