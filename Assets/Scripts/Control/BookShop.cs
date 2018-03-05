using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Name: Andrew Krager
ID: 1879130
Email: krage100@mail.chapman.edu
Course: CPSC-440-01
Assignment: Semester Project

Description: This is a script controlling the in game book shop
*/
public class BookShop : MonoBehaviour
{
    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;
    public GameObject arrow4;
    public GameObject arrow5;
    public GameObject arrow6;
    public GameObject arrow7;
    public GameObject arrow8;
    int placeholder = 0;
    //ArrayList arrows = new ArrayList();
    public Button maxHP;
    public Button stamina;
    public Button strength;
    public Button defense;
    public Button intuition;
    public Button magic;
    public Button luck;
    public Button exit;
    public Text shopText;
    public Text coinText;
    public Text itemInfo;
    public PlayerCharacter player;
    public int coins;
    public string item;
    //public GameObject inventory = GameObject.Find("EventSystem");
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        arrow1 = GameObject.Find("arrow1");
        arrow2 = GameObject.Find("arrow2");
        arrow3 = GameObject.Find("arrow3");
        arrow4 = GameObject.Find("arrow4");
        arrow5 = GameObject.Find("arrow5");
        arrow6 = GameObject.Find("arrow6");
        arrow7 = GameObject.Find("arrow7");
        arrow8 = GameObject.Find("arrow8");
        player.increaseCoins(1000);
        coins = player.getCoins();
        coinText.text = (player.getCoins()).ToString() + " C";
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
        itemInfo.text = "+1 Max HP ";
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (arrow1.activeInHierarchy)
            {
                itemInfo.text = "+1 Stamina ";
                arrow1.SetActive(false);
                arrow2.SetActive(true);
            }
            else if (arrow2.activeInHierarchy)
            {
                itemInfo.text = "+1 Strength ";
                arrow2.SetActive(false);
                arrow3.SetActive(true);
            }
            else if (arrow3.activeInHierarchy)
            {
                itemInfo.text = "+1 Defense ";
                arrow3.SetActive(false);
                arrow4.SetActive(true);
            }
            else if (arrow4.activeInHierarchy)
            {
                itemInfo.text = "+1 Intuition ";
                arrow4.SetActive(false);
                arrow5.SetActive(true);
            }
            else if (arrow5.activeInHierarchy)
            {
                itemInfo.text = "+1 Magic ";
                arrow5.SetActive(false);
                arrow6.SetActive(true);

            }
            else if (arrow6.activeInHierarchy)
            {
                itemInfo.text = "+1 Luck ";
                arrow6.SetActive(false);
                arrow7.SetActive(true);
            }
            else if (arrow7.activeInHierarchy)
            {
                itemInfo.text = " ";
                arrow7.SetActive(false);
                arrow8.SetActive(true);
            }
            else if (arrow8.activeInHierarchy)
            {
                itemInfo.text = "+1 to Maximum HP ";
                arrow8.SetActive(false);
                arrow1.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (arrow1.activeInHierarchy)
            {
                itemInfo.text = " ";
                arrow1.SetActive(false);
                arrow8.SetActive(true);
            }
            else if (arrow2.activeInHierarchy)
            {
                itemInfo.text = "+1 Max HP ";
                arrow2.SetActive(false);
                arrow1.SetActive(true);
            }
            else if (arrow3.activeInHierarchy)
            {
                itemInfo.text = "+1 Stamina ";
                arrow3.SetActive(false);
                arrow2.SetActive(true);
            }
            else if (arrow4.activeInHierarchy)
            {
                itemInfo.text = "+1 Strength ";
                arrow4.SetActive(false);
                arrow3.SetActive(true);
            }
            else if (arrow5.activeInHierarchy)
            {
                itemInfo.text = "+1 Defense ";
                arrow5.SetActive(false);
                arrow4.SetActive(true);

            }
            else if (arrow6.activeInHierarchy)
            {
                itemInfo.text = "+1 Intuition ";
                arrow6.SetActive(false);
                arrow5.SetActive(true);
            }
            else if (arrow7.activeInHierarchy)
            {
                itemInfo.text = "+1 Magic ";
                arrow7.SetActive(false);
                arrow6.SetActive(true);
            }
            else if (arrow8.activeInHierarchy)
            {
                itemInfo.text = "+1 Luck ";
                arrow8.SetActive(false);
                arrow7.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (arrow1.activeInHierarchy)
            {
                if (coins >= 30)
                {
                    coins = coins - 30;
                    maxHP.onClick.Invoke();
                    //shopText.text = "You bought a small hp potion";
                    coinText.text = coins + " C";
                    player.hp++;
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow2.activeInHierarchy)
            {
                if (coins >= 30)
                {
                    coins = coins - 30;
                    stamina.onClick.Invoke();
                    //shopText.text = "You bought a medium hp potion";
                    coinText.text = coins + " C";
                    player.stamina++;
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow3.activeInHierarchy)
            {
                if (coins >= 10)
                {
                    coins = coins - 10;
                    strength.onClick.Invoke();
                    //shopText.text = "You bought a large hp potion";
                    coinText.text = coins + " C";
                    player.increaseStrength(1);
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow4.activeInHierarchy)
            {
                if (coins >= 10)
                {
                    coins = coins - 10;
                    defense.onClick.Invoke();
                    //shopText.text = "You bought a small stamina potion";
                    coinText.text = coins + " C";
                    player.increaseDefense(1);
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
                    intuition.onClick.Invoke();
                    //shopText.text = "You bought a medium stamina potion";
                    coinText.text = coins + " C";
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow6.activeInHierarchy)
            {
                if (coins >= 20)
                {
                    coins = coins - 20;
                    magic.onClick.Invoke();
                    //shopText.text = "You bought a large stamina potion";
                    coinText.text = coins + " C";
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
                    luck.onClick.Invoke();
                    //shopText.text = "You bought a fire cracker";
                    coinText.text = coins + " C";
                }
                else
                {
                    shopText.text = "You don't have enough \n coins to buy that";
                }
            }
            else if (arrow8.activeInHierarchy)
            {
                exit.onClick.Invoke();
            }

        }

    }
}
