using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private CharacterStats stats => GetComponentInParent<CharacterStats>();
    private RectTransform myTransform => GetComponent<RectTransform>();
    private Slider slider => GetComponentInChildren<Slider>();

    private void Start()
    {
        slider.maxValue = stats.GetMaxHealthValue();
        slider.value = stats.currentHealth;

        entity.onFlipped += FlipUI;
        stats.onHealthChanged += UpdateHealthUI;
        stats.onMaxHealthChanged += UpdateMaxHealthUI;
    }

    private void UpdateHealthUI() => slider.value = stats.currentHealth;

    private void UpdateMaxHealthUI() => slider.maxValue = stats.GetMaxHealthValue();

    private void FlipUI() => myTransform.Rotate(0, 180, 0);
}
