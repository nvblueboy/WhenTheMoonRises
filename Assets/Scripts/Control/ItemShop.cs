using System.Collections;
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
    int placeholder = 0;
    //ArrayList arrows = new ArrayList();
    public Button smallHP;
    public Button medHP;
    public Button largeHP;
    public Button smallST;
    public Button medST;
    public Button largeST;
    public Button fireCrack;
    public Button sBomb;
    public Button aoeBomb;
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
        itemInfo.text = "Restores 5 HP ";
    }
    // Update is called once per frame
    void Update()
    {
        GameController.player.coins = coins;
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (arrow1.activeInHierarchy)
            {
                itemInfo.text = "Restores 20 HP ";
                arrow1.SetActive(false);
                arrow2.SetActive(true);
            }
            else if (arrow2.activeInHierarchy)
            {
                itemInfo.text = "Restores 50 HP ";
                arrow2.SetActive(false);
                arrow3.SetActive(true);
            }
            else if (arrow3.activeInHierarchy)
            {
                itemInfo.text = "Restores 5 Stamina ";
                arrow3.SetActive(false);
                arrow4.SetActive(true);
            }
            else if (arrow4.activeInHierarchy)
            {
                itemInfo.text = "Restores 20 Stamina ";
                arrow4.SetActive(false);
                arrow5.SetActive(true);
            }
            else if (arrow5.activeInHierarchy)
            {
                itemInfo.text = "Restores 50 Stamina ";
                arrow5.SetActive(false);
                arrow6.SetActive(true);

            }
            else if (arrow6.activeInHierarchy)
            {
                itemInfo.text = "-5 Enemy HP, Burned Status Effect ";
                arrow6.SetActive(false);
                arrow7.SetActive(true);
            }
            else if (arrow7.activeInHierarchy)
            {
                itemInfo.text = "-3 Enemy HP \n to 1 Enemy ";
                arrow7.SetActive(false);
                arrow8.SetActive(true);
            }
            else if (arrow8.activeInHierarchy)
            {
                itemInfo.text = "-3 Enemy HP to All Enemies ";
                arrow8.SetActive(false);
                arrow9.SetActive(true);
            }
            else if (arrow9.activeInHierarchy)
            {
                itemInfo.text = "-8 Enemy HP to All Enemies ";
                arrow9.SetActive(false);
                arrow10.SetActive(true);
            }
            else if (arrow10.activeInHierarchy)
            {
                itemInfo.text = "";
                arrow10.SetActive(false);
                arrow11.SetActive(true);
            }
            else if (arrow11.activeInHierarchy)
            {
                itemInfo.text = "Restores 5 HP ";
                arrow11.SetActive(false);
                arrow1.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (arrow1.activeInHierarchy)
            {
                itemInfo.text = "";
                arrow1.SetActive(false);
                arrow11.SetActive(true);
            }
            else if (arrow2.activeInHierarchy)
            {
                itemInfo.text = "Restores 5 HP ";
                arrow2.SetActive(false);
                arrow1.SetActive(true);
            }
            else if (arrow3.activeInHierarchy)
            {
                itemInfo.text = "Restores 20 HP ";
                arrow3.SetActive(false);
                arrow2.SetActive(true);
            }
            else if (arrow4.activeInHierarchy)
            {
                itemInfo.text = "Restores 50 HP ";
                arrow4.SetActive(false);
                arrow3.SetActive(true);
            }
            else if (arrow5.activeInHierarchy)
            {
                itemInfo.text = "Restores 5 Stamina ";
                arrow5.SetActive(false);
                arrow4.SetActive(true);

            }
            else if (arrow6.activeInHierarchy)
            {
                itemInfo.text = "Restores 20 Stamina ";
                arrow6.SetActive(false);
                arrow5.SetActive(true);
            }
            else if (arrow7.activeInHierarchy)
            {
                itemInfo.text = "Restores 50 Stamina ";
                arrow7.SetActive(false);
                arrow6.SetActive(true);
            }
            else if (arrow8.activeInHierarchy)
            {
                itemInfo.text = "-5 Enemy HP, Burned Status Effect ";
                arrow8.SetActive(false);
                arrow7.SetActive(true);
            }
            else if (arrow9.activeInHierarchy)
            {
                itemInfo.text = "-3 Enemy HP \n to 1 Enemy ";
                arrow9.SetActive(false);
                arrow8.SetActive(true);
            }
            else if (arrow10.activeInHierarchy)
            {
                itemInfo.text = "-3 Enemy HP to All Enemies ";
                arrow10.SetActive(false);
                arrow9.SetActive(true);
            }
            else if (arrow11.activeInHierarchy)
            {
                itemInfo.text = "-8 Enemy HP to All Enemies ";
                arrow11.SetActive(false);
                arrow10.SetActive(true);
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
                    GameController.player.addItem(new Item("small hp potion", "Small HP Potion"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow2.activeInHierarchy)
            {
                if (coins >= 20)
                {
                    coins = coins - 20;
                    medHP.onClick.Invoke();
                    //shopText.text = "You bought a medium hp potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("medium hp potion", "Medium HP Potion"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow3.activeInHierarchy)
            {
                if (coins >= 50)
                {
                    coins = coins - 50;
                    largeHP.onClick.Invoke();
                    //shopText.text = "You bought a large hp potion";
                    Debug.Log("Large HP pot purchased");
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("large hp potion", "Large HP Potion"));                    
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow4.activeInHierarchy)
            {
                if (coins >= 5)
                {
                    coins = coins - 5;
                    smallST.onClick.Invoke();
                    //shopText.text = "You bought a small stamina potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("small stamina potion", "Small Stamina Potion"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow5.activeInHierarchy)
            {
                if (coins >= 20)
                {
                    coins = coins - 20;
                    medST.onClick.Invoke();
                    //shopText.text = "You bought a medium stamina potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("medium stamina potion", "Medium Stamina Potion"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow6.activeInHierarchy)
            {
                if (coins >= 50)
                {
                    coins = coins - 50;
                    largeST.onClick.Invoke();
                    //shopText.text = "You bought a large stamina potion";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("large stamina potion", "Large Stamina Potion"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow7.activeInHierarchy)
            {
                if (coins >= 10)
                {
                    coins = coins - 10;
                    fireCrack.onClick.Invoke();
                    //shopText.text = "You bought a fire cracker";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("fire cracker", "Fire Cracker"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow8.activeInHierarchy)
            {
                if (coins >= 6)
                {
                    coins = coins - 6;
                    sBomb.onClick.Invoke();
                    //shopText.text = "You bought a single bomb";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("single bomb", "Single Bomb"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow9.activeInHierarchy)
            {
                if (coins >= 12)
                {
                    coins = coins - 12;
                    aoeBomb.onClick.Invoke();
                    //shopText.text = "You bought an aoe bomb";
                    coinText.text = coins + " C";
                    GameController.player.addItem(new Item("aoe bomb", "AOE Bomb"));
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow10.activeInHierarchy)
            {
                if (coins >= 50)
                {
                    coins = coins - 50;
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
                exit.onClick.Invoke();
            }

        }

    }
}

