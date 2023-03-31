using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HueShift : MonoBehaviour
{
    public PostProcessVolume volume;
    private ColorGrading colorGradingLayer;

    public float hueShiftAmount = 180f; // amount to shift hue by, in degrees

    private void Start()
    {
        if (volume.profile.TryGetSettings(out colorGradingLayer))
        {
            colorGradingLayer.hueShift.value = hueShiftAmount;
        }
    }

    public void SetHueShift(float shiftAmount)
    {
        hueShiftAmount = shiftAmount;
        if (colorGradingLayer != null)
        {
            colorGradingLayer.hueShift.value = hueShiftAmount;
        }
    }
}
