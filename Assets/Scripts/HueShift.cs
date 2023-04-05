using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HueShift : MonoBehaviour
{
    public PostProcessVolume volume;
    private ColorGrading colorGradingLayer;

    public float minShiftAmount = 20f; // minimum amount to shift hue by, in degrees
    public float hueShiftTime = 1.5f; // time to shift hue by the full amount, in seconds

    private float currentShiftAmount = 0f;
    private float targetShiftAmount = 0f;
    private float hueShiftVelocity = 0f;

    private void Awake()
    {
        if (volume.profile.TryGetSettings(out colorGradingLayer))
        {
        }
    }

    private void Update()
    {
        if (Mathf.Abs(targetShiftAmount - currentShiftAmount) > 0.01f)
        {
            currentShiftAmount = Mathf.SmoothDamp(currentShiftAmount, targetShiftAmount, ref hueShiftVelocity, hueShiftTime);
            colorGradingLayer.hueShift.value = currentShiftAmount;
        }
    }

    public void RandomizeHueShift()
    {
        targetShiftAmount = Random.Range(-180f, 180f);
        var difference = Mathf.Abs(targetShiftAmount - colorGradingLayer.hueShift.value);
        if (difference < minShiftAmount)
        {
            RandomizeHueShift();
        }
    }
}
