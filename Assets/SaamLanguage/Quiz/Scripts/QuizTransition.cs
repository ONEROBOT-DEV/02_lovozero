using Quiz;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class QuizTransition : MonoBehaviour
{
    [Required]
    [SerializeField] private QuizAnimation _quizAnimation;
    [Required]
    [SerializeField] private QuestionController _questionController;
    [Required]
    [SerializeField] private QuizWinDisplay _quizWinDisplay;

    private QuestionController _questionController1;
    private QuestionController _questionController2;

    private QuestionController _currentQuestionController;
    public QuestionController CurrentQuestionController => _currentQuestionController == null ? _questionController : _currentQuestionController;

    private void Awake()
    {
        _questionController1 = _questionController;
        _currentQuestionController = _questionController;
        _questionController2 = Instantiate(_questionController, _questionController.transform.parent);
    }

    private void OnEnable()
    {
        _questionController2.gameObject.SetActive(false);
        _questionController1.gameObject.SetActive(true);
        _quizWinDisplay.gameObject.SetActive(false);
        var pos = _questionController1.transform.position;
        pos.x = transform.position.x;
        _questionController1.transform.position = pos;
        _currentQuestionController = _questionController1;
    }

    public void ShowNextQuestion(QuestionData question, int index)
    {
        _quizAnimation.Animate(_currentQuestionController.transform, QuizAnimation.Direction.Off);
        _currentQuestionController = _currentQuestionController == _questionController1 ? _questionController2 : _questionController1;
        _quizAnimation.Animate(_currentQuestionController.transform, QuizAnimation.Direction.On);
        _currentQuestionController.SetQuestionData(question, index);
    }

    public void ShowWinMessage(WinMessage winMessage, int points, int maxPoints)
    {
        _quizAnimation.Animate(_currentQuestionController.transform, QuizAnimation.Direction.Off);
        _quizAnimation.Animate(_quizWinDisplay.transform, QuizAnimation.Direction.On);
        _quizWinDisplay.SetWinMessage(winMessage, points, maxPoints);
    }

    public void SetQuestion(QuestionData question, int index)
    {
        CurrentQuestionController.SetQuestionData(question, index);
    }

    public void SetEvent(Action<bool> OnQuestionAnswered)
    {
        _questionController1.QuestionAnswered += OnQuestionAnswered;
        _questionController2.QuestionAnswered += OnQuestionAnswered;
    }
}
