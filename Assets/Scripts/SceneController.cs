using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] private GameObject questionPopup;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (QuestionManager.Instance == null)
        {
            Instantiate(questionPopup); // Only instantiates if not already present
        }

        if (TimeTracker.Instance != null)
        {
            TimeTracker.Instance.ResetTime();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }*/
}
