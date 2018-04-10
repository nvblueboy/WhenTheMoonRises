﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Name: Andrew Krager
ID: 1879130
Email: krage100@mail.chapman.edu
Course: CPSC-440-01
Assignment: Semester Project

Description: This is a script controlling the in game shop
*/
public class ItemShop : MonoBehaviour
{
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;
    public GameObject arrow4;
    public GameObject arrow5;
    public GameObject arrow6;
    public GameObject arrow7;
    public GameObject arrow8;
    public GameObject arrow9;
    public GameObject arrow10;
    public GameObject arrow11;
    public GameObject arrow12;
    public GameObject arrow13;
    int placeholder = 0;
    //ArrayList arrows = new ArrayList();
    public Button smallHP;
    public Button medHP;
    public Button smMedHP;
    public Button largeHP;
    public Button smallST;
    public Button smMedST;
    public Button medST;
    public Button largeST;
    public Button fireCrack;
    public Button watch;
    public Button pendant;
    public Button bundle;
    public Button exit;
    public Text shopText;
    public Text coinText;
    public Text itemInfo;    
    public int coins;
    public string item;   
    
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
        arrow8 = GameObject.Find("arrow8");
        arrow9 = GameObject.Find("arrow9");
        arrow10 = GameObject.Find("arrow10");
        arrow11 = GameObject.Find("arrow11");
        arrow12 = GameObject.Find("arrow12");
        arrow13 = GameObject.Find("arrow13");
        coins = GameController.player.getCoins();
        coinText.text = (GameController.player.getCoins()).ToString() + " C";
        //arrows.Add(arrow1);
        //arrows.Add(arrow2);
        //arrows.Add(arrow3);
    }
    void Start()
    {
        arrow1.SetActive(true);
        arrow2.SetActive(false);
        arrow3.SetActive(false);
        arrow4.SetActive(false);
        arrow5.SetActive(false);
        arrow6.SetActive(false);
        arrow7.SetActive(false);
        arrow8.SetActive(false);
        arrow9.SetActive(false);
        arrow10.SetActive(false);
        arrow11.SetActive(false);
        arrow12.SetActive(false);
        arrow13.SetActive(false);
        itemInfo.text = "A small snack that replenishes 5 HP ";
    }
    // Update is called once per frame
    void Update()
    {
        GameController.player.coins = coins;
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (arrow1.activeInHierarchy)
            {
                itemInfo.text = "A meal-on-the-go that replenishes 10 HP ";
                arrow1.SetActive(false);
                arrow2.SetActive(true);
            }
            else if (arrow2.activeInHierarchy)
            {
                itemInfo.text = "A meal on-the-go that replenishes 20 HP ";
                arrow2.SetActive(false);
                arrow3.SetActive(true);
            }
            else if (arrow3.activeInHierarchy)
            {
                itemInfo.text = "A large meal that replenishes 50 HP ";
                arrow3.SetActive(false);
                arrow4.SetActive(true);
            }
            else if (arrow4.activeInHierarchy)
            {
                itemInfo.text = "A small drink that replenishes 5 STA ";
                arrow4.SetActive(false);
                arrow5.SetActive(true);
            }
            else if (arrow5.activeInHierarchy)
            {
                itemInfo.text = "A small drink that replenishes 10 STA ";
                arrow5.SetActive(false);
                arrow6.SetActive(true);

            }
            else if (arrow6.activeInHierarchy)
            {
                itemInfo.text = "A medium drink that replenishes 20 STA ";
                arrow6.SetActive(false);
                arrow7.SetActive(true);
            }
            else if (arrow7.activeInHierarchy)
            {
                itemInfo.text = "A large drink that replenishes 50 STA ";
                arrow7.SetActive(false);
                arrow8.SetActive(true);
            }
            else if (arrow8.activeInHierarchy)
            {
                itemInfo.text = "An explosive that can cause decent damage and even burn things close by ";
                arrow8.SetActive(false);
                arrow9.SetActive(true);
            }
            else if (arrow9.activeInHierarchy)
            {
                itemInfo.text = "An explosive that can cause significant damage to things close by ";
                arrow9.SetActive(false);
                arrow10.SetActive(true);
            }
            else if (arrow10.activeInHierarchy)
            {
                itemInfo.text = "An item that allows Sunny to perform more actions during the day ";
                arrow10.SetActive(false);
                arrow11.SetActive(true);
            }
            else if (arrow11.activeInHierarchy)
            {
                itemInfo.text = "A mysterious item that brings Sunny back to life if her HP hits 0. ";
                arrow11.SetActive(false);
                arrow12.SetActive(true);
            }
            else if (arrow12.activeInHierarchy)
            {
                itemInfo.text = " ";
                arrow12.SetActive(false);
                arrow13.SetActive(true);

            }
            else if (arrow13.activeInHierarchy)
            {
                itemInfo.text = "A small snack that replenishes 5 HP ";
                arrow13.SetActive(false);
                arrow1.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (arrow1.activeInHierarchy)
            {
                itemInfo.text = "";
                arrow1.SetActive(false);
                arrow13.SetActive(true);
            }
            else if (arrow2.activeInHierarchy)
            {
                itemInfo.text = "A small snack that replenishes 5 HP ";
                arrow2.SetActive(false);
                arrow1.SetActive(true);
            }
            else if (arrow3.activeInHierarchy)
            {
                itemInfo.text = "A meal-on-the-go that replenishes 10 HP ";
                arrow3.SetActive(false);
                arrow2.SetActive(true);
            }
            else if (arrow4.activeInHierarchy)
            {
                itemInfo.text = "A meal on-the-go that replenishes 20 HP ";
                arrow4.SetActive(false);
                arrow3.SetActive(true);
            }
            else if (arrow5.activeInHierarchy)
            {
                itemInfo.text = "A large meal that replenishes 50 HP ";
                arrow5.SetActive(false);
                arrow4.SetActive(true);

            }
            else if (arrow6.activeInHierarchy)
            {
                itemInfo.text = "A small drink that replenishes 5 STA ";
                arrow6.SetActive(false);
                arrow5.SetActive(true);
            }
            else if (arrow7.activeInHierarchy)
            {
                itemInfo.text = "A small drink that replenishes 10 STA ";
                arrow7.SetActive(false);
                arrow6.SetActive(true);
            }
            else if (arrow8.activeInHierarchy)
            {
                itemInfo.text = "A medium drink that replenishes 20 STA ";
                arrow8.SetActive(false);
                arrow7.SetActive(true);
            }
            else if (arrow9.activeInHierarchy)
            {
                itemInfo.text = "A large drink that replenishes 50 STA ";
                arrow9.SetActive(false);
                arrow8.SetActive(true);
            }
            else if (arrow10.activeInHierarchy)
            {
                itemInfo.text = "An explosive that can cause decent damage and even burn things close by ";
                arrow10.SetActive(false);
                arrow9.SetActive(true);
            }
            else if (arrow11.activeInHierarchy)
            {
                itemInfo.text = "An explosive that can cause significant damage to things close by ";
                arrow11.SetActive(false);
                arrow10.SetActive(true);
            }
            else if (arrow12.activeInHierarchy)
            {
                itemInfo.text = "An item that allows Sunny to perform more actions during the day ";
                arrow12.SetActive(false);
                arrow11.SetActive(true);
            }
            else if (arrow13.activeInHierarchy)
            {
                itemInfo.text = "-A mysterious item that brings Sunny back to life if her HP hits 0 ";
                arrow13.SetActive(false);
                arrow12.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (arrow1.activeInHierarchy)
            {
                if (coins >= 5)
                {
                    coins = coins - 5;
                    smallHP.onClick.Invoke();
                    //shopText.text = "You bought a small hp potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("fruit parfait", "Fruit Parfait"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow2.activeInHierarchy)
            {
                if (coins >= 10)
                {
                    coins = coins - 10;
                    smMedHP.onClick.Invoke();
                    //shopText.text = "You bought a medium hp potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("black bean soup", "Black Bean Soup"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow3.activeInHierarchy)
            {
                if (coins >= 20)
                {
                    coins = coins - 20;
                    medHP.onClick.Invoke();
                    //shopText.text = "You bought a large hp potion";
                    Debug.Log("Large HP pot purchased");
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("artisanal sandwich", "Artisanal Sandwich"));                    
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow4.activeInHierarchy)
            {
                if (coins >= 50)
                {
                    coins = coins - 50;
                    largeHP.onClick.Invoke();
                    //shopText.text = "You bought a small stamina potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("gourmet pizza", "Gourmet Pizza"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow5.activeInHierarchy)
            {
                if (coins >= 5)
                {
                    coins = coins - 5;
                    smallST.onClick.Invoke();
                    //shopText.text = "You bought a medium stamina potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("water bottle", "Water Bottle"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow6.activeInHierarchy)
            {
                if (coins >= 10)
                {
                    coins = coins - 10;
                    smMedST.onClick.Invoke();
                    //shopText.text = "You bought a large stamina potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("citrus cola can", "citrus cola can"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow7.activeInHierarchy)
            {
                if (coins >= 20)
                {
                    coins = coins - 20;
                    medST.onClick.Invoke();
                    //shopText.text = "You bought a fire cracker";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("lemonade jug", "Lemonade Jug"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow8.activeInHierarchy)
            {
                if (coins >= 50)
                {
                    coins = coins - 50;
                    largeST.onClick.Invoke();
                    //shopText.text = "You bought a single bomb";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("coffee pot", "Coffee Pot"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow9.activeInHierarchy)
            {
                if (coins >= 10)
                {
                    coins = coins - 10;
                    fireCrack.onClick.Invoke();
                    //shopText.text = "You bought an aoe bomb";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("fire cracker", "Fire Cracker"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow10.activeInHierarchy)
            {
                if (coins >= 30)
                {
                    coins = coins - 30;
                    bundle.onClick.Invoke();
                    //shopText.text = "You bought a mega firework bundle";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("mega firework bundle", "Mega Firework Bundle"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow11.activeInHierarchy)
            {
                if (coins >= 100)
                {
                    coins = coins - 100;
                    watch.onClick.Invoke();
                    //shopText.text = "You bought a mega firework bundle";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("wrist watch", "Wrist Watch"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow12.activeInHierarchy)
            {
                if (coins >= 75)
                {
                    coins = coins - 75;
                    pendant.onClick.Invoke();
                    //shopText.text = "You bought a mega firework bundle";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("protective pendant", "Protective Pendant"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow13.activeInHierarchy)
            {
                exit.onClick.Invoke();
            }

        }

    }
}

