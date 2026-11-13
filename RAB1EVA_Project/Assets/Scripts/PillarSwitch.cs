using UnityEngine;

public class PillarSwitch : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag your Pillar GameObject here in the Inspector.")]
    public RisingPillar targetPillar;

    [Header("Settings")]
    [Tooltip("If true, the switch can only be used once.")]
    public bool singleUse = true;

    private bool hasBeenActivated = false;

    // This triggers when the player walks into the switch's collider area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        if (hasBeenActivated && singleUse) return;

        if (targetPillar != null)
        {
            // Directly communicate with the pillar script
            targetPillar.ActivatePillar();
            hasBeenActivated = true;
            
            Debug.Log("Switch activated! Pillar is rising.");
        }
        else
        {
            Debug.LogWarning("No pillar assigned to this switch!", this);
        }
    }
   
}
