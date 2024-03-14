using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float initialTime = 90.0f;
    [SerializeField] private TMP_Text timerText;
    private Hurtbox hurtbox;
    private float time;

    Vector3 initialTimerPosition;
    private float hitTimer;

    void Start()
    {
        hurtbox = GetComponentInChildren<Hurtbox>();
        hurtbox.SubscribeOnHurt(OnHurt);
        time = initialTime;
        initialTimerPosition = timerText.rectTransform.position;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0.0f)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
            time = 0.0f;
        }

        timerText.text = ((int)time).ToString();

        hitTimer = Mathf.Lerp
        (
            hitTimer, 
            0.0f,
            1.0f - Mathf.Exp(-8.0f * Time.deltaTime)
        );
        timerText.rectTransform.position = initialTimerPosition + new Vector3(0.0f, hitTimer * -20.0f);
    }

    public void SubtractTime(float amount)
    {
        time -= amount;
        hitTimer = 1.0f;
    }

    void OnHurt(Collider param, Hitbox.Properties properties, Vector3 direction)
    {
        time -= properties.damage;
        hitTimer = 1.0f;
    }
}
