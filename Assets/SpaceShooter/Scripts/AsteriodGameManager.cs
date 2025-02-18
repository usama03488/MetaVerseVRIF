using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AsteriodGameManager : MonoBehaviour
{
    public static AsteriodGameManager instance;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;

    private int score = 0;
    private bool isGameOver = false;
    public GameObject Player;
    public AsteriodSpawner _spawner;
    public AudioSource Explosion;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreText();
        gameOverPanel.SetActive(false);
    }

    public void IncreaseScore()
    {
        if (!isGameOver)
        {
            score++;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Player.GetComponent<PlayerMovement>().enabled = false;
        Player.SetActive(false);
        _spawner.enabled = false;
        isGameOver = true;
        gameOverPanel.SetActive(true);
       // Time.timeScale = 0f; // Stop the game
    }

    public void RestartGame()
    {
        Player.SetActive(true);
        Player.GetComponent<PlayerMovement>().enabled = true;
        _spawner.enabled = true;
        //Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        isGameOver = false;
        score = 0;
        UpdateScoreText();

        // Reset player position
      //  Player.transform.position = new Vector3(0, -4f, 0);

        // Destroy all existing asteroids and bullets
        foreach (GameObject asteroid in GameObject.FindGameObjectsWithTag("Asteroid"))
        {
            Destroy(asteroid);
        }

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }

    }
}
