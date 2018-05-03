using UnityEngine;

public class ActionController : MonoBehaviour {
    private static int actionCount;	 

    public static void performAction(Constants.Action action)
    {
        switch(action)
        {
            case Constants.Action.OPEN_STORE:
                Debug.Log("Open store");
                StoreManager.openStore();
                break;
            case Constants.Action.OPEN_BOOKSTORE:
                Debug.Log("Action: Open bookstore");
                BookstoreManager.openStore();
                break;
            case Constants.Action.NONE:
                Debug.Log("Action: None");
                break;
            case Constants.Action.ADD_STRENGTH:
                Debug.Log("Action: Strength increased");
                GameController.player.strength += 1;             
                ++actionCount;
                break;
            case Constants.Action.ADD_STAMINA:
                Debug.Log("Action: Stamina increased");
                GameController.player.stamina += 1;
                GameController.player.currStamina += 1;
                ++actionCount;
                break;
            case Constants.Action.ADD_INTUITION:
                Debug.Log("Action: Intuition increased");
                GameController.player.intuition += 1;
                ++actionCount;
                break;
            case Constants.Action.ADD_MAGIC:
                Debug.Log("Action: Magic increased");
                ++actionCount;
                break;
            case Constants.Action.ADD_DEFENSE:
                Debug.Log("Action: Defense increased");
                GameController.player.defense += 1;
                ++actionCount;
                break;
            case Constants.Action.ADD_HEALTH:
                Debug.Log("Action: Health increased");
                GameController.player.hp += 1;
                GameController.player.currHP += 1;
                ++actionCount;
                break;
            case Constants.Action.LOAD_PREV_SCENE:
                Debug.Log("Action: Load previous scene");
                GameController.LoadPreviousScene();
                break;
            case Constants.Action.LOAD_NEXT_SCENE:
                Debug.Log("Action: Load next scene");                
                GameController.LoadScene("SunsetTransition");
                actionCount = 0;
                break;
            default:
                break;
        }
    }
    
    public static int getActionCount()
    {
        return actionCount;        
    } 
}
