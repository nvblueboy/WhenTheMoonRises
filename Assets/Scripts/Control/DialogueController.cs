using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class DialogueController : MonoBehaviour {
    private GameObject uiDialogue, canDialogue;
    private PlayerMovementController player;
    private ChoiceSelector choiceSelector;    
    private Text txtSpeaker, txtDialogue, actionText;   
    private DialogueComponent currentDialogue;
    private string sceneName;
    private float lastSkipTime, lastShowChoiceTime, oldSkip;
    private bool canSkip, dialogueActive;
    private Dictionary<int, DialogueComponent> dialogue;

    public string inputFile;
    public int initialDialogueIndex, endDialogueIndex;

    // Start
    void Start () {
        canSkip = false;
        dialogueActive = false;
        sceneName = SceneManager.GetActiveScene().name;
        
        try {
            actionText = GameObject.FindGameObjectWithTag("TempUI").GetComponent<Text>();
        } catch (NullReferenceException e) { }

        try {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        } catch (NullReferenceException e) { }               

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

        // If there is initial dialogue to display and it hasn't been displayed before
        if (initialDialogueIndex != 0 && !GameController.getLoadedScenes().Contains(sceneName)
            || !sceneName.Contains("Day"))
        {
            Show(initialDialogueIndex);
        }

        GameController.addLoadedScene(sceneName);
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
        else
        {
           
        }

        if (actionText != null)
        {
            actionText.text = "Actions: " + (6 - ActionController.getActionCount());
        }
    }

    // Skip
    private void Skip()
    {
        int next = currentDialogue.Next();        
        if(next == 0)
        {
            if (player != null)
            {
                Debug.Log("Player can move");
                player.setPlayerCanMove(true);
            }

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
            if(player != null)
            {
                Debug.Log("Player can move");
                player.setPlayerCanMove(true);
            }

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

        if (player != null)
        {
            Debug.Log("Player can't move");
            player.setPlayerCanMove(false);
        }
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
