using UnityEngine;
using System.Collections;

public class PillarSwitch : MonoBehaviour
{
   [Header("Target Reference")]
    public GameObject targetPillar;

    [Header("Disappearance Settings")]
    public float fadeDuration = 1.0f;
    public float rotationAmount = 90f;

    private bool isActivated = false;
    private Renderer[] switchRenderers;
    private Collider switchCollider;
    private AudioSource switchAudioSource;

    void Start()
    {
        switchRenderers = GetComponentsInChildren<Renderer>();
        switchCollider = GetComponent<Collider>();
        switchAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            isActivated = true;

           
            if (switchAudioSource != null)
            {
                switchAudioSource.Play();
            }

            
            if (targetPillar != null)
            {
                RisingPillar pillarScript = targetPillar.GetComponent<RisingPillar>();
                if (pillarScript != null)
                {
                    pillarScript.ActivatePillar();
                }
            }

            
            if (switchCollider != null)
            {
                switchCollider.enabled = false;
            }

            
            StartCoroutine(FadeAndRotateRoutine());
        }
    }

    IEnumerator FadeAndRotateRoutine()
    {
        if (switchRenderers == null || switchRenderers.Length == 0) yield break;

        Material[] materials = new Material[switchRenderers.Length];
        Color[] startColors = new Color[switchRenderers.Length];

        for (int i = 0; i < switchRenderers.Length; i++)
        {
            materials[i] = switchRenderers[i].material;

            materials[i].SetFloat("_Surface", 1);
            materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            materials[i].SetInt("_ZWrite", 0);
            materials[i].DisableKeyword("_ALPHATEST_ON");
            materials[i].EnableKeyword("_ALPHABLEND_ON");
            materials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            startColors[i] = materials[i].color;
        }

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, rotationAmount);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            for (int i = 0; i < materials.Length; i++)
            {
                Color newColor = startColors[i];
                newColor.a = Mathf.Lerp(1f, 0f, t);
                materials[i].color = newColor;
            }

            yield return null;
        }

        
        foreach (Renderer r in switchRenderers)
        {
            if (r != null) r.enabled = false;
        }

        
        if (switchAudioSource != null && switchAudioSource.isPlaying)
        {
            yield return new WaitForSeconds(switchAudioSource.clip.length);
        }

        gameObject.SetActive(false);
    }
}
