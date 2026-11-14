using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tell the player's checkpoint manager to update the active spawn point
            PlayerCheckpoint playerCp = other.GetComponent<PlayerCheckpoint>();
            if (playerCp != null)
            {
                playerCp.UpdateSpawnPoint(transform.position);
            }

            // Optional: Disable this checkpoint collider so it doesn't trigger repeatedly
            GetComponent<Collider>().enabled = false;
        }
    }
}
