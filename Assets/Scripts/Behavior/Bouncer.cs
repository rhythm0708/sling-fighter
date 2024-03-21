using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private Transform model;
    [SerializeField] private float bounceDelta = 5.0f;
    [SerializeField] private float shrinkSpeed = 16.0f;
    private Vector3 initialScale;
    
    void Start()
    {
        initialScale = model.localScale;
    }

    public void Bounce()
    {
        model.localScale = initialScale + new Vector3(bounceDelta, 0.0f, bounceDelta);
    }

    void Update()
    {
        model.localScale = Vector3.Lerp
        (
            model.localScale, 
            initialScale,
            1.0f - Mathf.Exp(-shrinkSpeed * Time.deltaTime)
        );
    }
}