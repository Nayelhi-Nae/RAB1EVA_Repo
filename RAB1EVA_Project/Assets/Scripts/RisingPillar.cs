using UnityEngine;
using System.Collections;

public class RisingPillar : MonoBehaviour
{
   [Header("Movement Settings")]
    [Tooltip("How high the pillar will rise.")]
    public float riseHeight = 5f;
    [Tooltip("Speed at which the pillar rises.")]
    public float moveSpeed = 3f;

    [Header("Camera Shake Settings")]
    [Tooltip("Drag the 'CameraShakeHolder' parent object here.")]
    public Transform cameraShakeHolder;
    [Tooltip("Intensity of the shake. Try values between 0.05 and 0.2.")]
    public float shakeMagnitude = 0.08f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isRising = false;
    private bool hasRisen = false;
    
    private Coroutine shakeCoroutine;
    private Vector3 originalLocalPos;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0f, riseHeight, 0f);

        if (cameraShakeHolder != null)
        {
            originalLocalPos = cameraShakeHolder.localPosition;
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
                StopShake();
            }
        }
    }

    public void ActivatePillar()
    {
        if (!hasRisen)
        {
            isRising = true;

            if (cameraShakeHolder != null && shakeCoroutine == null)
            {
                shakeCoroutine = StartCoroutine(ShakeRoutine());
            }
        }
    }

    IEnumerator ShakeRoutine()
    {
        while (isRising)
        {
            // Generate random X and Y offsets for the shake
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraShakeHolder.localPosition = originalLocalPos + new Vector3(offsetX, offsetY, 0f);

            yield return null;
        }

        // Reset position when finished rising
        cameraShakeHolder.localPosition = originalLocalPos;
        shakeCoroutine = null;
    }

    void StopShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
            if (cameraShakeHolder != null)
            {
                cameraShakeHolder.localPosition = originalLocalPos;
            }
        }
    }
}
