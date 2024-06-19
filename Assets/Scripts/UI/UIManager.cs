using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    private bool isGameOver = false;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if pause screen is already active, unpause si invers
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }
    
    #region Game Over
    // activate game over screen
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        // SoundManager.instance.PlaySound(gameOverSound);
    }

    // game over functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // restarts the current level
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        // if status == true pause | if status == false unpause
        if(!isGameOver)
            pauseScreen.SetActive(status);

        // if pause is true, "freeze" time
        if(status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }

    #endregion
}
