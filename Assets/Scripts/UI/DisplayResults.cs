using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class DisplayResults : MonoBehaviour
{
    [SerializeField] List<TMP_Text> resultsScreenText;
    [SerializeField] List<Vector2> resultsScreenTextPos;

    // Animating the text boxes moving right.
    [SerializeField] private float rightLength;
    private float rightAnimationTime1;
    private float rightAnimationTime2;
    private float rightAnimationTime3;
    [SerializeField] float rightAnimationLength;

    // Time stamps when the text boxes will be animated.
    [SerializeField] float timeStampResults;
    [SerializeField] float timeStampWaveScore;
    [SerializeField] float timeStampTotalScore;

    // Original and final color.
    [SerializeField] Color transparentColor;
    [SerializeField] Color originalColor;

    void Start()
    {
        rightAnimationTime1 = 0.0f;
        rightAnimationTime2 = 0.0f;
        rightAnimationTime3 = 0.0f;

        // Set location, make all text transparent to start.
        foreach (TMP_Text textBox in resultsScreenText)
        {
            var index = resultsScreenText.IndexOf(textBox);
            resultsScreenTextPos[index] = textBox.rectTransform.localPosition;
            textBox.color = transparentColor;

            // Prepare for the animation.
            textBox.gameObject.SetActive(true);
            textBox.rectTransform.localPosition = resultsScreenTextPos[index] + new Vector2(-rightLength, 0f);
        }
    }

    void Update()
    {
        // Update score after wave is complete.
        if(GameManager.Instance.clearedWave == true)
        {
            UpdateScore();
        }

        var clearTimer = GameManager.Instance.clearTimer;
        if (clearTimer > 5.0f)
        {
            // Debug.Log("cleared");
            // Reveal results line.
            if(clearTimer >= timeStampResults)
            {
                rightAnimationTime1 += Time.deltaTime / Time.timeScale;
                StartCoroutine(RevealText(resultsScreenText[0], rightAnimationTime1));
            }
            // Reveal wave score.
            if(clearTimer >= timeStampWaveScore)
            {
                rightAnimationTime2 += Time.deltaTime / Time.timeScale;
                StartCoroutine(RevealText(resultsScreenText[1], rightAnimationTime2));
                StartCoroutine(RevealText(resultsScreenText[2], rightAnimationTime2));
            }
            // Reveal total score.
            if(clearTimer >= timeStampTotalScore)
            {
                rightAnimationTime3 += Time.deltaTime / Time.timeScale;
                StartCoroutine(RevealText(resultsScreenText[3], rightAnimationTime3));
                StartCoroutine(RevealText(resultsScreenText[4], rightAnimationTime3));
            }
        }
    }
    
    // Update score before displaying it on the results screen.
    private void UpdateScore()
    {
        // Set wave number.
        Scene scene = SceneManager.GetActiveScene();
        resultsScreenText[1].text = "Wave " + Convert.ToInt32(scene.name.Remove(0, 4)) + " Score";

        // Set wave score.
        if(GameManager.Instance.Score >= 10000)
        {
            resultsScreenText[2].text = GameManager.Instance.Score.ToString("00 000");
        }
        else if(GameManager.Instance.Score >= 1000)
        {
            resultsScreenText[2].text = GameManager.Instance.Score.ToString("0 000");
        }
        else
        {
            resultsScreenText[2].text = GameManager.Instance.Score.ToString("000");
        }

        // Set total score.
        if(GameManager.Instance.Score >= 1000000)
        {
            resultsScreenText[4].text = GameManager.Instance.TotalScore.ToString("0 000 000");
        }
        else if (GameManager.Instance.Score >= 100000)
        {
            resultsScreenText[4].text = GameManager.Instance.TotalScore.ToString("000 000");
        }
        else if (GameManager.Instance.Score >= 10000)
        {
            resultsScreenText[4].text = GameManager.Instance.TotalScore.ToString("00 000");
        }
        else if (GameManager.Instance.Score >= 1000)
        {
            resultsScreenText[4].text = GameManager.Instance.TotalScore.ToString("0 000");
        }
        else
        {
            resultsScreenText[4].text = GameManager.Instance.TotalScore.ToString("000");
        }
    }

    private IEnumerator RevealText(TMP_Text textbox, float rightAnimationTime)
    {
        // Apply the animation.
        float t = rightAnimationTime / rightAnimationLength;
        int index = resultsScreenText.IndexOf(textbox);

        if(t>=1)
        {
            textbox.color = originalColor;
            yield return null;
        }
        else
        {
            Color lerpedColor = Color.Lerp(transparentColor, originalColor, t);
            textbox.color = lerpedColor;

            textbox.rectTransform.localPosition = resultsScreenTextPos[index] + new Vector2(rightLength, 0f) * t;
            yield return null;
        }
    }
}
