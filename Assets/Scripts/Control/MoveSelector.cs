using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSelector : MonoBehaviour {

    public Text topText;
    public Text selectedText;
    public Text bottomText;

    public List<string> moves;

    public int currentSelection = 0;

    private float oldValue;
    private bool hasFallen;
    private bool isRising;

    private float oldSelect = 0;

    public Fighter fighter;

	// Use this for initialization
	void Start () {
        if (fighter == null)
        {
            Debug.LogWarning("There is no fighter class attached!");
        }
        currentSelection = 0;
        updateTextBoxes();
	}

    // Update is called once per frame
    void Update() {
        float _old = Mathf.Abs(oldValue);
        float _new = Mathf.Abs(Input.GetAxis("Vertical"));
        if (_old > _new)
        {
            //If the old value is greater than the new value, the value must be falling (the player has let off the key)
            hasFallen = true;
            isRising = false;
        }
        if (_new > _old)
        {
            //If the new value is greater than the old value, the key must have been pressed.
            isRising = true;
        }
        if (isRising && hasFallen) {
            hasFallen = false;
            if (Input.GetAxis("Vertical") > 0) {
                currentSelection--;
            } else {
                currentSelection++;
            }
            if (currentSelection < 0) {
                currentSelection = moves.Count-1;
            }
            if (currentSelection >= moves.Count) {
                currentSelection = 0;
            }
            updateTextBoxes();
        }

        if (Input.GetAxis("Jump") > 0 && oldSelect == 0)
        {
            fighter.addSelectedMove(moves[currentSelection]);
        }

        oldValue = Input.GetAxis("Vertical");
        oldSelect = Input.GetAxis("Jump");
    }

    public void updateTextBoxes() {
        topText.text = getCircular<string>(moves, currentSelection - 1);
        selectedText.text = getCircular<string>(moves, currentSelection);
        bottomText.text = getCircular<string>(moves, currentSelection + 1);
    }

    public T getCircular<T>(List<T> list, int index) {
        int len = list.Count;
        if (index >= len) {
            return getCircular<T>(list, index - len);
        }
        if(index < 0) {
            return getCircular<T>(list, index + len);
        }
        return list[index];
    }
}
