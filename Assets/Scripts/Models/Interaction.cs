using UnityEngine.UI;
using UnityEngine;

/*
Name: Alec Reyerson
ID: 1826582
Email: reyer101@mail.chapman.edu
Course: CPSC-340-01
Assignment: Semester Project

Description: This is the parent interaction script with base functionality
*/

// Interaction
public abstract class Interaction : MonoBehaviour {
    public string preReq;
    public Color32 pulseColor;
    [HideInInspector] public bool displayDialogue;
    public bool removePreReq, delayAction;
    public float pulseSpeed, pulseStrength;    
    public Feedback[] successText, failText;

    protected bool hasInteracted, actionComplete;
    [SerializeField] protected PlayerCharacter player;
    protected FeedbackController feedbackController;
    protected Image indicator;

    private SpriteRenderer renderer;      
    private float alpha;    
    private bool dim;
    protected PopUpHider pop;
    protected GameController gameCtrl;
    protected PlayerMovementController move;
    protected int count = 0;
    // Awake
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        indicator = GameObject.FindGameObjectWithTag("PromptText").GetComponent<Image>();
        feedbackController = GameObject.FindGameObjectWithTag("FeedbackController").GetComponent<FeedbackController>();
        renderer = GetComponent<SpriteRenderer>();
        hasInteracted = false;
        actionComplete = false;
        displayDialogue = successText.Length > 0 || failText.Length > 0;              
        alpha = 255;        
        pulseColor.a = 255;
        pop = GameObject.Find("PopUp").GetComponent<PopUpHider>();
        pop.Hide();
        gameCtrl = GameObject.Find("GameController").GetComponent<GameController>();
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
    }      

    // FixedUpdate
    void FixedUpdate()
    {
        if(!hasInteracted)
        {
            if (alpha >= 254)
            {
                dim = true;

            }
            else if (alpha <= (255 - pulseStrength * 10))
            {
                dim = false;

            }

            if (dim)
            {
                alpha = alpha - Time.deltaTime * (10f * pulseSpeed);
            }
            else
            {
                alpha = alpha + Time.deltaTime * (10f * pulseSpeed);
            }

            pulseColor.a = (byte)alpha;
            renderer.color = pulseColor;
        }
        else
        {
            renderer.color = Color.white;
            indicator.enabled = false;
        }                
    }

    // hasPreReq
    public bool hasPreReq()
    {
        if(preReq == "")
        {            
            return true;
        }

        foreach(Item item in player.inventory)
        {            
            if(item.getName() == preReq)
            {
                return true;
            }
        }
        return false;
    }
    
    // isSuccess
    public bool isSuccess()
    {
        return hasInteracted;
    }

    // OnTriggerEnter
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !hasInteracted)
        {
            Debug.Log("Player entered");
            indicator.enabled = true;
        }
    }

    // OnTriggerExit
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player exited");
            indicator.enabled = false;
        }
    }  
    
    // OnDestroy
    void OnDestroy()
    {
        if(indicator != null)
        {
            indicator.enabled = false;
        }        
    }  

    // interact
    public abstract void interact();

    // triggerAction
    public abstract void triggerAction();
}
