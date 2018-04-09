using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSoundFXController : MonoBehaviour {


    public List<MoveSound> sounds;

    public FightController fc;

    private Dictionary<string, AudioClip> soundDict;

    private AudioSource source;

	// Use this for initialization
	void Start () {
        soundDict = new Dictionary<string, AudioClip>();

        foreach(MoveSound sound in sounds) {
            soundDict.Add(sound.name.ToLower(), sound.sound);
        }

        source = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate() {
        if (fc.animationNeeded) {
            Debug.Log(fc.moveName.ToLower());
            if (soundDict.ContainsKey(fc.moveName.ToLower())) {
                source.clip = soundDict[fc.moveName.ToLower()];
                source.time = 0f;
                source.Play();
               
            }
        }
    }
}

[System.Serializable]
public struct MoveSound {
    public string name;
    public AudioClip sound;
}
