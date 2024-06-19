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
            SoundManager.instance.PlaySound(finishSound);
            Time.timeScale = 0;
            endgameScreen.SetActive(true);
        }
    }
}
