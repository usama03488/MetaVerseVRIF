using UnityEngine;
using UnityEngine.SceneManagement;

public class PasswordFalling : MonoBehaviour
{
    public float fallSpeed = 3f;

    void Update()
    {
        // Move the password downwards
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // Destroy the password if it goes below a certain point (missed)
        if (transform.position.y < -5f)
        {
            //FindObjectOfType<ScoreManager>().DecreaseScore();
            Destroy(gameObject);
        }
    }
}
