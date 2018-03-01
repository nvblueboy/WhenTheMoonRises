using UnityEngine;

public class LoadScene : MonoBehaviour {
    public string scene;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameController.LoadScene(scene);
            Debug.Log("Loaded scene: " + scene);
        }
    }	
}
