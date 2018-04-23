using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
Name: Andrew Krager
ID: 1879130
Email: krage100@mail.chapman.edu
Course: CPSC-440-01
Assignment: Semester Project

Description: This is a script controlling the in game shop
*/
public class PopUpHider : MonoBehaviour
{
    public bool isActive = true;
    public bool didWork = false;
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;
    public GameObject arrow4;
    public GameObject arrow5;
    public GameObject arrow6;
    public GameObject arrow7;
    public Text item1;
    public Text item2;
    public Text item3;
    public Text item4;
    public Text item5;
    public Text item6;
    public Text item7;
    public int invSize;
    private bool isFirstItem;
    private List<WorldInteraction> worldItems;
    private PlayerCharacter player;
    private PlayerMovementController move;
    public CanvasGroup canvasGroup;

    // Use this for initialization
    void Awake()
    {
        arrow1 = GameObject.Find("arrow1");
        arrow2 = GameObject.Find("arrow2");
        arrow3 = GameObject.Find("arrow3");
        arrow4 = GameObject.Find("arrow4");
        arrow5 = GameObject.Find("arrow5");
        arrow6 = GameObject.Find("arrow6");
        arrow7 = GameObject.Find("arrow7");
        arrow1.SetActive(true);
        arrow2.SetActive(false);
        arrow3.SetActive(false);
        arrow4.SetActive(false);
        arrow5.SetActive(false);
        arrow6.SetActive(false);
        arrow7.SetActive(false);
        item1 = GameObject.Find("item1").GetComponent<Text>();
        item2 = GameObject.Find("item2").GetComponent<Text>();
        item3 = GameObject.Find("item3").GetComponent<Text>();
        item4 = GameObject.Find("item4").GetComponent<Text>();
        item5 = GameObject.Find("item5").GetComponent<Text>();
        item6 = GameObject.Find("item6").GetComponent<Text>();
        item7 = GameObject.Find("item7").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        move = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementController>();
        worldItems = new List<WorldInteraction>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            invSize = player.inventory.Count;
            for (int i = 0; i < invSize; ++i)
            {
                if (i == 0)
                    item1.text = player.inventory[i].getName();
                else if (i == 1)
                    item2.text = player.inventory[i].getName();
                else if (i == 2)
                    item3.text = player.inventory[i].getName();
                else if (i == 3)
                    item4.text = player.inventory[i].getName();
                else if (i == 4)
                    item5.text = player.inventory[i].getName();
                else if (i == 5)
                    item6.text = player.inventory[i].getName();
                else if (i == 6)
                    item7.text = player.inventory[i].getName();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (arrow1.activeInHierarchy)
                {
                    arrow1.SetActive(false);
                    arrow2.SetActive(true);
                }
                else if (arrow2.activeInHierarchy)
                {
                    arrow2.SetActive(false);
                    arrow3.SetActive(true);
                }
                else if (arrow3.activeInHierarchy)
                {
                    arrow3.SetActive(false);
                    arrow4.SetActive(true);
                }
                else if (arrow4.activeInHierarchy)
                {
                    arrow4.SetActive(false);
                    arrow5.SetActive(true);
                }
                else if (arrow5.activeInHierarchy)
                {
                    arrow5.SetActive(false);
                    arrow6.SetActive(true);

                }
                else if (arrow6.activeInHierarchy)
                {
                    arrow6.SetActive(false);
                    arrow7.SetActive(true);
                }
                else if (arrow7.activeInHierarchy)
                {
                    arrow7.SetActive(false);
                    arrow1.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (arrow1.activeInHierarchy)
                {
                    arrow1.SetActive(false);
                    arrow7.SetActive(true);
                }
                else if (arrow2.activeInHierarchy)
                {
                    arrow2.SetActive(false);
                    arrow1.SetActive(true);
                }
                else if (arrow3.activeInHierarchy)
                {
                    arrow3.SetActive(false);
                    arrow2.SetActive(true);
                }
                else if (arrow4.activeInHierarchy)
                {
                    arrow4.SetActive(false);
                    arrow3.SetActive(true);
                }
                else if (arrow5.activeInHierarchy)
                {
                    arrow5.SetActive(false);
                    arrow4.SetActive(true);

                }
                else if (arrow6.activeInHierarchy)
                {
                    arrow6.SetActive(false);
                    arrow5.SetActive(true);
                }
                else if (arrow7.activeInHierarchy)
                {
                    arrow7.SetActive(false);
                    arrow6.SetActive(true);
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (arrow1.activeInHierarchy)
                {
                                       
                    if (!isFirstItem)
                    {
                        int actionsSucceeded = 0;
                        int actionCount = worldItems.Count;
                        foreach (WorldInteraction worldItem in worldItems)
                        {
                            if (item1.text == worldItem.preReq)
                            {
                                didWork = true;
                                worldItem.victText();
                                worldItem.didAction(true);                                
                                ++actionsSucceeded;
                                if (!(worldItem.delayAction))
                                {
                                    worldItem.triggerAction();                                                                      
                                }

                                Hide();
                            }
                            else
                            {                                
                                worldItem.loseText();
                            }
                        }

                        if (actionsSucceeded == actionCount)
                        {
                            worldItems.Clear();
                        }
                    }                   
                                        
                    isFirstItem = false;
                }
                else if (arrow2.activeInHierarchy)
                {
                    int actionsSucceeded = 0;
                    int actionCount = worldItems.Count;
                    foreach (WorldInteraction worldItem in worldItems)
                    {
                        isFirstItem = false;
                        if (item2.text == worldItem.preReq)
                        {
                            arrow1.SetActive(true);
                            arrow2.SetActive(false);
                            didWork = true;
                            worldItem.victText();
                            Debug.Log("Item 2 vict text");
                            worldItem.didAction(true);
                            ++actionsSucceeded;
                            if (!(worldItem.delayAction))
                            {
                                worldItem.triggerAction();                                
                            }                           

                            Hide();
                        }
                        else
                        {                            
                            worldItem.loseText();
                        }
                    }

                    if(actionsSucceeded == actionCount)
                    {
                        worldItems.Clear();
                    }                    
                }
                else if (arrow3.activeInHierarchy)
                {
                    int actionsSucceeded = 0;
                    int actionCount = worldItems.Count;
                    foreach (WorldInteraction worldItem in worldItems)
                    {
                        isFirstItem = false;
                        if (item3.text == worldItem.preReq)
                        {
                            arrow1.SetActive(true);
                            arrow3.SetActive(false);
                            didWork = true;
                            worldItem.victText();
                            worldItem.didAction(true);
                            ++actionsSucceeded;
                            if (!(worldItem.delayAction))
                            {
                                worldItem.triggerAction();
                            }

                            Hide();
                        }
                        else
                        {                            
                            worldItem.loseText();
                        }
                    }

                    if (actionsSucceeded == actionCount)
                    {
                        worldItems.Clear();
                    }
                }
                else if (arrow4.activeInHierarchy)
                {
                    int actionsSucceeded = 0;
                    int actionCount = worldItems.Count;
                    foreach (WorldInteraction worldItem in worldItems)
                    {
                        isFirstItem = false;
                        if (item4.text == worldItem.preReq)
                        {
                            arrow1.SetActive(true);
                            arrow4.SetActive(false);
                            didWork = true;
                            worldItem.victText();
                            worldItem.didAction(true);
                            ++actionsSucceeded;
                            if (!(worldItem.delayAction))
                            {
                                worldItem.triggerAction();
                            }

                            Hide();
                        }
                        else
                        {                            
                            worldItem.loseText();
                        }
                    }

                    if (actionsSucceeded == actionCount)
                    {
                        worldItems.Clear();
                    }
                }
                else if (arrow5.activeInHierarchy)
                {
                    int actionsSucceeded = 0;
                    int actionCount = worldItems.Count;
                    foreach (WorldInteraction worldItem in worldItems)
                    {
                        isFirstItem = false;
                        if (item5.text == worldItem.preReq)
                        {
                            arrow1.SetActive(true);
                            arrow5.SetActive(false);
                            didWork = true;
                            worldItem.victText();
                            worldItem.didAction(true);
                            ++actionsSucceeded;
                            if (!(worldItem.delayAction))
                            {
                                worldItem.triggerAction();
                            }

                            Hide();
                        }
                        else
                        {                            
                            worldItem.loseText();
                        }
                    }

                    if (actionsSucceeded == actionCount)
                    {
                        worldItems.Clear();
                    }
                }
                else if (arrow6.activeInHierarchy)
                {
                    int actionsSucceeded = 0;
                    int actionCount = worldItems.Count;
                    foreach (WorldInteraction worldItem in worldItems)
                    {
                        isFirstItem = false;
                        if (item6.text == worldItem.preReq)
                        {
                            arrow1.SetActive(true);
                            arrow6.SetActive(false);
                            didWork = true;
                            worldItem.victText();
                            worldItem.didAction(true);
                            ++actionsSucceeded;
                            if (!(worldItem.delayAction))
                            {
                                worldItem.triggerAction();
                            }

                            Hide();
                        }
                        else
                        {                                                       
                            worldItem.loseText();
                        }
                    }

                    if (actionsSucceeded == actionCount)
                    {
                        worldItems.Clear();
                    }
                }
                else if (arrow7.activeInHierarchy)
                {
                    int actionsSucceeded = 0;
                    int actionCount = worldItems.Count;
                    foreach (WorldInteraction worldItem in worldItems)
                    {
                        isFirstItem = false;
                        if (item7.text == worldItem.preReq)
                        {
                            arrow1.SetActive(true);
                            arrow7.SetActive(false);
                            didWork = true;
                            worldItem.victText();
                            worldItem.didAction(true);
                            ++actionsSucceeded;
                            if (!(worldItem.delayAction))
                            {
                                worldItem.triggerAction();
                            }

                            Hide();
                        }
                        else
                        {                            
                            worldItem.loseText();
                        }
                    }

                    if (actionsSucceeded == actionCount)
                    {
                        worldItems.Clear();
                    }
                }
            }

        }
    }

    public void Hide()
    {
        isFirstItem = true;
        isActive = false;
        canvasGroup.alpha = 0f; //this makes everything transparent
        canvasGroup.blocksRaycasts = false; //this prevents the UI element to receive input events
    }

    public void Show(WorldInteraction item)
    {
        Debug.Log(item.ToString());
        worldItems.Add(item);
        isActive = true;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        this.Update();
        move.setPlayerCanMove(false);
    }
}
