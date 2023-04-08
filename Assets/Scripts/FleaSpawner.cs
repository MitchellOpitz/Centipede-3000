using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaSpawner : MonoBehaviour
{
    public GameObject fleaPrefab;
    public float spawnDelay = 10f;
    public int mushroomCountThreshold = 3;
    public float minX = -6f;
    public float maxX = 6f;
    public float spawnY = 11f;

    private MushroomCounter mushroomCounter;
    private int startingThreshold;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mushroomCounter = FindObjectOfType<MushroomCounter>();
        startingThreshold = mushroomCountThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay && mushroomCounter.mushroomCount < mushroomCountThreshold)
        {
            SpawnFlea();
            timer = 0f;
        }
    }

    private void SpawnFlea()
    {
        float spawnX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        Instantiate(fleaPrefab, spawnPosition, Quaternion.identity);
    }

    public void ResetThreshold()
    {
        mushroomCountThreshold = startingThreshold;
    }
}
