using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Name: Dylan Bowman
 * ID: 2250585
 * Email: bowma128@mail.chapman.edu
 * Course: CPSC-340-01
 * Assignment: Semester Project
 * 
 * Description: Handles the UI to select what move the player will use.
 */
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
            Debug.LogWarning("There is no fighter class attached to the move selector.");
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
        //If the axis has fallen and is currently rising, the player must have just pressed it.
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

    /*
     * Name: updateTextBoxes
     * Parameters: None
     * Description: Sets the text in the selector to display the selected move
     *     as well as the one above and below it on the list.
     */ 
    public void updateTextBoxes() {
        topText.text = getCircular<string>(moves, currentSelection - 1);
        selectedText.text = getCircular<string>(moves, currentSelection);
        bottomText.text = getCircular<string>(moves, currentSelection + 1);
    }

    /*
     * Name: getCircular
     * Parameters: List<T> list, int index
     * Description: gets the item of parameter list at parameter index if the list was circular.
     *     This acts as if one past the last element is the first element and vice versa.
     *     This is a generic function for later usability.
     */
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
