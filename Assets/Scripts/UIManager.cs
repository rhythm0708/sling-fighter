using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Reference to ScoreManager and TimeManager.
    ScoreManager scoreManager;
    TimeManager timeManager;

    // Text objects.
    [SerializeField] TMP_Text totalScoreText;
    [SerializeField] TMP_Text comboScoreText;
    [SerializeField] TMP_Text multiplierText;
    [SerializeField] TMP_Text timerText;

    // On-screen graphics.
    [SerializeField] List<Image> multiplierGraphics;
    [SerializeField] List<Color32> multiplierColors;
    [SerializeField] Color32 defaultColor;

    [Obsolete]
    private void Start()
    {
        // Obtain reference to ScoreManager.
        scoreManager = GameObject.Find("Player").transform.FindChild("ScoreManager").gameObject.GetComponent<ScoreManager>();
        timeManager = GameObject.Find("Main HUD").transform.FindChild("Timer").gameObject.GetComponent<TimeManager>();

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
        if(time>=100)
        {
            timerText.text = time.ToString("000");
        }
        else
        {
            timerText.text = time.ToString("00");
        }
    }
}