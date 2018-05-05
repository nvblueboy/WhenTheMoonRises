using UnityEngine;

public class AudioController : MonoBehaviour {

    // Awake
    void Awake()
    {
        DontDestroyOnLoad(this);
    }    

    // Update is called once per frame
    void Update () {
        if(GameController.getCurrentScene().Contains("Night"))
        {
            Destroy(gameObject);
        }		
	}
}
