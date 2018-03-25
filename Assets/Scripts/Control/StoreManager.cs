using UnityEngine;

public class StoreManager : MonoBehaviour {
    static GameObject storeUI;
    DialogueController dController;

    // Start
    void Start()
    {
        storeUI = GameObject.FindGameObjectWithTag("StoreUI");
        Debug.Log("StoreUI null: " + storeUI == null);
        dController = GameObject.FindGameObjectWithTag("DialogueController").GetComponent<DialogueController>();
        storeUI.SetActive(false);        
    }    
    
    // openStore
    public static void openStore()
    {
        storeUI.SetActive(true);
    }	

    // exitStore
    public void exitStore()
    {
        storeUI.SetActive(false);
        dController.Show(54);     
    }
}