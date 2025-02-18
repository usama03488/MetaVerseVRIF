using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarController : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 12f;
    public float detectionRadius = 50f;
    public float turnSpeed = 100f;

    private Rigidbody rb;
    public GameObject Light1;
    public GameObject Light2;
    public GameObject ParticleDestory;

    private void OnEnable()
    {
        // Get the Rigidbody component attached to the police car
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ChangeRandomSpeed());
        StartCoroutine(PoliceLights());

    }
    public IEnumerator ChangeRandomSpeed()
    {
        yield return new WaitForSeconds(5f);
        chaseSpeed = Random.Range(14, 18);
        StartCoroutine(ChangeRandomSpeed());
    }
    public IEnumerator PoliceLights()
    {
        yield return new WaitForSeconds(1f);
        Light1.SetActive(true);
        Light2.SetActive(false);
        yield return new WaitForSeconds(1f);
        Light2.SetActive(true);
        Light1.SetActive(false);
        StartCoroutine(PoliceLights());
    }

    private void FixedUpdate()
    {
        // Chase the player if within detection range
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // Move police car towards the player using physics
            if (rb.velocity.magnitude < chaseSpeed)
            {
                rb.AddForce(direction * chaseSpeed, ForceMode.Acceleration);
            }

            // Rotate police car to face the player
            Quaternion lookRotation = Quaternion.LookRotation(player.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger Game Over when the police car collides with the player
            CarGameManager.Instance.GameOver();
        }
        if (collision.gameObject.CompareTag("Police"))
        {
            GameObject Particle = Instantiate(ParticleDestory, transform.position, Quaternion.identity);
            Particle.GetComponent<ParticleSystem>().Play();
            CarGameManager.Instance.BlastSound.Play();
            CarGameManager.Instance.InstantiatePolice();
            // Trigger Game Over when the police car collides with the player
            Destroy(collision.collider.gameObject);
        }
    }
}
