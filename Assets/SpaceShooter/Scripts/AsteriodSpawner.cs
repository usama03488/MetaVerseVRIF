using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefab;
    public Transform LeftClamp;
    public Transform RightClamp;
    public float spawnRate = 1f;
    public float minX = -7f;
    public float maxX = 7f;

    void OnEnable()
    {
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            int Asteroid = Random.Range(0, asteroidPrefab.Length);

            float randomX = Random.Range(LeftClamp.position.x, RightClamp.position.x);
            Vector3 spawnPosition = new Vector3(randomX, 6f, 0);
            Instantiate(asteroidPrefab[Asteroid], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
