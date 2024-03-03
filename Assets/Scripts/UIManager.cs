using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    // Text objects.
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text multiplierText;

    // On-screen graphics.
    [SerializeField] List<Image> multiplierGraphics;
    [SerializeField] List<Color32> multiplierColors;
    [SerializeField] Color32 defaultColor;

    // HUD variables.
    [SerializeField] int scoreValue;
    [SerializeField] int multiplierValue;

    private void Start()
    {
        // Set score to 000 000.
        scoreText.text = "Score: 000 000";

        // Set multiplier to x0.
        multiplierText.text = "x0";
        foreach (Image graphic in multiplierGraphics)
        {
            graphic.material.color = defaultColor;
        }
    }

    private void Update()
    {
        
    }

    private void UpdateScore()
    {
        // Update text.
        scoreText.text = "Score: " + scoreValue.ToString("000 000");
    }

    private void UpdateMultiplier()
    {
        // Update text.
        if(0 <= multiplierValue && multiplierValue <= multiplierGraphics.Count)
        {
            multiplierText.text = "x" + multiplierValue;
        }
        else
        {
            Debug.LogException(new System.Exception($"Multiplier {multiplierValue} out of bounds."));
        }

        // Update graphic.
        for (int i = 0; i < multiplierGraphics.Count; i++)
        {
            if(i<multiplierValue)
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