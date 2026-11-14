using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class RisingPillar : MonoBehaviour
{
 [Header("Movement Settings")]
    public float riseHeight = 90f;
    public float moveSpeed = 10f;

    [Header("Cinemachine Shake Settings")]
    public CinemachineCamera cinemachineCamera;
    public float shakeAmplitude = 2f;
    public float shakeFrequency = 2f;

    [Header("Audio Settings")]
    [Tooltip("Drag your AudioSource here, or leave blank to auto-detect on this object.")]
    public AudioSource rumbleAudioSource;

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

       
        if (rumbleAudioSource == null)
        {
            rumbleAudioSource = GetComponent<AudioSource>();
        }

        if (rumbleAudioSource != null)
        {
            rumbleAudioSource.loop = true;
            rumbleAudioSource.playOnAwake = false;
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
                StopRumbleAudio();
            }
        }
    }

    public void ActivatePillar()
    {
        if (!hasRisen)
        {
            isRising = true;
            SetShakeActive(true);
            StartRumbleAudio();
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

    void StartRumbleAudio()
    {
        if (rumbleAudioSource != null && !rumbleAudioSource.isPlaying)
        {
            rumbleAudioSource.Play();
        }
    }

    void StopRumbleAudio()
    {
        if (rumbleAudioSource != null && rumbleAudioSource.isPlaying)
        {
            rumbleAudioSource.Stop();
        }
    }
}
