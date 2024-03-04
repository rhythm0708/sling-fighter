using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private bool collisionDetected = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object it's the ball
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
        // Wait for 3 seconds then switch scenes
        yield return new WaitForSeconds(3f);

        // Change the scene after the delay
        SceneManager.LoadScene("ThirdPersonTest");
    }
}