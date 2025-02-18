using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarGameManager : MonoBehaviour
{
    public static CarGameManager Instance;

    public GameObject player;
    public GameObject policeCarPrefab;
    public Transform[] spawnPoints;
    public TMP_Text scoreText;
    public AudioSource BlastSound;
    public GameObject GameOverCanvas;
    private float score = 0f;
    private bool gameRunning = true;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        player.GetComponent<CarPlayer>().enabled = true;
        gameRunning = true;
        // Spawn police cars at intervals
        SpawnPoliceCars();
        StartCoroutine(IncreaseScore());
    }

    private void SpawnPoliceCars()
    {

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject PoliceCar = Instantiate(policeCarPrefab, spawnPoint.position, spawnPoint.rotation);
            PoliceCar.GetComponent<PoliceCarController>().player = player.transform;
        }

    }
    public void InstantiatePolice()
    {
        GameObject PoliceCar = Instantiate(policeCarPrefab, spawnPoints[Random.Range(0,spawnPoints.Length)].position, spawnPoints[Random.Range(0, spawnPoints.Length)].rotation);
        PoliceCar.GetComponent<PoliceCarController>().player = player.transform;
    }

    private IEnumerator IncreaseScore()
    {
        while (gameRunning)
        {
            score += 1;
            scoreText.text = "Score: " + score.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void GameOver()
    {
        gameRunning = false;
        Debug.Log("Game Over!");
        GameOverCanvas.SetActive(true);
        foreach(var PoliceCars in FindObjectsOfType<PoliceCarController>())
        {
            Destroy(PoliceCars.gameObject);
        }
        player.GetComponent<CarPlayer>().enabled=false;
        player.GetComponent<AudioSource>().enabled=false;
        // Implement any UI or reset functionality here
    }

    public void Restart()
    {
        SpawnPoliceCars();
        gameRunning = true;
        StartCoroutine(IncreaseScore());
      
        GameOverCanvas.SetActive(false);
        player.GetComponent<CarPlayer>().enabled = true;
        player.GetComponent<AudioSource>().enabled = true;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }
    private void OnDisable()
    {
        gameRunning = false;
        Debug.Log("Game Over!");
      //  GameOverCanvas.SetActive(true);
        foreach (var PoliceCars in FindObjectsOfType<PoliceCarController>())
        {
            Destroy(PoliceCars.gameObject);
        }
        player.GetComponent<CarPlayer>().enabled = false;

        
   /*     gameRunning = true;
        SpawnPoliceCars();
        StartCoroutine(IncreaseScore());*/
      
       // GameOverCanvas.SetActive(false);
      //  player.GetComponent<CarPlayer>().enabled = true;
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }
}
