using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject sling;
    [SerializeField] private GameObject sidestep;

    [SerializeField] private bool promptSling = false;
    [SerializeField] private bool promptSidestep = false;

    [SerializeField] private int slingsTillSidestepPrompt = 5;
    private int slingsSinceLastSidestep;

    float slingPercent;
    List<SpriteRenderer> slingSprites;
    TMP_Text slingText;

    float sidestepPercent;
    List<SpriteRenderer> sidestepSprites;
    TMP_Text sidestepText;

    void Start()
    {
        slingSprites = sling.GetComponentsInChildren<SpriteRenderer>().ToList();
        slingText = sling.GetComponentInChildren<TMP_Text>();
        slingPercent = 0.0f;

        sidestepSprites = sidestep.GetComponentsInChildren<SpriteRenderer>().ToList();
        sidestepText = sidestep.GetComponentInChildren<TMP_Text>();
        sidestepPercent = 0.0f;

        if (promptSidestep)
        {
            slingsSinceLastSidestep = slingsTillSidestepPrompt;
        }
        else
        {
            slingsSinceLastSidestep = 0;
        }

        GameManager.Instance.player.movement.SubscribeOnSling(() =>
        {
            promptSling = false;
            slingsSinceLastSidestep += 1;
            if (slingsSinceLastSidestep >= slingsTillSidestepPrompt)
            {
                promptSidestep = true;
            }
        });

        GameManager.Instance.player.movement.SubscribeOnSidestep(() => {
            slingsSinceLastSidestep = 0;
            promptSidestep = false;
        });

        sling.SetActive(true);
        sidestep.SetActive(true);
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        Camera camera = Camera.main;

        if (camera != null)
        {
            Vector3 directionToCamera = transform.position - camera.transform.position;
            directionToCamera.y = 0.0f;
            directionToCamera = directionToCamera.normalized;

            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }

        if (promptSling)
        {
            slingPercent = Mathf.Lerp
            (
                slingPercent, 
                1.0f,
                1.0f - Mathf.Exp(-32.0f * Time.deltaTime)
            );
        }
        else
        {
            slingPercent = Mathf.Lerp
            (
                slingPercent, 
                0.0f,
                1.0f - Mathf.Exp(-32.0f * Time.deltaTime)
            );
        }
        foreach (SpriteRenderer sprite in slingSprites)
        {
            sprite.color = new Color(1.0f, 1.0f, 1.0f, slingPercent);
        }
        slingText.alpha = slingPercent;

        if (
            promptSidestep && 
            !promptSling && 
            GameManager.Instance.player.movement.GetState() == PlayerMovement.State.Move
        )
        {
            sidestepPercent = Mathf.Lerp
            (
                sidestepPercent, 
                1.0f,
                1.0f - Mathf.Exp(-32.0f * Time.deltaTime)
            );
        }
        else
        {
            sidestepPercent = Mathf.Lerp
            (
                sidestepPercent, 
                0.0f,
                1.0f - Mathf.Exp(-32.0f * Time.deltaTime)
            );
        }
        foreach (SpriteRenderer sprite in sidestepSprites)
        {
            sprite.color = new Color(1.0f, 1.0f, 1.0f, sidestepPercent);
        }
        sidestepText.alpha = sidestepPercent;

    }
}
