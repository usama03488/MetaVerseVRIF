using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Fruit_GameManager : MonoBehaviour
{
    public GameObject passwordSpawner;
    public GameObject player;
    public GameObject playButton;
    public GameObject gameoverPanel;
    public GameObject ScoreManager;
    public Text TextScore;
   
    private void OnDisable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   public void gameStop()
    {
       // passwordSpawner.GetComponent<PasswordSpawner>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        passwordSpawner.SetActive(false);
       



    }

    public void RestartGame()
    {
        
        playButton.SetActive(true);
        gameStop();
    }

    public void startStop()
    {
     //   passwordSpawner.GetComponent<PasswordSpawner>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        passwordSpawner.SetActive(true);
        TextScore.text = "00";
        ScoreManager.GetComponent<Fruit_ScoreManager>().score = 0;

    }

    public void tryAgain()
    {
        gameoverPanel.SetActive(true);
        passwordSpawner.SetActive(false);
        
        TextScore.text = "00";
        ScoreManager.GetComponent<Fruit_ScoreManager>().score = 0;
    }
}
