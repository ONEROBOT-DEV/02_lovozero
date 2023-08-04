using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Quiz
{
    public class QuestionController : MonoBehaviour
    {
        private static readonly Random rng = new Random();
        [ChildGameObjectsOnly, Required]
        [SerializeField] private Transform _answerContainer;
        [ChildGameObjectsOnly, Required]
        [SerializeField] private TextMeshProUGUI _questionText;
        [ChildGameObjectsOnly, Required]
        [SerializeField] private DotController _dotController;
        [SerializeField] private int _answerCount = 3;
        [SerializeField] private bool _shuffle = true;

        private bool _answered;

        private List<AnswerBar> _answerBars;

        public event Action<bool> QuestionAnswered;

        private void Awake()
        {
            _answerBars = Enumerable.Range(0, _answerCount).Select(index => _answerContainer.GetChild(index).GetComponent<AnswerBar>()).ToList();
        }

        private void OnEnable()
        {
            _answered = false;
        }

        public void SetQuestionData(QuestionData questionData, int index)
        {
            _answered = false;
            _questionText.text = questionData.QuestionText;
            Debug.Log("MarkDot " + index);
            _dotController.MarkDot(index);
            var answersCopy = new List<AnswerData>(questionData.Answers);
            if (_shuffle)
            {
                ShuffleList(answersCopy);
            }
            for (int i = 0; i < answersCopy.Count; i++)
            {
                _answerBars[i].SetAnswer(answersCopy[i]);
                _answerBars[i].AnswerSelected += RevealAnswer;
            }
        }

        public void RevealAnswer(AnswerBar bar)
        {
            if (_answered)
                return;
            _answered = true;
            QuestionAnswered?.Invoke(bar.AnswerData.IsCorrect);
            foreach (var answer in _answerBars)
            {
                if (answer == bar)
                {
                    answer.MarkIfWrong();
                }
                answer.MarkIfCorrect();
            }
        }

        private static void ShuffleList<T>(List<T> list)
        {
            int cnt = list.Count;
            while (cnt > 1)
            {
                int next = rng.Next(cnt);
                cnt--;
                (list[next], list[cnt]) = (list[cnt], list[next]);
            }
        }
    }
}
