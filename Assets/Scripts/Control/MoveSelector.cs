﻿using System.Collections;
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

    private SelectorNode root;

    private SelectorNode current;

    public PlayerCharacter fighter;

    public GameObject loopObject;
    public GameObject gridObject;
    public GameObject inPlaceObject;

    public GameObject currentMenu;

    private Dictionary<SelectorType, GameObject> selectorMap;

    public SelectorType currentType;

    //For parsing input:
    private float oldHoriz;
    private float oldVert;
    private float oldJump;
    private float oldFire;
    private bool horiz_hasFallen = true;
    private bool vert_hasFallen = true;
    private bool jump_hasFallen = true;
    private bool fire_hasFallen = true;

    public void Start() {
        if(fighter == null) {
            Debug.LogWarning("There is no fighter class attached to the move selector. I'm creating a fake player for testing purposes.");
            fighter = new PlayerCharacter();
            fighter.testInventory();
        }

        selectorMap = new Dictionary<SelectorType, GameObject>();
        selectorMap[SelectorType.Loop] = loopObject;
        selectorMap[SelectorType.Grid] = gridObject;
        selectorMap[SelectorType.In_Place] = inPlaceObject;

        root = new SelectorNode("root", "ROOT", new List<SelectorNode>(), SelectorType.Grid);

        SelectorNode moves = new SelectorNode("moves", "Moves", new List<SelectorNode>(), SelectorType.Loop);
        root.addChild(moves);

        foreach (Move m in fighter.getMoves()) {
            moves.addChild(new SelectorNode(m.name, m.name));
        }

        SelectorNode items = new SelectorNode("items", "Items", new List<SelectorNode>(), SelectorType.Loop);
        root.addChild(items);
        
        if (fighter.inventory.Length > 0) {
            foreach (string item in fighter.inventory) {
                //TODO: Update this loop once inventory gets overhauled.
                items.addChild(new SelectorNode(item, item));
            }
        }

        SelectorNode options = new SelectorNode("options", "Options", new List<SelectorNode>(), SelectorType.Loop);
        root.addChild(options);

        SelectorNode special = new SelectorNode("special", "Special", new List<SelectorNode>(), SelectorType.Loop);
        root.addChild(special);

        current = root;

        Debug.Log(root);

        updateDisplay();
    }

    public void updateDisplay() {
        currentType = current.type;
        foreach(KeyValuePair<SelectorType, GameObject> kvp in selectorMap) {
            if (kvp.Key == currentType) {
                kvp.Value.GetComponent<MoveSelector_Child>().setOptions(current.children);
                kvp.Value.SetActive(true);
                currentMenu = kvp.Value;
            } else {
                kvp.Value.SetActive(false);
            }
        }
    }

    public void Update() {
        //Parse input from the user.
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");
        float fire = Input.GetAxis("Fire3");

        float horiz_mag = Mathf.Abs(horiz);
        float vert_mag = Mathf.Abs(vert);

        if (horiz_mag < Mathf.Abs(oldHoriz)) {
            horiz_hasFallen = true;
        }

        if (vert_mag < Mathf.Abs(oldVert)) {
            vert_hasFallen = true;
        }

        Direction dir = Direction.None;

        if (horiz_mag > .5 && vert_mag < .5 && horiz_hasFallen) {
            if (horiz > 0 && horiz > oldHoriz) {
                dir = Direction.Right;
                horiz_hasFallen = false;
            } else if (horiz < 0 && horiz < oldHoriz) {
                dir = Direction.Left;
                horiz_hasFallen = false;
            }
        } else if (horiz_mag < .5 && vert_mag > .5 && vert_hasFallen) {
            if (vert > 0 && vert > oldVert) {
                dir = Direction.Up;
                vert_hasFallen = false;
            } else if (vert < 0 && vert < oldVert) {
                dir = Direction.Down;
                vert_hasFallen = false;
            }
        }

        if (dir != Direction.None) {
            currentMenu.GetComponent<MoveSelector_Child>().input(dir);
        }

        if (jump < oldJump) {
            jump_hasFallen = true;
        }

        if (jump > oldJump && jump_hasFallen) {
            jump_hasFallen = false;
            int sel = currentMenu.GetComponent<MoveSelector_Child>().currentSelection;
            SelectorNode selected = current.children[sel];

            if(selected.hasChildren()) {
                if(selected.children.Count > 0) {
                    current = selected;
                    updateDisplay();
                }
            } else {
                Debug.Log("Chosen: " + selected);
            }
        }

        if (fire < oldFire) {
            fire_hasFallen = true;
        }

        if (fire > oldFire && fire_hasFallen) {
            fire_hasFallen = false;
            current = current.parent;
            updateDisplay();
        }

        oldHoriz = horiz;
        oldVert = vert;
        oldJump = jump;
        oldFire = fire;

        if (currentMenu.GetComponent<MoveSelector_Child>().type != currentType) {
            updateDisplay();
        }
    }

}

public enum SelectorType { Loop, Grid, In_Place }
public enum Direction { None, Left, Right, Up, Down }

public class SelectorNode {
    private bool isData;
    public string name;
    public string display_name;
    public List<SelectorNode> children;
    public SelectorType type;
    public SelectorNode parent;

    public SelectorNode(string _name, string _display_name) {
        isData = true;
        name = _name;
        display_name = _display_name;
    }

    public SelectorNode(string _name, string _display_name, List<SelectorNode> _children, SelectorType _type) {
        isData = false;
        name = _name;
        display_name = _display_name;
        children = _children;
        type = _type;
    }

    public void addChild(SelectorNode child) {
        child.parent = this;
        children.Add(child);
    }

    public bool hasChildren() {
        return !isData;
    }

    public override string ToString() {
        //This spits out a JSON string to display the structure of this node.
        string name_formatted = "\"name\":\"" + name + "\"";
        string display_name_formatted = "\"display_name\":\"" + display_name + "\"";

        string output = "{" + name_formatted + ", " + display_name_formatted;
        if (!isData) {
            output += ", \"type\":\"" + type.ToString() + "\"";

            List<string> childrenStrings = new List<string>();
            
            foreach(SelectorNode child in children) {
                childrenStrings.Add(child.ToString());
            }

            output += ", \"children\":[" + string.Join(", ", childrenStrings.ToArray()) + "]";
        }
        output += "}";
        return output;
    }
}