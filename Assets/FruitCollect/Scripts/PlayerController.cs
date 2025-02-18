using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float minX = -7f;  // Minimum x position boundary
    public float maxX = 7f;   // Maximum x position boundary
    public bool check;
 
  /*  void Update()
    {
        // Get the horizontal input from arrow keys
        float moveInput = Input.GetAxis("Horizontal");

        // Calculate new position based on input and speed
        Vector3 newPosition = transform.position + Vector3.right * moveInput * speed * Time.deltaTime;

        // Clamp the x position within the specified range (-7 to 7)
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Update the player's position
        transform.position = newPosition;
    }*/
    public void doLeft()
    {
        if (check)
        {

            Debug.Log("Im Cahnging LEFT");

            // Calculate new position based on input and speed
            Vector3 newPosition = transform.localPosition + Vector3.right * -1 * speed * Time.deltaTime;

            // Clamp the x position within the specified range (-7 to 7)
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            // Update the player's position
            transform.localPosition = newPosition;
        }
    }
    public void doRight()
    {
     
        if (check)
        {
            Debug.Log("Im Cahnging RIGHT");
            // Calculate new position based on input and speed
            Vector3 newPosition = transform.localPosition + Vector3.right * 1 * speed * Time.deltaTime;

            // Clamp the x position within the specified range (-7 to 7)
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            // Update the player's position
            transform.localPosition = newPosition;
        }
    }
    public void OnDown()
    {
        check = true;
    }
    public void OnUp()
    {
        check = false;
    }
}
