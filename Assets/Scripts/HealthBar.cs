using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    [SerializeField] TextMeshProUGUI healthText;

    public void setMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        healthText.text = "Health: " + slider.value.ToString() + "/" + slider.maxValue.ToString(); 

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
