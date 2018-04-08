using UnityEngine.UI;
using UnityEngine;

public class Pulse : MonoBehaviour {
    public float pulseSpeed, pulseStrength;
    private Image image;
    public Color32 pulseColor;
    private float alpha;
    private bool dim;

    // Use this for initialization
    void Start () {
        alpha = 255;        
        image = GetComponent<Image>();
        pulseColor.a = 255;
    }
	
	// Update
	void Update () {
        if (alpha >= 254)
        {
            dim = true;

        }
        else if (alpha <= (255 - pulseStrength * 10))
        {
            dim = false;

        }

        if (dim)
        {
            alpha = alpha - Time.deltaTime * (10f * pulseSpeed);
        }
        else
        {
            alpha = alpha + Time.deltaTime * (10f * pulseSpeed);
        }

        pulseColor.a = (byte)alpha;
        image.color = pulseColor;
    }
}
