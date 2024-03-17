using UnityEngine;
using UnityEngine.UI;

public class ClearFader : MonoBehaviour
{
    Image image;
    [SerializeField] float introLength = 0.25f;
    float introTimer;

    void Start()
    {
        image = GetComponent<Image>();
        introTimer = introLength;
    }

    void Update()
    {
        if (introTimer > 0.0f)
        {
            introTimer -= Time.deltaTime;
            float opacity = introTimer / introLength;
            image.color = new Color(1.0f, 1.0f, 1.0f, opacity);
        }
        else if (GameManager.Instance.clearTimer > 4.0f)
        {
            float opacity = GameManager.Instance.clearTimer - 4.0f;
            image.color = new Color(1.0f, 1.0f, 1.0f, opacity);
        }
    }
}
