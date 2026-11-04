using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Header("UI Texts")]
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI timerText;

    private void OnEnable()
    {
        PlayerInteractor.OnPointsChanged += UpdatePoints;
    }

    private void OnDisable()
    {
        PlayerInteractor.OnPointsChanged -= UpdatePoints;
        
    }

    private void UpdatePoints(int current, int total)
    {
        pointsText.text = $"Points: {current}/{total}";
    }

    public void UpdateTimer(float timeRemaining)
    {
        if (timerText == null) return;
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";

        timerText .color = timeRemaining <= 10 ? Color.red : Color.white;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
