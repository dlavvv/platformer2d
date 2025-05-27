using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text bestTimeText;

    void Start()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (bestTime < float.MaxValue)
        {
            int minutes = (int)(bestTime / 60);
            int seconds = (int)(bestTime % 60);
            bestTimeText.text = $"Best Time: {minutes:D2}:{seconds:D2}";
        }
        else
        {
            bestTimeText.text = "Best Time: -";
        }
    }
}
