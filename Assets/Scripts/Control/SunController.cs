using UnityEngine.UI;
using UnityEngine;

public class SunController : MonoBehaviour {
    Image sunMeter;

	// Start
	void Start () {
        sunMeter = GetComponent<Image>();        
	}

    // Update
    void Update() {
        updateSunMeter(ActionController.getActionCount());
    }
	
	// updateSunMeter
    public void updateSunMeter(int actions)
    {
        sunMeter.sprite = Resources.Load<Sprite>(string.Format(
            Constants.SunMeterPath, actions));
    }
}
