using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public GameObject ParticleSystem;
    
    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);

        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }
    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            AsteriodGameManager.instance.Explosion.Play();
            GameObject ParticleExplosion = Instantiate(ParticleSystem, collision.gameObject.transform.position, Quaternion.identity);
            ParticleExplosion.GetComponent<ParticleSystem>().Play();
            Destroy(ParticleExplosion, 3f);
            
            Destroy(collision.gameObject); // Destroy asteroid
            Destroy(gameObject); // Destroy bullet
            AsteriodGameManager.instance.IncreaseScore(); // Increase score
        }
    }
}
