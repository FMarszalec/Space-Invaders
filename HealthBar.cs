using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Slider healthSlider;
    public Gradient gradient;
    public Image healthBarFill;

    public void SetTotalHealth (int healthPoints) {
        healthSlider.maxValue = healthPoints;
        healthSlider.value = healthPoints;
        gradient.Evaluate(1f);
    }

    public void SetHealth (int healthPoints) {
        healthSlider.value = healthPoints;
        healthBarFill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}
