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


    // HUD variables.
    [SerializeField] int scoreValue;
    [SerializeField] int multiplierValue;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void UpdateScore()
    {

    }

    private void UpdateMultiplier()
    {
        switch(multiplierValue)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            default:
                Debug.LogException(new System.Exception("Multiplier out of bounds."));
                break;
        }
    }
}