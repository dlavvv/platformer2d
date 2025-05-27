using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPopUpManager : MonoBehaviour
{
    private System.Action onCorrect;
    private System.Action onIncorrect;
    private PlayerAttack playerAttack;

    public GameObject questionPopup;
    public TMP_Text questionText;
    public Button[] answerButtons;
    public Button hintButton;
    public TMP_Text hintText;

    private QuestionData currentQuestion;
    private bool answered;

    private void Start()
    {
        playerAttack = FindAnyObjectByType<PlayerAttack>();
        questionPopup.SetActive(false);
    }

    public void ShowQuestion(System.Action correctCallback, System.Action incorrectCallback)
    {
        answered = false;
        hintText.text = "";
        currentQuestion = QuestionManager.Instance.GetRandomQuestion();

        onCorrect = correctCallback;
        onIncorrect = incorrectCallback;

        questionText.text = currentQuestion.question;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => Answer(index));
        }

        hintButton.onClick.RemoveAllListeners();
        hintButton.onClick.AddListener(() => hintText.text = currentQuestion.hint);

        questionPopup.SetActive(true);

        // 'pause' the game
        Time.timeScale = 0;

        // disable player attack
        if (playerAttack == null)
            playerAttack = FindObjectOfType<PlayerAttack>();
        if (playerAttack != null)
            playerAttack.SetCanMove(false);
    }

    private void Answer(int index)
    {
        if (answered) return;
        answered = true;

        // 'unpause' the game
        Time.timeScale = 1;

        // enable player attack
        if (playerAttack != null)
        {
            playerAttack.SetCanMove(true);
        }

        questionPopup.SetActive(false);

        if (index == currentQuestion.correctAnswerIndex)
            onCorrect?.Invoke();
        else
            onIncorrect?.Invoke();
        
    }
}
