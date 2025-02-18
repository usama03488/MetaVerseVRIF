using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGame : MonoBehaviour
{
    public Fruit_GameManager gameManager;
    // Start is called before the first frame update
    private void OnEnable()
    {
        gameManager.playButton.SetActive(true);
        gameManager.gameStop();

    }

    private void OnDisable()
    {
        gameManager.tryAgain();
    }
}
