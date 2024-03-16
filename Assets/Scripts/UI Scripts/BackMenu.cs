using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackMenu : MonoBehaviour
{
    [SerializeField] private Material mat;
    private Color startingColor;
    private bool startDetected = false;
    private float triggerTimer = 0f;
    private float triggerDuration = 1.5f;
    [SerializeField] private Slider backLoading;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        startingColor = mat.color;

        // Hide the loading slider initially
        backLoading.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (startDetected)
        {
            // Increment the timer while the player is in the trigger zone
            triggerTimer += Time.deltaTime;

            // Update loading progress
            backLoading.value = Mathf.Clamp01(triggerTimer / triggerDuration);

            // Check if the timer exceeds the desired duration
            if (triggerTimer >= triggerDuration)
            {
                // Switch scene after 1.5 seconds
                StartCoroutine(LoadNextScene());
                // Reset detection
                startDetected = false;
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        // Check if the colliding object is the ball
        if (collision.gameObject.CompareTag("PlayerInMenu") && !startDetected)
        {
            // Set startDetected to true to prevent multiple scene switches
            startDetected = true;

            // Change button color
            mat.color = Color.Lerp(startingColor, Color.white, Mathf.PingPong(Time.time, 1));

            // Restart the timer when the ball re-enters the trigger zone
            triggerTimer = 0f;

            // Show the loading slider
            backLoading.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        startDetected = false;

        // Reset button color on exit
        mat.color = startingColor; 

        // Hide the loading slider
        backLoading.gameObject.SetActive(false);
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(0.01f);

        // Load the next scene
        SceneManager.LoadScene("Main Menu");
    }
}
