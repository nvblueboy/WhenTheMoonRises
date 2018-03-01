using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChoiceSelector : MonoBehaviour {
    private Text txtTopChoice, txtSelectedChoice, txtBotChoice;
    private GameObject uiDialogue, canChoiceSelector;
    private DialogueController dController;
    private List<Choice> choiceList;
    private bool choicesActive;
    private float showChoicesTime;

    private float oldScroll;
    private int currentIdx, lastIndex;

    // Start
    void Start () {
        showChoicesTime = -999f;
        choicesActive = false;
        oldScroll = 0;        
        currentIdx = 0;
        lastIndex = 0;

        // Initialize choice selector background
        uiDialogue = GameObject.FindGameObjectWithTag("DialogueUI");        
        canChoiceSelector = uiDialogue.transform.GetChild(2).gameObject;

        // Initialize choice text
        txtTopChoice = canChoiceSelector.transform.GetChild(0).GetComponent<Text>();
        txtSelectedChoice = canChoiceSelector.transform.GetChild(1).GetComponent<Text>();
        txtBotChoice = canChoiceSelector.transform.GetChild(2).GetComponent<Text>();

        dController = GetComponent<DialogueController>();        
    }
	
	// Update is called once per frame
	void Update () {
        float select = Input.GetAxis("Jump");        
        if (choicesActive && select > 0 && Time.time - showChoicesTime > .2f) 
        {
            choicesActive = false;
            canChoiceSelector.SetActive(false);
            Choice selectedChoice = choiceList[currentIdx];
            ActionController.performAction(selectedChoice.actionCode);
            dController.Show(selectedChoice.next);
            Debug.Log("Selected choice next: " + selectedChoice.next);           
        }

        float scroll = Input.GetAxis("Vertical");                
        if (choicesActive && oldScroll == 0)
        {
            if(scroll > 0)
            {
                lastIndex = currentIdx;
                --currentIdx;
            } 
            else if(scroll < 0)
            {
                lastIndex = currentIdx;
                ++currentIdx;
            }

            try
            {
                txtSelectedChoice.text = choiceList[currentIdx].text;
            } 
            catch(ArgumentOutOfRangeException e)
            {
                currentIdx = lastIndex;
            }            

            if(currentIdx - 1 >= 0)
            {
                txtTopChoice.text = choiceList[currentIdx - 1].text;                
            }
            else
            {
                txtTopChoice.text = "";
            }

            if(currentIdx + 1 <= choiceList.Count - 1)
            {
                txtBotChoice.text = choiceList[currentIdx + 1].text;
            }    
            else
            {
                txtBotChoice.text = "";
            }                  
        }

        oldScroll = scroll;		
	}

    // ShowChoices
    public void ShowChoices(List<Choice> choices)
    {
        choicesActive = true;
        choiceList = choices;
        showChoicesTime = Time.time;
        canChoiceSelector.SetActive(true);
        txtSelectedChoice.text = choices[0].text;
        txtBotChoice.text = choices[1].text;                
    }
}
