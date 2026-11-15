using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Completed : MonoBehaviour
{
    [Header("Settings")]
    public string escenaSiguiente = "SCN_LEVEL2"; // ✅ Tipo 'string' y acceso 'public' añadidos
    public int pickUpsNecesarios = 4;

    private bool changed = false;
    private PlayerInteractor interactor;

    void Start()
    {
        // ✅ Se busca una sola vez al iniciar la escena, no en cada frame
        interactor = FindFirstObjectByType<PlayerInteractor>();
    }

    void Update()
    {
        if (changed || interactor == null) return;

        if (interactor.points >= pickUpsNecesarios)
        {
            changed = true;
            SceneManager.LoadScene(escenaSiguiente);
        }
    }
}