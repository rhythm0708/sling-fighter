using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject scoreName;
    [SerializeField] private GameObject rank;
    
    public void SetScore(string name, string score, string rank)
    {
        this.rank.GetComponent<TextMeshProUGUI>().text = rank;
        this.scoreName.GetComponent<TextMeshProUGUI>().text = name;
        this.score.GetComponent<TextMeshProUGUI>().text = score;
    }
}