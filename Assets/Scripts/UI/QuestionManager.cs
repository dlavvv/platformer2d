using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;
    
    public QuestionData[] questions;

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

    public QuestionData GetRandomQuestion()
    {
        return questions[Random.Range(0, questions.Length)];
    }
}
