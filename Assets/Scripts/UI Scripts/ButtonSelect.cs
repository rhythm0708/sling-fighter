using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    [SerializeField] private Button primaryButton;

    void Start()
    {
        primaryButton.Select();
    }

    void Update()
    {
        // Check if the primary button is not selected and the mouse is clicked
        if (!primaryButton.gameObject.Equals(EventSystem.current.currentSelectedGameObject) && Input.GetMouseButtonDown(0))
        {
            // Select the primary button again
            primaryButton.Select();
        }
        else if (!primaryButton.gameObject.Equals(EventSystem.current.currentSelectedGameObject) && Input.GetMouseButtonDown(1))
        {
            // Select the primary button again
            primaryButton.Select();
        }
    }
}
