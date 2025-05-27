using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question")]
public class QuestionData : ScriptableObject
{
    [TextArea]
    public string question;
    public string[] answers = new string[3];
    public int correctAnswerIndex;

    [TextArea]
    public string hint;

}
