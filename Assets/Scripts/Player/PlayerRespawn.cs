using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform startCheckpoint; // start position
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // animation event
    public void CheckRespawn()
    {
        // check if checkpoint available
        if (startCheckpoint == null)
        {
            // show game over screen
            uiManager.GameOver();

            return; // don't execute the rest of the function
        }

        FindObjectOfType<QuestionPopUpManager>().ShowQuestion(OnAnswerCorrect, OnAnswerIncorrect);
    }

    // activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            startCheckpoint = collision.transform; // store the start checkpoint after we pass it
            collision.GetComponent<Collider2D>().enabled = false; // deactivate checkpoint collider
            Debug.Log("Player passed the checkpoint.");
        }
    }

    private void OnAnswerCorrect()
    {
        transform.position = startCheckpoint.position;
        playerHealth.Respawn();
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(startCheckpoint.parent);
    }

    private void OnAnswerIncorrect()
    {
        uiManager.GameOver();
    }
}
