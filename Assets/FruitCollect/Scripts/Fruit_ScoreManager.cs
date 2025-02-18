using UnityEngine;
using UnityEngine.UI;

public class Fruit_ScoreManager : MonoBehaviour
{
    public Text scoreText;  // UI Text to display the score
    public int score = 0;  // Initial score

    void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore(int amount = 5)
    {
        score += amount;
        UpdateScoreText();
    }

    public void DecreaseScore(int amount = 5)
    {
        score -= amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
       // scoreText.text = "Score: " + score.ToString();
    }
}
