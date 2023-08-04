using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Questions", menuName = "ScriptableObjects/Questions", order = 1)]
public class QuizQuestions : ScriptableObject
{
    [SerializeField] private List<QuestionData> questions;

    public List<QuestionData> Questions => questions;
}

[Serializable]
public class QuestionData
{
    [SerializeField] private string _questionText;
    [SerializeField] private List<AnswerData> _answers;

    public string QuestionText => _questionText;

    public List<AnswerData> Answers => _answers;
}

[Serializable]
public class AnswerData
{
    [SerializeField] private string _answerText;
    [SerializeField] private bool _isCorrect;

    public string AnswerText => _answerText;

    public bool IsCorrect => _isCorrect;
}