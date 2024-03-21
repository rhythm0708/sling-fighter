using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboController : MonoBehaviour
{
    [SerializeField] TMP_Text comboText;
    private int prevCombo;
    private float timeStamp;
    [SerializeField] float resetTime;

    void Start()
    {
        timeStamp = 0.0f;
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
            }
            else
            {
                // Combo should not immediately reset to zero.
                comboText.text = prevCombo.ToString();
            }
        }
    }

    private IEnumerator PopOutText()
    {

    }

}
