using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Reference to ScoreManager and TimeManager and playerHitbox.
    ScoreManager scoreManager;
    TimeManager timeManager;
    Hitbox playerHitbox;

    // Text objects.
    [SerializeField] TMP_Text totalScoreText;
    [SerializeField] TMP_Text comboScoreText;
    [SerializeField] TMP_Text multiplierText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text livesText;

    // On-screen graphics.
    [SerializeField] List<Image> multiplierGraphics;
    [SerializeField] List<Image> lifeGraphics;
    [SerializeField] List<Color32> multiplierColors;
    [SerializeField] Color32 defaultColor;
    [SerializeField] float textPopRatio;
    [SerializeField] float textShrinkRatio;
    [SerializeField] float popDelayRate;

    [Obsolete]
    private void Start()
    {
        // Obtain reference to ScoreManager.
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        timeManager = GameObject.Find("Main HUD").transform.FindChild("Timer").gameObject.GetComponent<TimeManager>();
        playerHitbox = GameObject.Find("Player").transform.Find("Hitbox").gameObject.GetComponent<Hitbox>();

        // Subscribe to playerHitbox OnHit.
        playerHitbox.SubscribeOnHit(TimerTextPopOut);

        // Set score to 000 000.
        totalScoreText.text = "Score: 000 000";

        // Set multiplier to x0.
        multiplierText.text = "x0";
        foreach (Image graphic in multiplierGraphics)
        {
            graphic.material.color = defaultColor;
        }
    }

    private void Update()
    {
        // Update HUD.
        if (scoreManager != null)
        {
            UpdateTotalScore(scoreManager.TotalScore);
            UpdateComboScore(scoreManager.ComboScore);
            UpdateMultiplier(scoreManager.MultiplierValue);
        }
        else
        {
            Debug.LogException(new Exception("Cannot find ScoreManager script / scoreManager is null."));
        }

        if(timeManager != null)
        {
            UpdateTime(timeManager.CurrentTime);
        }
    }

    private void UpdateTotalScore(int totalScore)
    {
        // Update text.
        totalScoreText.text = "Score: " + totalScore.ToString("000 000");
    }

    private void UpdateComboScore(int comboScore)
    {
        // Update text.
        comboScoreText.text = "+ " + comboScore;
    }

    private void UpdateMultiplier(int multiplier)
    {
        // Update text.
        if(0 <= multiplier && multiplier <= multiplierGraphics.Count)
        {
            multiplierText.text = "x" + multiplier;
        }
        else
        {
            Debug.LogException(new System.Exception($"Multiplier {multiplier} out of bounds."));
        }

        // Update graphic.
        for (int i = 0; i < multiplierGraphics.Count; i++)
        {
            if(i<multiplier)
            {
                multiplierGraphics[i].color = multiplierColors[i];
                // Debug.Log("Color.");
            }
            else
            {
                multiplierGraphics[i].color = defaultColor;
            }
        }
    }

    private void UpdateTime(int time)
    {
        // Update time.
        if (time>=100)
        {
            timerText.text = time.ToString("000");
        }
        else
        {
            timerText.text = time.ToString("00");
        }
    }

    private void UpdateLife(int lives)
    {
        // Update text.
        if (0 <= lives && lives <= lifeGraphics.Count)
        {
            livesText.text = "Lives: " + lives;
        }
        else
        {
            Debug.LogException(new System.Exception($"Lives {lives} out of bounds."));
        }

        // Update graphic.
        for (int i = 0; i < lifeGraphics.Count; i++)
        {
            if (i < lives)
            {
                lifeGraphics[i].color = new Color32(255, 255, 255, 255);
            }
            else
            {
                lifeGraphics[i].color = new Color32(255, 255, 255, 0);
            }
        }
    }

    private void TimerTextPopOut(Collider collider)
    {
        // Makes timer text "pop out" if time is increased.
        var originalFontSize = timerText.fontSize;

        timerText.fontSize *= textPopRatio;
        StartCoroutine(PopText(timerText, originalFontSize, popDelayRate));
    }

    private IEnumerator PopText(TMP_Text textbox, float originalFontSize, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Pops and reduces font size.
        while (timerText.fontSize >= originalFontSize)
        {
            timerText.fontSize *= textShrinkRatio;
            yield return new WaitForSeconds(delay);
        }
        timerText.fontSize = originalFontSize;
    }
}