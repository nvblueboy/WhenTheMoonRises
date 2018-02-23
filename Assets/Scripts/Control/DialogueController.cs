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

        /*Choice choice1 = new Choice(0, "Shop", Constants.Action.OPEN_STORE);
        Choice choice2 = new Choice(4, "Talk");
        Choice choice3 = new Choice(5, "Train", Constants.Action.ADD_STRENGTH);
        Choice choice4 = new Choice(0, "Exit");

        List<Choice> choices = new List<Choice>() { choice1, choice2, choice3, choice4 };
        ChoiceWrapper choiceWrapper = new ChoiceWrapper();
        choiceWrapper.choices = choices;

        DialogueComponent d1 = new DialogueComponent(1, 3, "Shopkeeper", "Hello, and welcome to my shop!");        
        DialogueComponent d3 = new DialogueComponent(3, -1, "Shopkeeper", "How can I help you today?", choiceWrapper);
        DialogueComponent d4 = new DialogueComponent(4, 0, "Sunny", "So what can you tell me about Raven's Ridge?");
        DialogueComponent d5 = new DialogueComponent(5, 0, "", "One point added to your Strength!");
        dialogue = new Dictionary<int, DialogueComponent>() { { 1, d1 }, { 3, d3 }, { 4, d4 }, { 5, d5 } };
        List<DialogueComponent> dList = new List<DialogueComponent>() { d1, d3, d4, d5 };

        DialogueWrapper dialogueWrapper = new DialogueWrapper(dList);
        DialogueUtils.storeDialogue(dialogueWrapper, "NarrativeTest");        
        string test= JsonUtility.ToJson(dialogueWrapper);
        Debug.Log(test);*/

        Choice choice1 = new Choice(13, "Study Mystic Astronomy", Constants.Action.ADD_INTUITION);
        Choice choice2 = new Choice(15, "Study Magic", Constants.Action.ADD_MAGIC);
        Choice choice3 = new Choice(18, "Talk");
        Choice choice4 = new Choice(24, "Exit");
        List<Choice> choices1 = new List<Choice>() { choice1, choice2, choice3, choice4 };        

        DialogueComponent d1 = new DialogueComponent(1, 2, "Sunny", "Guess there's no time to waste. I've got a lot I should do before heading out to the woods.");
        DialogueComponent d2 = new DialogueComponent(2, 3, "", "During the day, you can build your skills by visiting different locations. Building a skill costs 1 action.");
        DialogueComponent d3 = new DialogueComponent(3, 0, "", "Make sure to watch the sun meter on the right. Once Sunny has taken six actions, the day will end.");

        DialogueComponent d7 = new DialogueComponent(7, 8, "", "It's locked");
        DialogueComponent d8 = new DialogueComponent(8, 0, "Sunny", "It's been pretty empty in there for a while. I wonder how the bells still go off.");

        DialogueComponent d9 = new DialogueComponent(9, 10, "Gemma", "Sunny! I'm glad you came by. I think some of my books could tell us a lot about the shards you need to collect, and maybe helpful for dealing with those creatures in the woods.");
        DialogueComponent d10 = new DialogueComponent(10, 11, "Gemma", "It's unfortunate that it took this happening for people to believe me. Besides you. You've always been so nice to me, Sunny.");
        DialogueComponent d11 = new DialogueComponent(11, 12, "Sunny", "They told me my hobbies were pretty far-fetched too, but we're proving them wrong now, huh? You knew about magic and stars, and I was dead on about how much a sword would come in handy.");
        DialogueComponent d12 = new DialogueComponent(12, -1, "Gemma", "Hehe, you're right. So, what do you think we should do?", new ChoiceWrapper(choices1));
        DialogueComponent d13 = new DialogueComponent(13, 14, "", "Gemma plops a dusty tome in front of you on the table. You two skim the pages, gathering information on the special properties of stars.");
        DialogueComponent d14 = new DialogueComponent(14, 0, "", "You gain 1 point in Intutition. ");
        DialogueComponent d15 = new DialogueComponent(15, 16, "Gemma", "O-oh? You really want to study magic with me? ");
        DialogueComponent d16 = new DialogueComponent(16, 17, "", "Though taken by surprise, Gemma begins to teach you the basics of magic.");
        DialogueComponent d17 = new DialogueComponent(17, 0, "", "You gain 1 point in Magic.");
        DialogueComponent d18 = new DialogueComponent(18, 19, "Gemma", "How are you holding up? Do you think you can really handle these... things?");
        DialogueComponent d19 = new DialogueComponent(19, 20, "Sunny", "Yeah, I'm sure I can handle it.");
        DialogueComponent d20 = new DialogueComponent(20, 21, "Gemma", "We still don't know much about them. I wouldn't let your guard down.");
        DialogueComponent d21 = new DialogueComponent(21, 22, "Sunny", "I'll be okay. I promise. I'm not gonna let anyone in town get hurt by these things.");
        DialogueComponent d22 = new DialogueComponent(22, 23, "Gemma", "I wish I could go with you, but I'd just drag you down.");
        DialogueComponent d23 = new DialogueComponent(23, 0, "Sunny", "No, no, you're doing great here. Your research is going to help me a lot.");
        DialogueComponent d24 = new DialogueComponent(24, 0, "Gemma", "Good luck, Sunny!");

        DialogueComponent d25 = new DialogueComponent(25, 26, "Lulu", "Welcome back, Sunny! What could I help you with?");



        dialogue = new Dictionary<int, DialogueComponent>() { { 1, d1 }, { 2, d2 }, { 3, d3 }, { 7, d7 }, { 8, d8 }, { 9, d9 }, { 10, d10 }, { 11, d11 }
            , {12, d12 }, {13, d13 }, {14, d14 }, {15, d15 }, {16, d16 }, {17, d17 }, {18, d18 }, {19, d19 }, {20, d20 }, {21, d21 }, {22, d22 }, {23, d23 }
            , {24, d24 } };
        //dialogue = DialogueUtils.initDialogueForCurrentScene();

        Show(9);             
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
