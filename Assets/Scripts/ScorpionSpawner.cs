using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionSpawner : MonoBehaviour
{
    public GameObject scorpionPrefab;
    public float spawnDelay = 10f;
    public float spawnX = 0f;
    public float minY = -3f;
    public float maxY = 3f;

    private void Start()
    {
        InvokeRepeating("SpawnScorpion", spawnDelay, spawnDelay);
    }

    void SpawnScorpion()
    {
        float spawnY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);

        // Round the spawn position to the nearest integer to align with the grid
        spawnPosition.x = Mathf.RoundToInt(spawnPosition.x);
        spawnPosition.y = Mathf.RoundToInt(spawnPosition.y);

        Instantiate(scorpionPrefab, spawnPosition, Quaternion.identity);
    }
}
