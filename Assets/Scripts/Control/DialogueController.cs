using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    private GameObject uiDialogue, canDialogue;
    private ChoiceSelector choiceSelector;    
    private Text txtSpeaker, txtDialogue;   
    private DialogueComponent currentDialogue;
    private float lastSkipTime, lastShowChoiceTime, oldSkip;
    private bool canSkip, dialogueActive;
    private Dictionary<int, DialogueComponent> dialogue;

    public string inputFile;
    public int initialDialogueIndex, endDialogueIndex;

    // Start
    void Start () {
        canSkip = false;
        dialogueActive = false;

        // Initialize all parent GameObjects for hiding and showing dialogue UI
        uiDialogue = GameObject.FindGameObjectWithTag("DialogueUI");        
        canDialogue = uiDialogue.transform.GetChild(1).gameObject;        

        // Initialize dialogue text
        txtSpeaker = canDialogue.transform.GetChild(1).GetComponent<Text>();
        txtDialogue = canDialogue.transform.GetChild(0).GetComponent<Text>();        

        choiceSelector = GetComponent<ChoiceSelector>();        

        lastSkipTime = -999f;   
        lastShowChoiceTime = -999f;
                
        dialogue = DialogueUtils.initDialogueForScene(inputFile);
          
        if(initialDialogueIndex !=0 )
        {
            Show(initialDialogueIndex);
        }           
    }
	
	// Update
	void Update () {
        float newSkip = Input.GetAxis("Jump");        
        if(oldSkip < 1 && newSkip > 0 && Time.time - lastSkipTime > .2f && canSkip)
        {
            Skip();            
        }
        oldSkip = newSkip;

        if (!dialogueActive)
        {
            uiDialogue.SetActive(false);
        }	
	}

    // Skip
    private void Skip()
    {
        int next = currentDialogue.Next();        
        if(next == 0)
        {            
            canDialogue.SetActive(false);
            uiDialogue.SetActive(false);
            ActionController.performAction(currentDialogue.action);
            return;
        }
        else if(next > 0)
        {
            Show(next);
        }
        else
        {
            Debug.Log("Action count: " + ActionController.getActionCount());
            if(ActionController.getActionCount() >= 6)
            {
                Show(endDialogueIndex);
            }
            else
            {
                canDialogue.SetActive(false);
                choiceSelector.ShowChoices(currentDialogue.choiceWrapper.choices);
                canSkip = false;
            }                                                         
        }
    }

    // Select
    public void Select(int choice)
    {
        // Show appropriate dialogue and perform correct action for selected choice               
        Choice selectedChoice = currentDialogue.choiceWrapper.choices[choice];        
        Show(selectedChoice.next);        
        ActionController.performAction(selectedChoice.actionCode);
        lastSkipTime = Time.time;
    }

    // Show
    public void Show(int key)
    {
        if(key < 1)
        {
            uiDialogue.SetActive(false);
            dialogueActive = false;
            return;
        }

        Show(dialogue[key]);        
    }
    
    public void Show(DialogueComponent dialogue)
    {
        activateDialogueUI();
        currentDialogue = dialogue;
        txtDialogue.text = currentDialogue.text;
        txtSpeaker.text = currentDialogue.speaker;
    }
    
    // activateDialogueUI
    private void activateDialogueUI()
    {
        canSkip = true;
        dialogueActive = true;
        uiDialogue.SetActive(true);
        canDialogue.SetActive(true);
        lastSkipTime = Time.time;
    }    
}
