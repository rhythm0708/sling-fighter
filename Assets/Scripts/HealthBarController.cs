using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // Variables.
    [SerializeField] Slider healthBarSlider;
    [SerializeField] string enemyName;
    DummyController dummy;

    public bool enemyAlive;

    void Start()
    {
        // Get dummy GameObject.
        dummy = GameObject.Find("DummyEnemy")?.GetComponent<DummyController>();
        enemyAlive = true;

        // Assign starting slider values.
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = dummy.GetMaxHealth;
        healthBarSlider.value = dummy.GetMaxHealth;
    }

    void Update()
    {
        // Update slider value.
        healthBarSlider.value = dummy.GetHealth;

        // Enemy dies when healthBar is at zero.
        if(healthBarSlider.value <= 0)
        {
            enemyAlive = false;
        }
    }
}
