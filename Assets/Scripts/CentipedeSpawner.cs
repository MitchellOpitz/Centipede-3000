using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSpawner : MonoBehaviour
{
    public GameObject headPrefab;
    public Vector2 spawnPoint;

    void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        StartCoroutine(SpawnHead());
    }

    public IEnumerator SpawnHead()
    {
        yield return new WaitForSeconds(3f);
        GameObject newHead = Instantiate(headPrefab, spawnPoint, Quaternion.identity);
    }

}
