using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAnimationDefinitions : MonoBehaviour {
    public List<MoveAnimation> animations;

    public Dictionary<string, AnimationType> animDict;


    // Use this for initialization
    void Start () {
        animDict = new Dictionary<string, AnimationType>();

        foreach (MoveAnimation anim in animations) {
            animDict.Add(anim.name, anim.type);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public AnimationType getType(string move) {
        if (animDict.ContainsKey(move)) {
            return animDict[move];
        } else {
            return AnimationType.None;
        }
    }
}

public enum AnimationType { Flash, Shake, Fade, None };

[System.Serializable]
public struct MoveAnimation {
    public string name;
    public AnimationType type;
}