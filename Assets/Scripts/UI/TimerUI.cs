using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TMP_Text timerText;

    void Update()
    {
        float time = TimeTracker.Instance.GetCurrentTime();
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        timerText.text = $"{minutes:D2}:{seconds:D2}";
    }
}
