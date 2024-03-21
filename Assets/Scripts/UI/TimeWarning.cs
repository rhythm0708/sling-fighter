using TMPro;
using UnityEngine;

public class TimeWarning : MonoBehaviour
{
    [SerializeField] private float timeTillWarning = 5.0f;
    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (GameManager.Instance.timer <= timeTillWarning + 1.0f && !GameManager.Instance.clearedWave)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }
}
