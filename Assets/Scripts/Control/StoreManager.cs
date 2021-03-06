using UnityEngine;

public class StoreManager : MonoBehaviour {
    static GameObject storeUI;
    static GameObject character;
    DialogueController dController;

    // Start
    void Start()
    {
        storeUI = GameObject.FindGameObjectWithTag("StoreUI");
        character = GameObject.FindGameObjectWithTag("BldgCharacter");
        Debug.Log("StoreUI null: " + storeUI == null);
        dController = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
        storeUI.SetActive(false);        
    }    
    
    // openStore
    public static void openStore()
    {
        storeUI.SetActive(true);
        character.SetActive(false);
    }	

    // exitStore
    public void exitStore()
    {
        storeUI.SetActive(false);
        character.SetActive(true);

        string prevScene = GameController.GetPreviousScene();
        if (prevScene[3] == '1')
        {
            dController.Show(54);
        }
        else if(prevScene[3] == '2')
        {
            dController.Show(57);
        }
    }
}