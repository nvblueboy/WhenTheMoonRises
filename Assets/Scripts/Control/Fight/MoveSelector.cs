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

    private SelectorNode root;

    private SelectorNode current;

    private SelectorNode items;    

    public GameObject loopObject;
    public GameObject gridObject;
    public GameObject inPlaceObject;

    public GameObject currentMenu;

    private Dictionary<SelectorType, GameObject> selectorMap;

    public SelectorType currentType;

    public bool takeControl;

    //For parsing input:
    private float oldHoriz;
    private float oldVert;
    private float oldJump;
    private float oldFire;
    private bool horiz_hasFallen = true;
    private bool vert_hasFallen = true;
    private bool jump_hasFallen = true;
    private bool fire_hasFallen = true;

    private Dictionary<string, object> selections;
    private Dictionary<string, string> types;

    public void Start() {
        takeControl = true;
        Debug.Log("Player in Move Selector: " + GameController.player.strength);
        /*if(GameController.player == null) {
            Debug.LogWarning("There is no fighter class attached to the move selector. I'm creating a fake player for testing purposes.");
            GameController.player = new PlayerCharacter();
            GameController.player.testInventory();
        }*/
        if(GameController.player.name == "[Test_inv]") {
            GameController.player.testInventory();
        }

        MoveUtils.InitMoves();

        selections = new Dictionary<string, object>();
        types = new Dictionary<string, string>();

        selectorMap = new Dictionary<SelectorType, GameObject>();
        selectorMap[SelectorType.Loop] = loopObject;
        selectorMap[SelectorType.Grid] = gridObject;
        selectorMap[SelectorType.In_Place] = inPlaceObject;

        root = new SelectorNode("root", "ROOT", new List<SelectorNode>(), SelectorType.Grid);

        SelectorNode moves = new SelectorNode("moves", "Moves", new List<SelectorNode>(), SelectorType.Loop);
        root.addChild(moves);

        foreach (Move m in GameController.player.getMoves()) {
            selections.Add(m.name, MoveUtils.GetMove(m.name));
            types.Add(m.name, "move");
            moves.addChild(new SelectorNode(m.name, m.name));
        }

        items = new SelectorNode("items", "Items", new List<SelectorNode>(), SelectorType.Loop);
        root.addChild(items);
        
        if (GameController.player.inventory.Count > 0) {

            foreach (Item item in GameController.player.inventory) {
                Debug.Log(item.getName());
                selections.Add(item.getName(), item);
                types.Add(item.getName(), "item");
                items.addChild(new SelectorNode(item.getName(), item.getDisplayName()));
            }
        } 



        SelectorNode options = new SelectorNode("run", "Run");
        root.addChild(options);

        SelectorNode special = new SelectorNode("special", "Special", new List<SelectorNode>(), SelectorType.Loop);
        root.addChild(special);

        current = root;

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

        updateDescription();
    }

    public void Update() {
        //Parse input from the user.
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");
        float fire = Input.GetAxis("Close");

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

            //Process change in description if it's needed.

            updateDescription();
        }

        if (jump < oldJump) {
            jump_hasFallen = true;
        }

        if (jump > oldJump && oldJump == 0 && takeControl) {
            jump_hasFallen = false;
            int sel = currentMenu.GetComponent<MoveSelector_Child>().currentSelection;
            SelectorNode selected = current.children[sel];

            if(selected.hasChildren()) {
                if(selected.children.Count > 0) {
                    current = selected;
                    updateDisplay();
                }
            } else {
                if (selected.name == "run") {
                    GameController.player.addSelectedMove("Run");
                }
                object selectedObj = selections[selected.name];

                if (types[selected.name] == "item") {
                    Item selectedItem = (Item)selections[selected.name];
                    selectedItem.affectPlayer(GameController.player);

                    GameController.player.removeItemByName(selectedItem.getName());
                    items.removeChildByName(selectedItem.getName());

                    GameController.player.addSelectedMove("Item Use");

                    current = root;

                } else if (types[selected.name] == "move") {
                    Debug.Log(selected.name);
                    GameController.player.addSelectedMove(selected.name);

                    current = root;
                }

                updateDisplay();
            }
        }

        if (fire < oldFire) {
            fire_hasFallen = true;
        }

        if (fire > oldFire && fire_hasFallen) {
            fire_hasFallen = false;
            if(current != root) {
                current = current.parent;
            }
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
    public void updateDescription() {
        MoveSelector_Child kiddo = currentMenu.GetComponent<MoveSelector_Child>();
        int sel = kiddo.currentSelection;
        SelectorNode selected = current.children[sel];

        if(types.ContainsKey(selected.name)) {
            if(types[selected.name] == "move") {
                Move m = MoveUtils.GetMove(selected.name);
                kiddo.setDescription(m.description);
            } else if (types[selected.name] == "item") {
                Item i = (Item)selections[selected.name];
                kiddo.setDescription(i.description);
            }
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

    public void removeChildByName(string name) {
        foreach(SelectorNode node in children) {
            if (node.name == name) {
                children.Remove(node);
                return;
            }
        }
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