using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchController : MonoBehaviour {

    public bool save;

    private bool oldSave;
    private bool upToDate;


	// Use this for initialization
	void Start () {
        save = false;
        upToDate = true;
	}

    void Awake()
    {
        Debug.Log("Awake");
    }
	
	// Update is called once per frame
	void Update () {
	    if (save && !oldSave)
        {
            upToDate = false;
        }
        if (!save && oldSave)
        {
            upToDate = false;
        }

        if (!upToDate)
        {
            Debug.Log("Updating");
            updateData();
            upToDate = true;
        }

        oldSave = save;
	}

    void updateData()
    {
        if (save)
        {
            List<GameObject> rootObjects = new List<GameObject>();
            Scene scene = SceneManager.GetActiveScene();
            scene.GetRootGameObjects(rootObjects);

            foreach (GameObject go in rootObjects)
            {
                if (go != this.gameObject)
                {
                    go.transform.parent = this.transform;
                }
            }
        } else
        {
            List<Transform> tList = new List<Transform>();
            foreach(Transform t in this.transform)
            {
                tList.Add(t);
            }

            foreach(Transform child in tList)
            {
                child.parent = null;
            }
        }
    }
}
