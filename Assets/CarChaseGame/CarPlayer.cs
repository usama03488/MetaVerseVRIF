using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPlayer : MonoBehaviour
{
    public float acceleration = 15f;
    public float turnSpeed = 100f;
    public float maxSpeed = 20f;

    private Rigidbody rb;
    private float horizontalInput;
    

    private void OnEnable()
    {
        // Get the Rigidbody component attached to the player car
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get player input for movement
      //  horizontalInput = Input.GetAxis("Horizontal");
       
    }

    private void FixedUpdate()
    {
        // Move the car forward with physics-based movement
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward  * acceleration, ForceMode.Acceleration);
        }

        // Rotate the car using torque
         // Smoothly rotate the car based on horizontal input
        if (horizontalInput != 0)
        {
            Quaternion targetRotation = Quaternion.Euler(0, horizontalInput * Time.fixedDeltaTime * turnSpeed, 0);
            rb.MoveRotation(rb.rotation * targetRotation); // Apply smooth rotation
        }
    }
    public void Doleft()
    {
        horizontalInput = -1f;
    }
    public void DoRight()
    {
        horizontalInput = 1f;
    }

    public void DoStraight()
    {
        horizontalInput = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Police") || collision.gameObject.CompareTag("Obstacle"))
        {
            // Trigger Game Over when colliding with police or obstacles
            CarGameManager.Instance.GameOver();
        }
    }
}
