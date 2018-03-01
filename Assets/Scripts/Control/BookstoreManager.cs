using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookstoreManager : MonoBehaviour {
    static GameObject storeUI;
    DialogueController dController;

	// Start
	void Start () {
        storeUI = GameObject.FindGameObjectWithTag("StoreUI");
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
        dController.Show(25);
    }
}
