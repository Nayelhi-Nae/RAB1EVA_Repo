using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelCompletado : MonoBehaviour
{
    [Header("Settings")]
    public string escenaSiguiente = "SCN_HELL";
    public int pickUpsNecesarios = 1;

    private bool Changed = false;

    void Update()
    {
        if (Changed) return;

        
        PlayerInteractor interactor = FindFirstObjectByType<PlayerInteractor>();

        if (interactor == null) return;

        
        if (interactor.points >= pickUpsNecesarios)
        {
            Changed = true;
            SceneManager.LoadScene(escenaSiguiente);
        }
    }
}
