using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider healthBarSlider;
    [SerializeField] RectTransform trailBar;
    [SerializeField] TMP_Text dummyName;

    private DummyController dummy;
    private float trailValue;

    void Start()
    {
        dummy = GameManager.Instance.dummy;

        // Assign starting slider values.
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = dummy.maxHealth;
        healthBarSlider.value = dummy.maxHealth;

        dummyName.text = dummy.displayName;
    }

    void Update()
    {
        // Update trail value.
        trailValue = Mathf.Lerp
        (
            trailValue,
            dummy.health / dummy.maxHealth,
            1.0f - Mathf.Exp(-4.0f * Time.deltaTime)
        );
        trailBar.sizeDelta = new Vector2(trailValue * 1592f, 75f);

        // Update slider value.
        healthBarSlider.value = dummy.health;
    }
}
