using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField] TMP_Text numberText;

    // To animate the timer going up and down, we separate
    // the two states using a boolean. Each state then has
    // its own timer
    bool animateDown = false;
    [SerializeField] private float upLength = 0.25f;
    private float upAnimationTime;
    [SerializeField] private float downLength = 0.05f;
    private float downAnimationTime;

    private Vector2 initialPos;

    void Start()
    {
        upAnimationTime = 0.0f;
        downAnimationTime = 0.0f;
        initialPos = numberText.rectTransform.localPosition;

        // When time is subtracted, go into the down state
        GameManager.Instance.SubscribeOnSubtractTime(() => {
            animateDown = true;
            downAnimationTime = 0.0f;
        });
    }

    void Update()
    {
        if (animateDown)
        {
            downAnimationTime += Time.deltaTime;
            if (downAnimationTime >= downLength)
            {
                // Go into the up state once the down state is complete
                downAnimationTime = downLength;
                animateDown = false;
                upAnimationTime = 0.0f;
            }

            // Apply the animation
            float t = downAnimationTime / downLength;
            numberText.alpha = 1.0f - t;
            numberText.rectTransform.localPosition = initialPos + new Vector2(0.0f, -50.0f) * t;
        }
        else
        {
            upAnimationTime += Time.deltaTime;
            if (upAnimationTime > upLength)
            {
                upAnimationTime = upLength;
            }


            float t = upAnimationTime / upLength;
            // Ease out of the animation
            t = Mathf.Pow(1.0f - t, 2.0f);

            // Apply the animation.
            numberText.alpha = 1.0f - t;
            numberText.rectTransform.localPosition = initialPos + new Vector2(0.0f, -50.0f) * t;
            numberText.text = ((int)GameManager.Instance.timer).ToString();
        }
    }
}
