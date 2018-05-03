using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawnController : MonoBehaviour {

    public Vector3 offset = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropStar() {
        GameObject star = (GameObject)Instantiate(Resources.Load("items/StarShard"));
        star.transform.position = this.transform.position + offset;
    }
}
