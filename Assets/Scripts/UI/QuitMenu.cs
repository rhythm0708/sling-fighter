using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class QuitMenu : MonoBehaviour
{
    [SerializeField] private Material mat;
    private Color startingColor;
    private bool startDetected = false;
    private float triggerTimer = 0f;
    private float triggerDuration = 1.5f;
    [SerializeField] private Slider quitLoading;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        startingColor = mat.color;

        // Hide the loading slider initially
        quitLoading.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (startDetected)
        {
            // Increment the timer while the player is in the trigger zone
            triggerTimer += Time.deltaTime;

            // Update loading progress
            quitLoading.value = Mathf.Clamp01(triggerTimer / triggerDuration);

            // Check if the timer exceeds the desired duration
            if (triggerTimer >= triggerDuration)
            {
                // Switch scene after 1.5 seconds
                StartCoroutine(QuitApplication());
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
            quitLoading.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        startDetected = false;

        // Reset button color on exit
        mat.color = startingColor;

        // Hide the loading slider
        quitLoading.gameObject.SetActive(false);
    }

    private IEnumerator QuitApplication()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(0.01f); 

        // Quit play mode in the Unity Editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        
        // Quit the standalone application
        #else
            Application.Quit(); 
        #endif
    }
}
