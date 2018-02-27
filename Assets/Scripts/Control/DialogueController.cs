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
    public int initialDialogueIndex;

    // Start
    void Start () {
        canSkip = false;
        dialogueActive = false;

        // Initialize all parent GameObjects for hiding and showing dialogue UI
        uiDialogue = GameObject.FindGameObjectWithTag("DialogueUI");
        Debug.Log("Assign uiDialogue");
        canDialogue = uiDialogue.transform.GetChild(1).gameObject;        

        // Initialize dialogue text
        txtSpeaker = canDialogue.transform.GetChild(1).GetComponent<Text>();
        txtDialogue = canDialogue.transform.GetChild(0).GetComponent<Text>();        

        choiceSelector = GetComponent<ChoiceSelector>();

        Choice choice1 = new Choice(13, "Study Mystic Astronomy", Constants.Action.ADD_INTUITION);
        Choice choice2 = new Choice(15, "Study Magic", Constants.Action.ADD_MAGIC);
        Choice choice3 = new Choice(18, "Talk");
        Choice choice4 = new Choice(24, "Exit");
        List<Choice> choices1 = new List<Choice>() { choice1, choice2, choice3, choice4 };

        choice1 = new Choice(26, "Buy");
        choice2 = new Choice(27 , "Talk");
        choice3 = new Choice(34, "Exit");
        List<Choice> choices2 = new List<Choice>() { choice1, choice2, choice3};

        choice1 = new Choice(38, "Try cycling", Constants.Action.ADD_STAMINA);
        choice2 = new Choice(40, "Try boxing", Constants.Action.ADD_STRENGTH);
        choice3 = new Choice(42, "Talk");
        choice4 = new Choice(48, "Exit");
        List<Choice> choices3 = new List<Choice>() { choice1, choice2, choice3, choice4 };

        choice1 = new Choice(53, "Buy");
        choice2 = new Choice(54, "Talk");
        choice3 = new Choice(60, "Exit");
        List<Choice> choices4 = new List<Choice>() { choice1, choice2, choice3};

        choice1 = new Choice(64, "Study first aid", Constants.Action.ADD_DEFENSE);
        choice2 = new Choice(68, "Study longevity", Constants.Action.ADD_HEALTH);
        choice3 = new Choice(73, "Talk");
        choice4 = new Choice(80, "Exit");
        List<Choice> choices5 = new List<Choice>() { choice1, choice2, choice3, choice4};

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

        DialogueComponent d25 = new DialogueComponent(25, -1, "Lulu", "Welcome back, Sunny! What could I help you with?", new ChoiceWrapper(choices2));

        DialogueComponent d26 = new DialogueComponent(26, 0, "Lulu", "Of course. Let me show you what we've got in stock.", Constants.Action.OPEN_BOOKSTORE);
        DialogueComponent d27 = new DialogueComponent(27, 28, "Lulu", "It's so brave of you to go out into the woods, dear. I must urge you to be careful.");
        DialogueComponent d28 = new DialogueComponent(28, 29, "Sunny", "No need to worry about me, Lu. I'm pretty sturdy. ");
        DialogueComponent d29 = new DialogueComponent(29, 30, "Lulu", "I know. As small as you are, you've always been a fighter.");
        DialogueComponent d30 = new DialogueComponent(30, 31, "Sunny", "Pff. Here I am, saving the whole town, and you insult me.");
        DialogueComponent d31 = new DialogueComponent(31, 32, "Lulu", "I'm just playing around with you, Sunny. But I hope you know that you can back out at any time. ");
        DialogueComponent d32 = new DialogueComponent(32, 33, "Lulu", "None of us want you to risk your life out there, and you're welcome to spend the night with me if you need to get patched up.");
        DialogueComponent d33 = new DialogueComponent(33, -1, "Sunny", "Thanks, Lu.", new ChoiceWrapper(choices2));
        DialogueComponent d34 = new DialogueComponent(34, 0, "Lulu", "Take care, you hear?", Constants.Action.LOAD_PREV_SCENE);

        DialogueComponent d35 = new DialogueComponent(35, 36, "Hal", "Sunny? Is that you?");
        DialogueComponent d36 = new DialogueComponent(36, 37, "Sunny", "Yeah... I thought I should get back in shape if I'm gonna rough up some monsters.");
        DialogueComponent d37 = new DialogueComponent(37, -1, "Hal", "Fantastic! I'd be happy to train you. What do you want to work on?", new ChoiceWrapper(choices3));
        DialogueComponent d38 = new DialogueComponent(38, 39, "", "While you refuse to run, you are willing to mount the stationary bike. Under Hal's guidance, you work up quite a sweat.");
        DialogueComponent d39 = new DialogueComponent(39, 0, "", "You gain 1 point in Stamina.");
        DialogueComponent d40 = new DialogueComponent(40, 41, "", "You pull on some boxing clubs and start to go toe to toe with Hal. You can't beat him... yet. ");
        DialogueComponent d41 = new DialogueComponent(41, 0, "", "You gain 1 point in Strength.");
        DialogueComponent d42 = new DialogueComponent(42, 43, "Hal", "What's up?");
        DialogueComponent d43 = new DialogueComponent(43, 44, "Sunny", "I was just wondering why you aren't coming out into the woods with me. What's holding you back?");
        DialogueComponent d44 = new DialogueComponent(44, 45, "Hal", "I just don't know what we're up against. My family's here and I feel that I can protect them better if I stay with them.");
        DialogueComponent d45 = new DialogueComponent(45, 46, "Sunny", "I understand. It's definitely a risk, and I wouldn't ask you to leave your family behind.");
        DialogueComponent d46 = new DialogueComponent(46, 47, "Hal", "I believe in you, Sunny. I bet you can take those monsters out without a scratch.");
        DialogueComponent d47 = new DialogueComponent(47, 0, "Sunny", "We'll see about that.");
        DialogueComponent d48 = new DialogueComponent(48, 0, "Hal", "See you, Sunny!");

        DialogueComponent d49 = new DialogueComponent(49, 50, "Greyson", "Hey Sunny!");
        DialogueComponent d50 = new DialogueComponent(50, 51, "Sunny", "Hey Greyson! Are you working the store alone today?");
        DialogueComponent d51 = new DialogueComponent(51, 52, "Greyson", "Yup. Trevor and Judd are out getting some things for the store, but I'm doing just fine on my own.");
        DialogueComponent d52 = new DialogueComponent(52, -1, "Greyson", "Anyway, what can I get you?", new ChoiceWrapper(choices4));
        DialogueComponent d53 = new DialogueComponent(53, 0, "Greyson", "Take a look! We've got some really good deals in today.", Constants.Action.OPEN_STORE);
        DialogueComponent d54 = new DialogueComponent(54, 55, "Greyson", "How's it been getting ready for tonight?");
        DialogueComponent d55 = new DialogueComponent(55, 56, "Sunny", "I don't know yet, really. I feel like I should be doing lots of things, but there's only so much time in a day.");
        DialogueComponent d56 = new DialogueComponent(56, 57, "Greyson", "You're kinda going in blind on this one. I'm sure after tonight is over, you'll have a much better idea of what you're doing.");
        DialogueComponent d57 = new DialogueComponent(57, 58, "Sunny", "I hope so. Everyone here's counting on me. I want to take care of this as soon as I can.");
        DialogueComponent d58 = new DialogueComponent(58, 59, "Greyson", "Don't worry so much. You're going to be amazing.");
        DialogueComponent d59 = new DialogueComponent(59, -1, "Sunny", "Thanks, Greyson. ", new ChoiceWrapper(choices4));
        DialogueComponent d60 = new DialogueComponent(60, 0, "Greyson", "Come around any time!", Constants.Action.LOAD_PREV_SCENE);

        DialogueComponent d61 = new DialogueComponent(61, 62, "Ivy", "Oh, hello Sunny. Are you feeling alright?");
        DialogueComponent d62 = new DialogueComponent(62, 63, "Sunny", "I'm doing just fine, doc. I was actually wondering if you could help me with something else.");
        DialogueComponent d63 = new DialogueComponent(63, -1, "Ivy", "What can I help you with?", new ChoiceWrapper(choices5));
        DialogueComponent d64 = new DialogueComponent(64, 65, "Sunny", "Anything you can teach me for patching myself up if those things get a good swipe at me? ");
        DialogueComponent d65 = new DialogueComponent(65, 66, "Ivy", "I could definitely help you with that. How about you come back with me and I'll go through it with you.");
        DialogueComponent d66 = new DialogueComponent(66, 67, "", "Ivy walks you through multiple ways of blocking against severe damage.");
        DialogueComponent d67 = new DialogueComponent(67, 0, "", "You gain 1 point in Defense.");
        DialogueComponent d68 = new DialogueComponent(68, 69, "Sunny", "Is there anything I could do to be... more resilient, I guess? ");
        DialogueComponent d69 = new DialogueComponent(69, 70, "Ivy", "Well, there are a lot of long term habits you can get into to be healthier, but... I do know of some experimental things we can try.");
        DialogueComponent d70 = new DialogueComponent(70, 71, "Sunny", "Sounds perfect! Lay it on me.");
        DialogueComponent d71 = new DialogueComponent(71, 72, "", "Ivy brings out multiple odd-looking plants from the back of the clinic. You wonder if this was such a good idea when she has you eat them.");
        DialogueComponent d72 = new DialogueComponent(72, 0, "", "You gain 1 point in Health.");
        DialogueComponent d73 = new DialogueComponent(73, 74, "Ivy", "Today's a big day for you, isn't it?");
        DialogueComponent d74 = new DialogueComponent(74, 75, "Sunny", "It seems to be a big day for all of us.");
        DialogueComponent d75 = new DialogueComponent(75, 76, "Ivy", "I suppose you're right. I hope we're not pressuring you too much.");
        DialogueComponent d76 = new DialogueComponent(76, 77, "Sunny", "It's fine. I get that everyone's pretty shaken up about all this.");
        DialogueComponent d77 = new DialogueComponent(77, 78, "Ivy", "Our town's pretty quiet for the most part. Remember when a tree fell over on one of the electrical lines and it was like an earthquake had hit?");
        DialogueComponent d78 = new DialogueComponent(78, 79, "Sunny", "Heh, yeah. We're not equipped to deal with the unusual, are we?");
        DialogueComponent d79 = new DialogueComponent(79, 0, "Ivy", "I think you'll be, Sunny. ");
        DialogueComponent d80 = new DialogueComponent(80, 81, "Ivy", "Try not to land yourself in here, Sunny.");
        DialogueComponent d81 = new DialogueComponent(81, 0, "Sunny", "I make no promises.");

        DialogueComponent d82 = new DialogueComponent(82, 83, "Apollo", "Hey. I was wondering if I'd get to catch you before you left.");
        DialogueComponent d83 = new DialogueComponent(83, 84, "Sunny", "You did just in time.");
        DialogueComponent d84 = new DialogueComponent(84, 85, "Apollo", "I imagine you're going to be busy for a while now.");
        DialogueComponent d85 = new DialogueComponent(85, 86, "Sunny", "I'm trying to be. The sooner we get to the bottom of this, the better.");
        DialogueComponent d86 = new DialogueComponent(86, 87, "Apollo", "Yeah, of course...");
        DialogueComponent d87 = new DialogueComponent(87, 88, "Apollo", "Sunny, you know you don't have to do this, right?");
        DialogueComponent d88 = new DialogueComponent(88, 89, "Sunny", "Aww, you worried about me, Polly?");
        DialogueComponent d89 = new DialogueComponent(89, 90, "Apollo", "Yeah. I am.");
        DialogueComponent d90 = new DialogueComponent(90, 91, "Sunny", "I'm gonna be fine. ");
        DialogueComponent d91 = new DialogueComponent(91, 92, "Apollo", "Please tell me you're not just doing this to impress Gemma.");
        DialogueComponent d92 = new DialogueComponent(92, 93, "Sunny", "... W-what? No, of course not. I'm doing this for everyone, Apollo.");
        DialogueComponent d93 = new DialogueComponent(93, 94, "Apollo", "Sunny...");
        DialogueComponent d94 = new DialogueComponent(94, 95, "Sunny", "I swear. I'm not just doing it for that.");
        DialogueComponent d95 = new DialogueComponent(95, 96, "Apollo", "Alright. I'm glad to hear that at least. You know I like Gemma too - not like you do, of course - but her whole star thing kinda freaks me out.");
        DialogueComponent d96 = new DialogueComponent(96, 97, "Sunny", "She knows more about this than the rest of us.");
        DialogueComponent d97 = new DialogueComponent(97, 98, "Apollo", "Exactly! Isn't that... kind of fishy? I mean we didn't take her seriously for so long and now-");
        DialogueComponent d98 = new DialogueComponent(98, 99, "Sunny", "Gemma's not the cause of this. I want to know what's going on just as much as you do, but we shouldn't throw each other under the bus.");
        DialogueComponent d99 = new DialogueComponent(99, 100, "Apollo", "... Okay, you're right. I'm sorry for sounding like a jerk. ");
        DialogueComponent d100 = new DialogueComponent(100, 0, "Sunny", "Hey, everyone around here's pretty stressed. Don't worry about it.");



        dialogue = new Dictionary<int, DialogueComponent>() { { 1, d1 }, { 2, d2 }, { 3, d3 }, { 7, d7 }, { 8, d8 }, { 9, d9 }, { 10, d10 }, { 11, d11 }
            , {12, d12 }, {13, d13 }, {14, d14 }, {15, d15 }, {16, d16 }, {17, d17 }, {18, d18 }, {19, d19 }, {20, d20 }, {21, d21 }, {22, d22 }, {23, d23 }
            , {24, d24 } , {25, d25 }, {26, d26 }, {27, d27 }, {28, d28 }, {29, d29 }, {30, d30 }, {31, d31 }, {32, d32 }, {33, d33 }, {34, d34 }, {35, d35 }
            , {36, d36 }, {37, d37 }, {38, d38 }, {39, d39 }, {40, d40 }, {41, d41 }, {42, d42 }, {43, d43 }, {44, d44 }, {45, d45 }, {46, d46 }, {47, d47 }
            , {48, d48 }, {49, d49 }, {50, d50 }, {51, d51 }, {52, d52 }, {53, d53 }, {54, d54 }, {55, d55 }, {56, d56 }, {57, d57 }, {58, d58 }, {59, d59 }
            , {60, d60 }, {61, d61 }, {62, d62 }, {63, d63 }, {64, d64 }, {65, d65 }, {66, d66 }, {67, d67 }, {68, d68 }, {69, d69 }, {70, d70 }, {71, d71 }
            , {72, d72 }, {73, d73 }, {74, d74 }, {75, d75 }, {76, d76 }, {77, d77 }, {78, d78 }, {79, d79 }, {80, d80 }, {81, d81 }, {82, d82 }, {83, d83 }
            , {84, d84 }, {85, d85 }, {86, d86 }, {87, d87 }, {88, d88 }, {89, d89 }, {90, d90 }, {91, d91 }, {92, d92 }, {93, d93 }, {94, d94 }, {95, d95 }
            , {96, d96 }, {97, d97 }, {98, d98 }, {99, d99 }, {100, d100 } };
        

        List<DialogueComponent> dList = new List<DialogueComponent>();

        foreach(KeyValuePair<int, DialogueComponent> entry in dialogue)
        {
            dList.Add(entry.Value);
        }

        //DialogueUtils.storeDialogue(new DialogueWrapper(dList), "Day1");


        lastSkipTime = -999f;  // Used to prevent skipping of dialogue following choice 
        lastShowChoiceTime = -999f;
                
        //dialogue = DialogueUtils.initDialogueForScene(inputFile);       

        //Show(63);  
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
        Debug.Log("Next: " + next);
        if(next == 0)
        {
            Debug.Log("Next is 0, return");
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
            canDialogue.SetActive(false);            
            choiceSelector.ShowChoices(currentDialogue.choiceWrapper.choices);
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
    public void Show(int key)
    {
        if(key < 1)
        {
            uiDialogue.SetActive(false);
            dialogueActive = false;
            return;
        }

        canSkip = true;
        dialogueActive = true;
        uiDialogue.SetActive(true);  // activate dialogue UI components 
        canDialogue.SetActive(true);          
        lastSkipTime = Time.time;
        currentDialogue = dialogue[key];
        txtDialogue.text = currentDialogue.text;
        txtSpeaker.text = currentDialogue.speaker;
    }    
}
