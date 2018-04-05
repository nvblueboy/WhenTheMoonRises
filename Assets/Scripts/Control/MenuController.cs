using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MenuController : MonoBehaviour {
    GameObject menuUI, characterUI;
    Text txtLevel, txtWeapon, txtHealth, txtStamina, txtStrength, txtDefense,
        txtIntuition, txtExperience, txtPhase;
    PlayerMovementController playerController;
    private float oldMenu;
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

        InitCharacterMenu();                        		
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
    }

    // InitCharacterMenu
    private void InitCharacterMenu()
    {
        int childCount = characterUI.transform.childCount;        
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

    // ShowMenu
    private void ShowMenu()
    {
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
