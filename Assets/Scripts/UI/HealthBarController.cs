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
    private Vector2 initialDimensions;

    void Start()
    {
        dummy = GameManager.Instance.dummy;
        trailValue = 1.0f;
        
        // Assign starting slider values.
        healthBarSlider.minValue = 0;
        healthBarSlider.maxValue = dummy.maxHealth;
        healthBarSlider.value = dummy.maxHealth;
        initialDimensions = new Vector2(trailBar.rect.width, trailBar.rect.height);

        dummyName.text = dummy.displayName;
    }

    void Update()
    {
        // Update trail value.
        if (!GameManager.Instance.dummy.inKnockback)
            {
            trailValue = Mathf.Lerp
            (
                trailValue,
                dummy.health / dummy.maxHealth,
                1.0f - Mathf.Exp(-4.0f * Time.deltaTime)
            );
            trailBar.sizeDelta = new Vector2(trailValue * initialDimensions.x, initialDimensions.y);
        }

        // Update slider value.
        healthBarSlider.value = dummy.health;
    }
}
