using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text messageText;

    public void UpdateScore(int fallen)
    {
        scoreText.text = $"Fallen: {fallen}";
    }

    public void UpdateLevel(int level)
    {
        levelText.text = $"Level: {level + 1}";
    }

    public void ShowMessage(string msg)
    {
        messageText.text = msg;
    }
}
