using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchController : MonoBehaviour {

    public bool save;

    private bool oldSave;
    private bool upToDate;
   
    public FightController fc;
    public GameObject fc_go;

    public MonoBehaviour passingObject;
    public GameObject passingGameObject;

    private GameObject parentGO;

	// Use this for initialization
	void Start () {
        save = false;
        upToDate = true;

        fc = null;

        parentGO = new GameObject("Inactive Scene");
        parentGO.transform.parent = this.transform;
        parentGO.SetActive(false);

    }

    void Awake() {
        Debug.Log("Awake");
    }
	
	// Update is called once per frame
	void Update () {
        if (fc != null) {
            if (fc.state == "post-fight") {
                save = false;
                Destroy(fc_go);
            }
        }

        if (save && !oldSave) {
            upToDate = false;
        }

        if (!save && oldSave) {
            upToDate = false;
        }

        if (!upToDate) {
            Debug.Log("Updating");
            updateData();
            upToDate = true;
        }

        oldSave = save;
	}

    public void passObject(MonoBehaviour mb)
    {
        passingObject = mb;
    }


    void updateData()
    {
        if (save) {
            DontDestroyOnLoad(this.gameObject);
            List<GameObject> rootObjects = new List<GameObject>();
            Scene scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(rootObjects);

            foreach (GameObject go in rootObjects) {
                if (go != this.gameObject) {
                    go.transform.parent = parentGO.transform;
                }
            }

            SceneManager.LoadScene("Fight");
        } else {
            List<GameObject> rootObjects = new List<GameObject>();
            Scene scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(rootObjects);

            foreach (GameObject go in rootObjects) {
                if (go != this.gameObject) {
                    Destroy(go);
                }
            }

            List<Transform> tList = new List<Transform>();
            foreach(Transform t in parentGO.transform) {
                tList.Add(t);
            }

            foreach(Transform child in tList) {
                child.parent = null;
            }

            Destroy(passingGameObject);
        }
    }
}
