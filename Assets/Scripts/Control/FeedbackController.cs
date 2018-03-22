using System;
using UnityEngine.UI;
using UnityEngine;

public class FeedbackController : MonoBehaviour {    
    private float oldSkip;

    private Interaction currentInteraction;
    private PlayerMovementController playerController;
    private GameObject feedbackUI;
    private Text feedbackText, speakerText;
    private string activeFeedback, previousScene;
    private bool feedbackActive;
    private int feedbackIdx, feedbackEnd;
    private float newFeedbackTime;
    private Feedback[] currentFeedback;    

    private string currentScene, prevScene, activeScene;

    // Awake
    void Awake()
    {
        Debug.Log("Awake");    
        oldSkip = 1;
        activeFeedback = "";
        newFeedbackTime = -999f;        
        playerController = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerMovementController>();
        feedbackUI = GameObject.FindGameObjectWithTag("DialogueUI");
        feedbackText = feedbackUI.GetComponentInChildren<Text>();
        speakerText = feedbackUI.transform.GetChild(2).GetComponent<Text>();               
    }

    void Start()
    {
        Debug.Log("Start");
    }

    // Update
    void Update()
    {
        if(!feedbackActive)
        {
            activeFeedback = "";
        }

        if (activeFeedback != "" && Time.time - newFeedbackTime > .2f)
        {            
            float skip = Input.GetAxis("Jump");            
            if (skip > 0 && oldSkip == 0)
            {
                Debug.Log("Skip dialogue: " + feedbackIdx);
                try
                {
                    feedbackIdx++;
                    feedbackText.text = currentFeedback[feedbackIdx].text;
                    speakerText.text = currentFeedback[feedbackIdx].speaker;
                }
                catch (IndexOutOfRangeException e)
                {                                        
                    feedbackUI.SetActive(false);
                    feedbackActive = false;
                    playerController.setPlayerCanMove(true);
                    feedbackIdx = 0;
                    if (currentInteraction != null)
                    {
                        currentInteraction.triggerAction();
                        currentInteraction = null;
                    }
                    return;
                }
            }
            oldSkip = skip;
        }
    }

    /*
    Name: showFeedback
    Parameters: Dialogue[] dialogue
    */
    public void showFeedback(Feedback[] feedback)
    {
        showFeedback(feedback, "/", null);
    }

    /*
    Name: showFeedback
    Parameters: Dialogue[] feedback, string triggeredObject, Interaction interaction
    */
    public void showFeedback(Feedback[] feedback, string triggeredObject, Interaction interaction)
    {
        // if the dialogue should be displayed and dialogue isn't
        // associated with currently active interaction        
        if (activeFeedback == triggeredObject)
        {            
            return;
        }

        if (interaction != null)
        {
            if (interaction.delayAction)
            {
                // only need to track interaction if we have to trigger a delayed action
                currentInteraction = interaction;
            }

            if (!interaction.displayDialogue)
            {
                return;
            }
        }        
        currentFeedback = feedback;
        activeFeedback = triggeredObject;
        feedbackUI.SetActive(true);
        feedbackActive = true;
        playerController.setPlayerCanMove(false);
        feedbackText.text = feedback[0].text;
        speakerText.text = feedback[0].speaker;
        newFeedbackTime = Time.time;
    }
}
