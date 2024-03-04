using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    private bool collisionDetected = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the tag "Ball"
        if (collision.gameObject.CompareTag("Ball") && !collisionDetected)
        {
            // Set collisionDetected to true to prevent multiple scene switches
            collisionDetected = true;

            // Start the timer coroutine
            StartCoroutine(SceneSwitchTimer());
        }
    }

    private IEnumerator SceneSwitchTimer()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Change the scene after the delay
        SceneManager.LoadScene("Settings");
    }
}