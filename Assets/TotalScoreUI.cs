using TMPro;
using UnityEngine;

public class TotalScoreUI : MonoBehaviour
{
    void Start()
    {
        GetComponent<TMP_Text>().text = ((int)GameManager.Instance.TotalScore).ToString();
    }
}
