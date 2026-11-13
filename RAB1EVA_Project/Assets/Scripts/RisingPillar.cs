using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class RisingPillar : MonoBehaviour
{
 [Header("Movement Settings")]
    public float riseHeight = 90f;
    public float moveSpeed = 10f;

    [Header("Cinemachine Shake Settings")]
    [Tooltip("Drag your PlayerCam here.")]
    public CinemachineCamera cinemachineCamera;
    public float shakeAmplitude = 2f;
    public float shakeFrequency = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isRising = false;
    private bool hasRisen = false;

    private CinemachineBasicMultiChannelPerlin noiseComponent;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0f, riseHeight, 0f);

        if (cinemachineCamera != null)
        {
            noiseComponent = cinemachineCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
            SetShakeActive(false);
        }
    }

    void Update()
    {
        if (isRising && !hasRisen)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition;
                hasRisen = true;
                isRising = false;
                SetShakeActive(false);
            }
        }
    }

    public void ActivatePillar()
    {
        if (!hasRisen)
        {
            isRising = true;
            SetShakeActive(true);
        }
    }

    void SetShakeActive(bool active)
    {
        if (noiseComponent != null)
        {
            noiseComponent.AmplitudeGain = active ? shakeAmplitude : 0f;
            noiseComponent.FrequencyGain = active ? shakeFrequency : 0f;
        }
    }
}
