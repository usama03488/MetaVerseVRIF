using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerCollision : MonoBehaviour
{
    public Fruit_ScoreManager scoreManager;
    public GameObject gameController;
    public AudioSource pickupsSound;
    public AudioSource GameOverSound;
    public Fruit_GameManager gameManager;


    /*void OnTriggerEnter(Collidercollision)
    {
        if (collision.CompareTag("StrongPassword"))
        {
            Debug.Log("Collide");
            scoreManager.IncreaseScore(10);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("WeakPassword"))
        {
            scoreManager.DecreaseScore(5);
            Destroy(collision.gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("StrongPassword"))
        {
            Debug.Log("Collide");
            scoreManager.IncreaseScore(10);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("WeakPassword"))
        {
            pickupsSound.Play();
            // scoreManager.DecreaseScore(5);
            scoreManager.IncreaseScore(5);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
          
            Debug.Log("GameOver");
            //   gameController.SetActive(false);
            gameManager.tryAgain();
            GameOverSound.Play();
          

        }
    }
}
