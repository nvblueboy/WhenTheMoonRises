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

    public SelectorGroup root;

    public SelectorGroup current;

    public int currentSelection = 0;

    private float oldValue;
    private bool hasFallen;
    private bool isRising;

    private float oldSelect = 0;
    private float oldBack = 0;

    public Fighter fighter;

    // Use this for initialization
    void Start () {
        if (fighter == null)
        {
            Debug.LogWarning("There is no fighter class attached to the move selector.");
        }
        currentSelection = 0;

        //Lay out the selector groups.
        root = new SelectorGroup();

        bool hasItems = true; //TODO: Make this actually evaluate if the player has items.

        if (hasItems) {
            SelectorOption items = new SelectorOption("Items", root, new SelectorGroup());
            root.addOption(items);
            //TODO: Loop through items and add them to this selector option.
            SelectorOption item1 = new SelectorOption("Potion", items.child, "potion");
            SelectorOption item2 = new SelectorOption("Fire Flower", items.child, "fire_flower");
            SelectorOption item3 = new SelectorOption("Mushroom", items.child, "mushroom");
            items.child.addOption(item1);
            items.child.addOption(item2);
            items.child.addOption(item3);
        }

        SelectorOption moves = new SelectorOption("Moves", root, new SelectorGroup());
        root.addOption(moves);

        SelectorOption move1 = new SelectorOption("Punch", moves.child, "punch");
        moves.child.addOption(move1);

        SelectorOption move2 = new SelectorOption("Kick", moves.child, "kick");
        moves.child.addOption(move2);

        SelectorOption options = new SelectorOption("Options", root, new SelectorGroup());
        root.addOption(options);

        SelectorOption option1 = new SelectorOption("Run", options.child, "run");
        options.child.addOption(option1);

        SelectorOption option2 = new SelectorOption("Wait", options.child, "wait");
        options.child.addOption(option2);

        Debug.Log(root);

        current = root;

        updateTextBoxes();
    }

    // Update is called once per frame
    void Update() {
        float _old = Mathf.Abs(oldValue);
        float _new = Mathf.Abs(Input.GetAxis("Vertical"));
        if(_old > _new) {
            //If the old value is greater than the new value, the value must be falling (the player has let off the key)
            hasFallen = true;
            isRising = false;
        }
        if(_new > _old) {
            //If the new value is greater than the old value, the key must have been pressed.
            isRising = true;
        }
        //If the axis has fallen and is currently rising, the player must have just pressed it.
        if(isRising && hasFallen) {
            hasFallen = false;
            if(Input.GetAxis("Vertical") > 0) {
                currentSelection--;
            } else {
                currentSelection++;
            }
            if(currentSelection < 0) {
                currentSelection = current.options.Count - 1;
            }
            if(currentSelection >= current.options.Count) {
                currentSelection = 0;
            }
            updateTextBoxes();
        }

        if(Input.GetAxis("Jump") > 0 && oldSelect == 0) {
           if (!current.options[currentSelection].isData) {
                current = current.options[currentSelection].child;
                updateTextBoxes();
           }
        }

        if (Input.GetAxis("Fire3") > 0 && oldSelect == 0) {
            if(current != root) {
                current = current.parent;
                updateTextBoxes();
            }
        }

        oldBack = Input.GetAxis("Fire3");
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
        topText.text = getCircular<SelectorOption>(current.options, currentSelection - 1).displayString;
        selectedText.text = getCircular<SelectorOption>(current.options, currentSelection).displayString;
        bottomText.text = getCircular<SelectorOption>(current.options, currentSelection + 1).displayString;
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

public class SelectorGroup {
    public List<SelectorOption> options;
    public SelectorGroup parent;

    public SelectorGroup(List<SelectorOption> _options) {
        options = _options;
    }

    public SelectorGroup() {
        options = new List<SelectorOption>();
    }

    public void addOption(SelectorOption option) {
        options.Add(option);
    }

    public void setParent(SelectorGroup _parent) {
        parent = _parent;
    }

    public override string ToString() {
        List<string> strs = new List<string>();
        foreach(SelectorOption option in options) {
            strs.Add(option.ToString());
        }
        return "[" + string.Join(",", strs.ToArray()) + "]";
    }

}

public class SelectorOption {
    public string displayString;
    public SelectorGroup child;
    public string data;
    public bool isData;
    public SelectorGroup parent;

    public SelectorOption(string display, SelectorGroup _parent, SelectorGroup group) {
        displayString = display;
        child = group;
        isData = false;
        parent = _parent;
        group.setParent(_parent);
    }
    
    public SelectorOption(string display, SelectorGroup _parent, string dataStr) {
        displayString = display;
        data = dataStr;
        isData = true;
        parent = _parent;
    }

    public override string ToString() {
        if(isData) {
            return "{\"display\":\"" + displayString + "\", \"data\":\"" + data + "\"}";
        } else {
            return "{\"display\":\"" + displayString + "\", \"child\":" + child.ToString() + "}";
        }
    }
}
