using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour {

    public Slider ammoSlider;
    public Gradient gradient;
    public Image ammoBarFill;

    public void SetTotalAmmo (int fullClip) {
        ammoSlider.maxValue = fullClip;
        ammoSlider.value = fullClip;
        gradient.Evaluate(fullClip);
    }

    public void SubstractAmmo() {
        ammoSlider.value -= 1;
        ammoBarFill.color = gradient.Evaluate(ammoSlider.normalizedValue);
    }

    public void SetAmmo (int ammo) {
        ammoSlider.value = ammo;
        ammoBarFill.color = gradient.Evaluate(ammoSlider.normalizedValue);
    }
}
