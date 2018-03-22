﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchController : MonoBehaviour {

    public bool save;

    public bool destroyEnemy;

    private bool oldSave;
    private bool upToDate;

    private string exitState;
   
    public FightController fc;
    public GameObject fc_go;

    public MonoBehaviour passingObject;
    public GameObject passingGameObject;

    private GameObject parentGO;

	// Use this for initialization
	void Start () {
        save = false;
        upToDate = true;
        destroyEnemy = false;

        fc = null;

        parentGO = new GameObject("Inactive Scene");
        parentGO.transform.parent = this.transform;
        parentGO.SetActive(false);

    }    
	
	// Update is called once per frame
	void Update () {

        destroyEnemy = false;

        if (fc != null) {
            if (fc.state == "post-fight") {

                save = false;
                if (fc.exitState == "win") {
                    destroyEnemy = true;
                }
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

            if(destroyEnemy) {
                StarSpawnController ssc = passingGameObject.GetComponent<StarSpawnController>();
                if(ssc != null) {
                    ssc.DropStar();
                }
                Destroy(passingGameObject);
            } else {
                passingGameObject.GetComponent<EnemyMovement>().tempDisableTrigger();
            }
        }
    }
}
