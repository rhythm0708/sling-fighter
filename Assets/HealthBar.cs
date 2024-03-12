using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private RectTransform rootRect;
    private float screenWidth;
    private float screenHeight;
    [SerializeField] private float margin = 100.0f;
    [SerializeField] private RectTransform healthRect;
    [SerializeField] private RectTransform backgroundRect;
    [SerializeField] private RectTransform trailRect;
    [SerializeField] private HealthComponent bossHealth;
    private float trailValue = 1.0f;

    void Start()
    {
        rootRect = transform.root.GetComponent<RectTransform>();
    }

    void Update()
    {
        screenWidth = rootRect.rect.width;
        screenHeight = rootRect.rect.height;

        transform.localPosition = new Vector3
        (
            -screenWidth * 0.5f + margin, 
            -screenHeight * 0.5f + margin, 
            0.0f
        );

        float maxWidth = screenWidth - margin * 2.0f;
        backgroundRect.sizeDelta = new Vector2(maxWidth, 50.0f);

        trailValue = Mathf.Lerp
        (
            trailValue, 
            bossHealth.ratio,
            1.0f - Mathf.Exp(-4.0f * Time.deltaTime)
        );
        trailRect.sizeDelta = new Vector2(maxWidth * trailValue, 50.0f);
        healthRect.sizeDelta = new Vector2(maxWidth * bossHealth.ratio, 50.0f);
    }
}
