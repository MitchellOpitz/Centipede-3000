using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomSpawner : MonoBehaviour
{
    public GameObject mushroomPrefab;
    public int numMushroomsToSpawn = 10;
    public Vector2 spawnPoint1;
    public Vector2 spawnPoint2;
    public Vector2 gridSize = new Vector2(1f, 1f);

    private void Start()
    {
        SpawnMushrooms();
    }

    void SpawnMushrooms()
    {
        for (int i = 0; i < numMushroomsToSpawn; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(spawnPoint1.x, spawnPoint2.x), Random.Range(spawnPoint1.y, spawnPoint2.y));
            spawnPos.x = Mathf.RoundToInt(spawnPos.x / gridSize.x) * gridSize.x;
            spawnPos.y = Mathf.RoundToInt(spawnPos.y / gridSize.y) * gridSize.y;

            Instantiate(mushroomPrefab, spawnPos, Quaternion.identity);
        }
    }
}
