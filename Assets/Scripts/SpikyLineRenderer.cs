using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyLineRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 100;
    public float updateFrequency = 0.1f;
    public float amplitude = 0.5f;
    public float shiftDuration = 0.1f;

    private List<Vector3> points = new List<Vector3>();
    private Coroutine updateCoroutine;

    void Start()
    {
        // Initialize the line renderer
        lineRenderer.positionCount = numberOfPoints;
        for (int i = 0; i < numberOfPoints; i++)
        {
            float x = Mathf.Lerp(-20f, 20f, (float)i / (numberOfPoints - 1));
            points.Add(new Vector3(x, -4.5f, 0f));
        }
        lineRenderer.SetPositions(points.ToArray());

        // Start the update coroutine
        updateCoroutine = StartCoroutine(UpdateLine());
    }

    private IEnumerator UpdateLine()
    {
        while (true)
        {
            // Generate random amplitudes for each point
            float[] amplitudes = new float[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                amplitudes[i] = Random.Range(0f, amplitude);
            }

            // Shift the line towards the new amplitudes over time
            float elapsed = 0f;
            while (elapsed < shiftDuration)
            {
                float t = elapsed / shiftDuration;
                for (int i = 0; i < numberOfPoints; i++)
                {
                    Vector3 point = points[i];
                    point.y = Mathf.Lerp(point.y, -4.5f + amplitudes[i], t);
                    points[i] = point;
                }
                lineRenderer.SetPositions(points.ToArray());

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Wait for the next update
            yield return new WaitForSeconds(updateFrequency);
        }
    }

    void OnDestroy()
    {
        // Stop the update coroutine when the object is destroyed
        StopCoroutine(updateCoroutine);
    }
}
