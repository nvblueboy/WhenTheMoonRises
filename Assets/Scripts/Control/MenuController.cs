using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour {
    GameObject menuUI, characterUI;
    Text txtLevel, txtWeapon, txtHealth, txtStamina, txtStrength, txtDefense,
        txtIntuition, txtExperience, txtPhase;
    Toggle toggleCharacter, toggleInventory, toggleJournal, toggleOptions;
    List<Toggle> tabs;
    PlayerMovementController playerController;
    private float oldMenu, oldRight, oldLeft;
    private int tabIndex;
    private bool menuActive;

	// Start
	void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerMovementController>();
        menuUI = GameObject.FindGameObjectWithTag("MenuUI");
        characterUI = GameObject.FindGameObjectWithTag("CharacterUI");
        menuUI.SetActive(false);
        menuActive = false;
        oldMenu = 0f;
        oldLeft = 0f;
        oldRight = 0f;
        tabIndex = 0;

        InitMenu();                                      		
	}
	
	// Update
	void Update () {
        float newMenu = Input.GetAxis("Cancel");
        if (newMenu > 0 && oldMenu < 1  
            && GameController.getCurrentScene().Contains("Day"))
        {
            if(menuActive)
            {
                ExitMenu();
            }
            else
            {
                ShowMenu();                
            }           
        }
        oldMenu = newMenu;

        float right = Input.GetAxis("ToggleRight");
        if(oldRight < 1 && right > 0 && menuActive)
        {            
            try
            {
                tabIndex++;
                SetActiveTab(tabIndex);                
            } catch (ArgumentOutOfRangeException e)
            {                
                tabIndex--;
            }
        }
        oldRight = right;

        float left = Input.GetAxis("ToggleLeft");
        if (oldLeft < 1 &&  left > 0 && menuActive)
        {            
            try
            {
                tabIndex--;
                SetActiveTab(tabIndex);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.Log("Here");
                tabIndex++;
            }
        }
        oldLeft = left;
    }

    // InitMenu
    private void InitMenu()
    {
        //init tabs
        tabs = new List<Toggle>();
        int childCount = menuUI.transform.childCount;
        for(int i = 0; i < childCount; ++i)
        {
            GameObject gameObject = menuUI.transform.GetChild(i).gameObject;
            if (gameObject.name.Contains("character")) {
                toggleCharacter = gameObject.GetComponent<Toggle>();
                tabs.Add(toggleCharacter);
            } else if (gameObject.name.Contains("inventory")) {
                toggleInventory = gameObject.GetComponent<Toggle>();
                tabs.Add(toggleInventory);
            } else if (gameObject.name.Contains("journal")) {
                toggleJournal = gameObject.GetComponent<Toggle>();
                tabs.Add(toggleJournal);
            } else if (gameObject.name.Contains("options")) {
                toggleOptions = gameObject.GetComponent<Toggle>();
                tabs.Add(toggleOptions);
            }
        }

        // init character menu elements
        childCount = characterUI.transform.childCount;        
        for(int i = 0; i < childCount; ++i)
        {            
            GameObject gameObject = characterUI.transform.GetChild(i).gameObject;
            if (gameObject.name.Contains("strength")) {
                txtStrength = gameObject.GetComponent<Text>();
            } else if (gameObject.name.Contains("lvl")) {
                txtLevel = gameObject.GetComponent<Text>();
            } else if (gameObject.name.Contains("defense")) {
                txtDefense = gameObject.GetComponent<Text>();
            } else if (gameObject.name.Contains("stamina")) {
                txtStamina = gameObject.GetComponent<Text>();
            } else if (gameObject.name.Contains("intuition")) {
                txtIntuition = gameObject.GetComponent<Text>();
            } else if (gameObject.name.Contains("experience")) {
                txtExperience = gameObject.GetComponent<Text>();
            } else if (gameObject.name.Contains("phase")) {
                txtPhase = gameObject.GetComponent<Text>();
            } else if (gameObject.name.Contains("health")) {
                txtHealth = gameObject.GetComponent<Text>();
            } else if (gameObject.name.Contains("weapon")) {
                txtWeapon = gameObject.GetComponent<Text>();
            }
        }
    }    

    // UpdateCharacterMenu
    private void UpdateCharacterMenu()
    {
        PlayerCharacter player = GameController.player;   
             
        txtHealth.text = player.currHP + "/" + player.hp;
        txtDefense.text = player.defense.ToString();
        txtStamina.text = player.currStamina + "/" + player.stamina;
        txtIntuition.text = player.intuition.ToString();        
        txtExperience.text = player.experience.ToString();

        string weapon = player.weapon;
        if(weapon != null && weapon != "")
        {
            txtWeapon.text = char.ToUpper(weapon[0]) + weapon.Substring(1);
        }

        string currentScene = GameController.getCurrentScene();
        if(currentScene.Contains("Day"))
        {
            txtPhase.text = currentScene.Substring(0, 3) + " " + currentScene.Substring(3, 1)
                + ", Actions Left " + (6 - ActionController.getActionCount()) + "/6";
        } else if(currentScene.Contains("Night")) {

        }
    }

    // InitInvertory
    private void UpdateInventory()
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        Debug.Log("Inventory null: " + inventory == null);
        Text slot1 = inventory.transform.GetChild(5).GetComponent<Text>();
        Text slot2 = inventory.transform.GetChild(6).GetComponent<Text>();
        Text slot3 = inventory.transform.GetChild(7).GetComponent<Text>();
        Text slot4 = inventory.transform.GetChild(8).GetComponent<Text>();
        Text slot5 = inventory.transform.GetChild(9).GetComponent<Text>();

        List<Text> slots = new List<Text>() { slot1, slot2, slot3, slot4, slot5 };

        for (int i = 0; i < 5; ++i)
        {
            try
            {
                slots[i].text = GameController.player.inventory[i].getDisplayName();

            }
            catch (ArgumentOutOfRangeException e)
            {
                slots[i].text = "Empty";
            }
        }
    }

    // SetActiveTab
    private void SetActiveTab(int index)
    {
        tabs[index].isOn = true;
        for(int i = 0; i < tabs.Count; ++i)
        {
            if(i != index)
            {
                tabs[i].isOn = false;
            } 
        }

        if (tabIndex == 1)
        {
            UpdateInventory();
        }
        else if (tabIndex == 3)
        {
            GameObject.FindGameObjectWithTag("SaveButton").GetComponent<Button>().Select();
        }
    }   

    // ShowMenu
    private void ShowMenu()
    {
        tabIndex = 0;
        SetActiveTab(tabIndex);       
        UpdateCharacterMenu();
        menuActive = true;        
        playerController.setPlayerCanMove(false);
        menuUI.SetActive(true);
    }

    // ExitMenu
    public void ExitMenu()
    {        
        playerController.setPlayerCanMove(true);
        menuUI.SetActive(false);
        menuActive = false;
    }

    // Exit
    public void Exit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}
