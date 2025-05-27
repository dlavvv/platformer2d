using UnityEngine;

public class FinalFinishPoint : MonoBehaviour
{
    [SerializeField] private GameObject endgameScreen;
    [SerializeField] private AudioClip finishSound;

    private void Awake()
    {
        endgameScreen.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // stops the score count
            TimeTracker.Instance.StopTimer();

            float finalTime = TimeTracker.Instance.totalTime;
            float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

            if (finalTime < bestTime)
            {
                PlayerPrefs.SetFloat("BestTime", finalTime);
                PlayerPrefs.Save();
            }

            SoundManager.instance.PlaySound(finishSound);
            Time.timeScale = 0;
            endgameScreen.SetActive(true);
        }
    }
}
