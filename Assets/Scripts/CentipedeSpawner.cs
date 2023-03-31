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

        // Get the sprite renderer component of the new centipede
        SpriteRenderer spriteRenderer = newHead.GetComponent<SpriteRenderer>();

        // Randomly generate a hue shift value
        float hueShift = Random.Range(-180f, 180f);

        GetComponent<HueShift>().SetHueShift(hueShift);
    }

}
