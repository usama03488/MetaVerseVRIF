using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireRate = 0.5f;

    private float nextFireTime;
    public GameObject ParticleExplosion;
    public AudioSource GameOver;
    public AudioSource ShootSound;
    public Transform LeftClamp;
    public Transform RightClamp;
    void Update()
    {
        MovePlayer();
        Shoot();
    }

    void MovePlayer()
    {
      /*  float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

        // Restrict the player to screen bounds
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, LeftClamp.position.x, RightClamp.position.x);
        transform.position = position;*/
    }
    public void DoLeft()
    {
        float moveX = -1 * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

        // Restrict the player to screen bounds
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, LeftClamp.position.x, RightClamp.position.x);
        transform.position = position;
    }
    public void DoRight()
    {
        float moveX = 1 * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

        // Restrict the player to screen bounds
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, LeftClamp.position.x, RightClamp.position.x);
        transform.position = position;
    }

    void Shoot()
    {
        if (Time.time > nextFireTime)
        {
            ShootSound.Play();
            nextFireTime = Time.time + fireRate;
            Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        }
    }

    // Trigger Game Over when hit by asteroid
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameOver.Play();
            // Game over logic
           GameObject ExplosionParticle = Instantiate(ParticleExplosion, transform.position,Quaternion.identity);
            ExplosionParticle.GetComponent<ParticleSystem>().Play();
            Destroy(ExplosionParticle, 3f);
            AsteriodGameManager.instance.GameOver();
        }
    }
}
