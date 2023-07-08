using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class AnswerBar : MonoBehaviour
    {
        [SerializeField] private Color _defaultAnswerColor;
        [SerializeField] private Color _wrongAnswerColor;
        [SerializeField] private Color _correctAnswerColor;

        private Image _background;
        private TextMeshProUGUI _answerText;
        private Answer _answer;
        private Button _button;

        private void Awake()
        {
            _background = GetComponentInChildren<Image>();
            _answerText = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener();
        }

        public void MarkIfCorrect()
        {
            if (_answer.IsCorrect)
            {
                _background.color = _defaultAnswerColor;
            }
        }

        public void MarkIfWrong()
        {
            if (!_answer.IsCorrect)
            {
                _background.color = _wrongAnswerColor;
            }
        }

        public void MarkDefault()
        {
            _background.color = _correctAnswerColor;
        }

        public void SetAnswer(Answer answer)
        {
            _answer = answer;
            _answerText.text = _answer.AnswerText;
            MarkDefault();
        }
    }
}
