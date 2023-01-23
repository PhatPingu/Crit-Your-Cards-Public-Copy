using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreatureHealthSlider : MonoBehaviour
{
    [SerializeField] CreatureProperty CreatureProperty;
    [SerializeField] Slider healthSlider;

    void FixedUpdate()
    {
        healthSlider.maxValue = CreatureProperty.currentCreatureMaxHealth;
        healthSlider.value = CreatureProperty.currentCreatureHealth;
    }
}
