using UnityEngine;
using UnityEngine.SceneManagement; //Librería que contiene acciones de carga/descarga de escenas

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance; //Variable que permite acceder a este script desde cualquier otro script

    [Header("Scene Management")]
    //HAY QUE CAMBIAR LOS NUMEROS CONFORME A LA ESCENA
    public int mainMenuScene = 0;
    public int firstLevelScene = 0;
    public int levelClearedScene = 0;
    public int victoryScene = 0;

    [Header ("Status")]
    public int lastLevelScene = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LevelCompleted(int levelIndex)
    {
        lastLevelScene = levelIndex;
        SceneManager.LoadScene(levelClearedScene);
    }

    public void LoadNextLevel()
    {
        int next = lastLevelScene + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
        else
        {
            SceneManager.LoadScene(victoryScene);
        }
    }

    public void StartGame()
    {
        lastLevelScene = 0;
        SceneManager.LoadScene(firstLevelScene);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void SceneChanger(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
