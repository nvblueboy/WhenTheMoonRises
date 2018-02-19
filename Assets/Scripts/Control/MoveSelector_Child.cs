using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSelector_Child : MonoBehaviour {

    public SelectorType type;

    public List<Text> labels;

    public int currentSelection;
    private int oldSelection;


    public List<SelectorNode> options;

    public void Start() {
        currentSelection = 0;
        oldSelection = -1;
    }

    public void Update() {
        if (oldSelection != currentSelection) {
            if(options != null) {
                updateText();
                oldSelection = currentSelection;
            }
        }
    }

    public void input(Direction dir) {
        if (type == SelectorType.Loop) {
            if (dir == Direction.Up) {
                currentSelection = (currentSelection - 1) % options.Count;
            } else if (dir == Direction.Down) {
                currentSelection = (currentSelection + 1) % options.Count;
            }
        } else if (type == SelectorType.Grid) {
            if(dir == Direction.Up || dir == Direction.Down) {
                currentSelection = (currentSelection + 2) % 4;
            } else {
                if(currentSelection == 1 || currentSelection == 3) {
                    currentSelection--;
                } else {
                    currentSelection++;
                }
            }
        } else if (type == SelectorType.In_Place) {
            if (dir == Direction.Right && currentSelection < options.Count - 1) {
                currentSelection++;
            } else if (dir == Direction.Left && currentSelection > 0) {
                currentSelection--;
            }
            Debug.Log(currentSelection);
        }
        updateText();
    }


    public void updateText() {
        if (type == SelectorType.Loop) {
            labels[0].text = getCircular<SelectorNode>(options, currentSelection - 1).display_name;
            labels[1].text = getCircular<SelectorNode>(options, currentSelection).display_name;
            labels[2].text = getCircular<SelectorNode>(options, currentSelection + 1).display_name;
        } else if (type == SelectorType.Grid) {
            if (options.Count < 4) {
                Debug.LogWarning("There's less than 4 options in the current selector, but it's a grid.");
            }
            labels[0].text = getCircular<SelectorNode>(options, 0).display_name;
            labels[1].text = getCircular<SelectorNode>(options, 1).display_name;
            labels[2].text = getCircular<SelectorNode>(options, 2).display_name;
            labels[3].text = getCircular<SelectorNode>(options, 3).display_name;
            labels[currentSelection].text = "<" + labels[currentSelection].text + ">";
        } else if (type == SelectorType.In_Place) {
            labels[0].text = getCircular<SelectorNode>(options, currentSelection).display_name;
        }
    }

    public void setOptions(List<SelectorNode> _options) {
        options = _options;
        foreach(SelectorNode child in options) {
            Debug.Log(child);
        }
        updateText();
    }


    public T getCircular<T>(List<T> list, int index) {
        int len = list.Count;
        if(index >= len) {
            return getCircular<T>(list, index - len);
        }
        if(index < 0) {
            return getCircular<T>(list, index + len);
        }
        return list[index];
    }
}
