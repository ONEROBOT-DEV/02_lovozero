using UnityEngine;

public class QuizLoader : MonoBehaviour
{
    [SerializeField] private QuizQuestions _quizQuestions;
    [SerializeField] private WinMessages _winMessages;

    public int Count => _quizQuestions.Questions.Count;

    public QuestionData GetQuestion(int questionIndex)
    {
        return _quizQuestions.Questions[questionIndex];
    }

    public bool IsLastQuestion(int questionIndex)
    {
        return questionIndex == _quizQuestions.Questions.Count - 1;
    }

    public WinMessage GetMessageForPoints(int points) => _winMessages.GetMessageForPoints(points);
}
