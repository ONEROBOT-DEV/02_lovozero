using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class AnswerBar : MonoBehaviour
    {
        [SceneObjectsOnly]
        [SerializeField] private Image _backgroundImage;
        [SceneObjectsOnly]
        [SerializeField] private TextMeshProUGUI _answerText;
        [SceneObjectsOnly]
        [SerializeField] private Button _answerButton;

        [SerializeField] private Sprite _defaultAnswerSprite;
        [SerializeField] private Sprite _wrongAnswerSprite;
        [SerializeField] private Sprite _correctAnswerSprite;

        private AnswerData _answerData;

        public AnswerData AnswerData => _answerData;

        public event Action<AnswerBar> AnswerSelected;

        private void Awake()
        {
            _answerButton.onClick.RemoveAllListeners();
            _answerButton.onClick.AddListener(OnClick);
        }

        private void OnEnable()
        {
            MarkDefault();
        }

        public void MarkIfCorrect()
        {
            if (_answerData.IsCorrect)
            {
                _backgroundImage.sprite = _correctAnswerSprite;
            }
        }

        public void MarkIfWrong()
        {
            if (!_answerData.IsCorrect)
            {
                _backgroundImage.sprite = _wrongAnswerSprite;
            }
        }

        public void MarkDefault()
        {
            _backgroundImage.sprite = _defaultAnswerSprite;
        }

        public void SetAnswer(AnswerData answer)
        {
            _answerData = answer;
            _answerText.text = _answerData.AnswerText;
            MarkDefault();
        }

        public void OnClick()
        {
            AnswerSelected?.Invoke(this);
        }
    }
}
