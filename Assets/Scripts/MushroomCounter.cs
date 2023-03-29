using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomCounter : MonoBehaviour
{
    public int mushroomCount = 0;

    public void Add()
    {
        mushroomCount++;
        Debug.Log("Mushroom Count: " + mushroomCount);
    }

    public void Remove()
    {
        mushroomCount--;
        Debug.Log("Mushroom Count: " + mushroomCount);
    }
}
