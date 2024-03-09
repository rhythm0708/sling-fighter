using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Reference to ScoreManager.
    ScoreManager scoreManager;

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
                multiplierGraphics[i].material.color = multiplierColors[i];
            }
            else
            {
                multiplierGraphics[i].material.color = defaultColor;
            }
        }
    }
}