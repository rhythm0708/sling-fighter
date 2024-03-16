using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{
    // Variables.
    [SerializeField] Slider healthBarSlider;
    [SerializeField] RectTransform trailBar;

    // Enemy name.
    EnemyNames enemyNames;
    [SerializeField] TMP_Text enemyNameTextBox;

    DummyController dummyController;

    private float trailValue;

    public bool enemyAlive;

    void Start()
    {
        enemyAlive = true;

        // Get dummy GameObject.
        dummyController = GameObject.FindWithTag("Enemy")?.GetComponent<DummyController>();
        dummyController.SubscribeOnSlain(OnSlain);

        // Assign starting slider values.
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = dummyController.GetMaxHealth;
        healthBarSlider.value = dummyController.GetMaxHealth;

        // Set enemy name.
        enemyNames = this.GetComponentInChildren<EnemyNames>();
        enemyNameTextBox.text = enemyNames.CurrentEnemyName;
    }

    void Update()
    {
        // Update trail value.
        trailValue = Mathf.Lerp(
            trailValue,
            dummyController.GetHealth / dummyController.GetMaxHealth,
            1.0f - Mathf.Exp(-4.0f * Time.deltaTime)
            );
        trailBar.sizeDelta = new Vector2(trailValue * 1592f, 75f);

        // Update slider value.
        healthBarSlider.value = dummyController.GetHealth;
    }

    void OnSlain()
    {
        healthBarSlider.value = 0;
        enemyAlive = false;
    }
}
