using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("Timer Settings")]
    public float startTime = 60f;
    public bool isTimerRunning = false;

    private float timeRemaining;

    public UIManager uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = startTime;
        isTimerRunning = true;

        if (uiManager != null)
        {
            uiManager.UpdateTimer(timeRemaining);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerRunning)
        {
         return;
        }

        if (PlayerInteractor.Instance != null && PlayerInteractor.Instance.levelCompleted)
        {
            isTimerRunning = false;
            return;
        }

        timeRemaining -= Time.deltaTime;

        if (uiManager != null)
        {
            uiManager.UpdateTimer(timeRemaining);
        }

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isTimerRunning = false;
            TimeUp();
        }


    }

    public void TimeUp()
    {
        isTimerRunning = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
