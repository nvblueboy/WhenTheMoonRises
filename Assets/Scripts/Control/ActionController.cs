using UnityEngine;

public class ActionController : MonoBehaviour {

	// Start
	void Start () {
		
	}    
	
	// Update is called once per frame
	void Update () {
		
	}    

    public static void performAction(Constants.Action action)
    {
        switch(action)
        {
            case Constants.Action.OPEN_STORE:
                Debug.Log("Open store");
                StoreManager.openStore();
                break;
            case Constants.Action.OPEN_BOOKSTORE:
                Debug.Log("Open bookstore");
                BookstoreManager.openStore();
                break;
            case Constants.Action.NONE:
                break;
            case Constants.Action.ADD_STRENGTH:
                Debug.Log("Strength increased");
                break;
            case Constants.Action.ADD_STAMINA:
                break;
            case Constants.Action.ADD_INTUITION:
                Debug.Log("Intuition increased");
                break;
            case Constants.Action.ADD_MAGIC:
                Debug.Log("Magic increased");
                break;
            case Constants.Action.ADD_DEFENSE:
                Debug.Log("Defense increased");
                break;
            case Constants.Action.ADD_HEALTH:
                Debug.Log("Health increased");
                break;
            case Constants.Action.LOAD_PREV_SCENE:
                Debug.Log("Load previous scene");
                GameController.LoadPreviousScene();
                break;
            default:
                break;
        }
    }  
}
