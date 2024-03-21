using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboController : MonoBehaviour
{
    [SerializeField] TMP_Text comboText;

    void Start()
    {
        
    }

    void Update()
    {
        // Update combo text.
        comboText.text = GameManager.Instance.player.comboCount.ToString();
    }
}
