using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PasswordSpawner : MonoBehaviour
{
    public GameObject[] passwordPrefabs; // Array to hold different password prefabs (Strong and Weak)
    public float spawnInterval = 1.5f;   // Time interval between spawns
    public float spawnRangeX = 8f;        // Horizontal range for spawning passwords
    public Transform[] SpawnFruitsPoint;
    
    private void OnEnable()
    {
        StartCoroutine(SpawnPassword());
    }  

    IEnumerator SpawnPassword()
    {
        while (true)
        {
            // Wait for the next spawn
            yield return new WaitForSeconds(spawnInterval);

            // Randomly select a position along the x-axis
            //   float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Transform Positionx = SpawnFruitsPoint[Random.Range(0, SpawnFruitsPoint.Length)];
            Vector2 spawnPosition = new Vector2(Positionx.position.x, transform.localPosition.y);

            // Randomly select a password prefab from the array
            int randomIndex = Random.Range(0, passwordPrefabs.Length);

            // Instantiate the password prefab at the spawn position
            Instantiate(passwordPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}
