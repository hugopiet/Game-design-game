using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnController : MonoBehaviour
{
    public GameObject[] cloudPrefabs; // Array to hold different types of GameObjects
    public float verticalLineLength = 10f; // Length of the vertical line
    public bool startSpawn = false; // Trigger to start/stop spawning
    public float minSpawnTime = 1f; // Minimum time between spawns
    public float maxSpawnTime = 5f; // Maximum time between spawns

    private Coroutine spawnCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (startSpawn && spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnClouds());
        }
        else if (!startSpawn && spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    // Coroutine to spawn clouds at random intervals
    IEnumerator SpawnClouds()
    {
        while (true)
        {
            SpawnCloud();
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Method to spawn a cloud at a random position along a vertical line
    void SpawnCloud()
    {
        int randomIndex = Random.Range(0, cloudPrefabs.Length);
        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            transform.position.y + Random.Range(-verticalLineLength / 2, verticalLineLength / 2),
            transform.position.z
        );

        Instantiate(cloudPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}
