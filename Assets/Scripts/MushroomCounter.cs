using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCounter : MonoBehaviour
{
    public int mushroomCount = 0;

    public void Update()
    {
        // Find all GameObjects with the "Mushroom" tag
        GameObject[] mushrooms = GameObject.FindGameObjectsWithTag("Mushroom");

        // Set the initial mushroomCount to the number of mushrooms found
        mushroomCount = mushrooms.Length;
    }
}
