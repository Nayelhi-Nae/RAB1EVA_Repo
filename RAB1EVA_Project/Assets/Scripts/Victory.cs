using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("SCN_Victory")]
    public string victorySceneName = "SCN_Victory";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadVictory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadVictory();
        }
    }

    private void LoadVictory()
    {
        SceneManager.LoadScene(victorySceneName);
    }
}
