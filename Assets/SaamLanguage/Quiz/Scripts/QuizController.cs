using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    [Required]
    [SerializeField] private QuizTransition _quizTransition;
    [Required]
    [SerializeField] private QuizLoader _quizLoader;
    [Required]
    [SerializeField] private QuizTimer _quizTimer;
    [SerializeField] private int _pointsForAnswer;
    [SerializeField] private float _delayBeforeNextQuestion;

    private int _currentPoints = 0;

    private int _currentQuestionIndex = 0;

    private void OnEnable()
    {
        _currentPoints = 0;
        _currentQuestionIndex = 0;
        var QuestionData = _quizLoader.GetQuestion(_currentQuestionIndex);
        _quizTransition.SetQuestion(QuestionData, 0);
        _quizTimer.TimeEnded += ShowWinMessage;
        _quizTimer.StartTimer();
    }

    private void OnDisable()
    {
        _quizTimer.TimeEnded -= ShowWinMessage;
    }

    private void Start()
    {
        _quizTransition.SetEvent(OnQuestionAnswered);
    }

    private void OnQuestionAnswered(bool isCorrect)
    {
        if (isCorrect)
        {
            _currentPoints += _pointsForAnswer;
        }
        StartCoroutine(LoadNextQuestion());
    }

    private IEnumerator LoadNextQuestion()
    {
        yield return new WaitForSeconds(_delayBeforeNextQuestion);
        if (_quizLoader.IsLastQuestion(_currentQuestionIndex))
        {
            ShowWinMessage();
        } 
        else
        {
            _currentQuestionIndex++;
            _quizTransition.ShowNextQuestion(_quizLoader.GetQuestion(_currentQuestionIndex), _currentQuestionIndex);
        }
    }

    private void ShowWinMessage()
    {
        _quizTransition.ShowWinMessage(_quizLoader.GetMessageForPoints(_currentPoints), _currentPoints, _quizLoader.Count);
        _quizTimer.StopTimer();
    }
}
