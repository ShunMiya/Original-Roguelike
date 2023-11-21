using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private float fpsMeasurementPeriod = 1.0f;
    private int fpsAccumulator = 0;
    private float fpsNextPeriod = 0;

    [SerializeField] private TextMeshProUGUI text;

    void Start()
    {
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurementPeriod;
    }

    void Update()
    {
        fpsAccumulator++;
        if (Time.realtimeSinceStartup > fpsNextPeriod)
        {
            int currentFPS = (int)(fpsAccumulator / fpsMeasurementPeriod);
            text.SetText("FPS: " + currentFPS);
            fpsAccumulator = 0;
            fpsNextPeriod += fpsMeasurementPeriod;
        }
    }
}
