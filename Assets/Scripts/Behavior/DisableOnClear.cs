using UnityEngine;

public class DisableOnClear : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.clearedWave)
        {
            gameObject.SetActive(false);
        }
    }
}
