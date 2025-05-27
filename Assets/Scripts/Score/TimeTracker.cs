using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    public static TimeTracker Instance { get; private set; }

    public float totalTime;
    private bool isTracking = true;

    // singleton pattern
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

    void Update()
    {
        if (isTracking)
        {
            totalTime += Time.unscaledDeltaTime;
        }
    }

    public void ResetTime()
    {
        totalTime = 0f;
        isTracking = true;
    }

    public void StopTimer()
    {
        isTracking = false;
        CheckAndSaveBestTime();
    }

    public float GetCurrentTime()
    {
        return totalTime;
    }

    private void CheckAndSaveBestTime()
    {
        float currentTime = totalTime;

        // Check if a best time already exists
        if (!PlayerPrefs.HasKey("BestTime") || currentTime < PlayerPrefs.GetFloat("BestTime"))
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
            PlayerPrefs.Save();
        }
    }

    public float GetBestTime()
    {
        return PlayerPrefs.GetFloat("BestTime", -1f); // -1 if no score exists yet
    }
}
