using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Quiz
{
    public class AnswerController : MonoBehaviour
    {
        private static readonly Random rng = new Random();

        [SerializeField] private Transform _answerContainer;
        [SerializeField] private int _answerCount;

        private List<AnswerBar> _answerBars;

        private void Awake()
        {
            _answerBars = Enumerable.Range(0, _answerCount).Select(index => _answerContainer.GetChild(index).GetComponent<AnswerBar>()).ToList();
        }

        public void RevealAnswer(int marked)
        {
            _answerBars[marked].MarkIfWrong();
            foreach (var answer in _answerBars)
            {
                answer.MarkIfCorrect();
            }
            
        }

        public void SetAnswers(IList<Answer> answers, bool shuffled)
        {
            var answersCopy = new List<Answer>(answers);
            if (shuffled)
            {
                ShuffleList(answersCopy);
            }
            for (int i = 0; i < answersCopy.Count; i++)
            {
                _answerBars[i].SetAnswer(answersCopy[i]);
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

        private void OnValidate()
        {
            int answerCount = _answerContainer.GetComponentsInChildren<AnswerBar>().Length;
            if (answerCount != _answerCount)
            {
                Debug.LogError("answerContainer must contain only 3 children");
            }
        }
    }
}
