using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Material mat;
    private Color startingColor;
    private bool startDetected = false;
    private float triggerTimer = 0f;
    private float triggerDuration = 1.5f;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        startingColor = mat.color;
    }

    void Update()
    {
        if (startDetected)
        {
            // Increment the timer while the ball is in the trigger zone
            triggerTimer += Time.deltaTime;

            // Check if the timer exceeds the desired duration
            if (triggerTimer >= triggerDuration)
            {
                // Switch scene after 3 seconds
                StartCoroutine(LoadNextScene());
                startDetected = false; // Reset detection
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
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        startDetected = false;

        // Reset button color on exit
        mat.color = startingColor; 
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(0.1f);

        // Load the next scene
        SceneManager.LoadScene("ThirdPersonTest");
    }
}
