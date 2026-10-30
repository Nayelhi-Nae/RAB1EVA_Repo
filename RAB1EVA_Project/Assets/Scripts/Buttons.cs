using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void Play()
    {
       SceneManagement.Instance.StartGame();
    }

    public void NextLevel()
    {
       SceneManagement.Instance.LoadNextLevel();
    }

    public void MainMenu()
    {
        SceneManagement.Instance.GoToMainMenu();
    }

    public void Exit()
    {
        SceneManagement.Instance.ExitGame();
    }
}
