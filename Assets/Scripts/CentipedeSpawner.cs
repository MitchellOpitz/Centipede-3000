using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSpawner : MonoBehaviour
{
    public GameObject headPrefab;
    public Vector2 spawnPoint;

    void Start()
    {
        SpawnHead();
    }

    void SpawnHead()
    {
        GameObject newHead = Instantiate(headPrefab, spawnPoint, Quaternion.identity);
    }

}
