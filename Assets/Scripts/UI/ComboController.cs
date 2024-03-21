using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboController : MonoBehaviour
{
    [SerializeField] TMP_Text comboText;
    [SerializeField] Color originalTextColor;
    [SerializeField] Color waveEndTextColor;
    private int prevCombo;
    private float timeStamp;
    [SerializeField] float resetTime;

    // Pop out animation.
    [SerializeField] float animationTime;
    [SerializeField] float animationLength;
    [SerializeField] float minFontSize;
    [SerializeField] float maxFontSize;


    void Start()
    {
        timeStamp = 0.0f;
        comboText.color = originalTextColor;

        GameManager.Instance.dummy.SubscribeOnHitByPlayer(AnimateNumber);
        GameManager.Instance.dummy.SubscribeOnSlain(FreezeCombo);

    }

    void Update()
    {
        var comboCount = GameManager.Instance.player.comboCount;

        if(comboCount >= prevCombo)
        {
            // Update combo text.
            comboText.text = comboCount.ToString();

            prevCombo = comboCount;
            timeStamp = 0;
        }
        else
        {
            timeStamp += Time.deltaTime;
            if(timeStamp >= resetTime)
            {
                prevCombo = comboCount;
                timeStamp = 0;

                // Reset.
                StartCoroutine(ShrinkNumber());
            }
            else
            {
                // Combo should not immediately reset to zero.
                comboText.text = prevCombo.ToString();
            }
        }
    }

    private void AnimateNumber()
    {
        /*
        var t = animationTime / animationLength;
        comboText.fontSize = minFontSize;
        comboText.color = originalTextColor;
        
        while (t<1)
        {
            // Pop out.
            var textFont = Mathf.Lerp(minFontSize, maxFontSize, t);
            comboText.fontSize = textFont;

            // Increase opacity.
            var opacity = Mathf.Lerp(originalTextColor.a, 255f, t);
            comboText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, opacity);

            animationTime += Time.deltaTime;
            t = animationTime / animationLength;
        }
        */
        animationTime = 0f;
        comboText.fontSize = maxFontSize;
        comboText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, 255f);
    }

    private IEnumerator ShrinkNumber()
    {
        /*
        var t = animationTime / animationLength;

        while (t < 1)
        {
            // Return size.
            var textFont = Mathf.Lerp(maxFontSize, minFontSize, t);
            comboText.fontSize = textFont;

            // Fade opacity.
            var opacity = Mathf.Lerp(255f, originalTextColor.a, t);
            comboText.color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, opacity);

            animationTime += Time.deltaTime;
            t = animationTime / animationLength;
        }
        */
        animationTime = 0;
        comboText.fontSize = minFontSize;
        comboText.color = originalTextColor;

        yield return null;
    }

    private void FreezeCombo()
    {
        comboText.color = waveEndTextColor;
        this.enabled = false;
    }
}
