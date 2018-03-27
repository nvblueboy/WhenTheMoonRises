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
    private string sceneName, inputFile;
    private float lastNextTime, lastShowChoiceTime, oldNext;
    private int lastChoiceID, startIndex, endIndex;
    private bool canNext, dialogueActive;
    private Dictionary<int, DialogueComponent> dialogue;
    
    public int startIndexOne, endIndexOne, startIndexTwo, endIndexTwo;

    // Start
    void Start () {
        canNext = false;
        dialogueActive = false;
        lastChoiceID = 0;
        sceneName = SceneManager.GetActiveScene().name;

        if(sceneName.Contains("Day"))
        {
            inputFile = sceneName;                             
        }
        else
        {
            // Shops will use the JSON dialogue file belonging to the current Day scene
            inputFile = GameController.GetPreviousScene();
        }        

        switch (inputFile[3])
        {
            case '1':
                startIndex = startIndexOne;
                endIndex = endIndexOne;
                break;
            case '2':
                startIndex = startIndexTwo;
                endIndex = endIndexTwo;
                break;
        }                
        
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

        lastNextTime = -999f;   
        lastShowChoiceTime = -999f;
                
        dialogue = DialogueUtils.initDialogueForScene(inputFile);

        // Used only to translate dialogue from CSV into JSON
        //DialogueUtils.storeDialogueFromFile("Day2", "Day2");

        // If there is initial dialogue to display and it hasn't been displayed before
        if (startIndex != 0 && !GameController.getLoadedScenes().Contains(sceneName)
            || !sceneName.Contains("Day"))
        {
            Show(startIndex);
        }

        GameController.addLoadedScene(sceneName);
    }
	
	// Update
	void Update () {
        float newNext = Input.GetAxis("Jump");        
        if(oldNext < 1 && newNext > 0 && Time.time - lastNextTime > .2f && canNext)
        {
            Next();            
        }
        oldNext = newNext;

        float close = Input.GetAxis("Close");        
        if(close > 0 && ActionController.getActionCount() < 6)
        {
            Close();
        }

        float skip = Input.GetAxis("Skip");
        if(skip > 0 && ActionController.getActionCount() < 6)
        {            
            Skip();
        }

        if (!dialogueActive)
        {
            uiDialogue.SetActive(false);
        }        

        if (actionText != null)
        {
            actionText.text = "Actions: " + (6 - ActionController.getActionCount());
        }
    }

    // Next
    private void Next()
    {
        int next = currentDialogue.Next();        
        if(next == 0)
        {
            if (player != null)
            {                
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
            if(ActionController.getActionCount() >= 6)
            {
                Show(endIndex);
            }
            else
            {
                lastChoiceID = currentDialogue.id;
                canDialogue.SetActive(false);
                choiceSelector.ShowChoices(currentDialogue.choiceWrapper.choices);
                canNext = false;
            }                                                         
        }
    }

    // Close
    public void Close()
    {
        Show(0);
        lastChoiceID = 0;
        if (!sceneName.Contains("Day"))
        {
            GameController.LoadPreviousScene();
        }
    }

    // Skip
    public void Skip()
    {
        int id = currentDialogue.id;
        int prevID = id;        

        while (id != 0)
        {            
            if (id < 0)
            {
                lastChoiceID = currentDialogue.id;
                canDialogue.SetActive(false);
                choiceSelector.ShowChoices(dialogue[prevID].choiceWrapper.choices);
                canNext = false;                               
                return;
            }
            else
            {
                if(dialogue[id].action != Constants.Action.NONE)
                {
                    ActionController.performAction(dialogue[id].action);
                    Show(0);
                    return;
                }               
            }            
            prevID = id;
            id = dialogue[id].Next();                        
        }

        if (currentDialogue.Next() == 0)
        {
            Close();
            return;
        }

        if (lastChoiceID == 0)
        {
            Close();
            return;
        }
        Show(lastChoiceID);        
    }

    // Select
    public void Select(int choice)
    {
        // Show appropriate dialogue and perform correct action for selected choice               
        Choice selectedChoice = currentDialogue.choiceWrapper.choices[choice];        
        Show(selectedChoice.next);        
        ActionController.performAction(selectedChoice.actionCode);
        lastNextTime = Time.time;
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
        canNext = true;
        dialogueActive = true;
        uiDialogue.SetActive(true);
        canDialogue.SetActive(true);
        lastNextTime = Time.time;
    }    
}