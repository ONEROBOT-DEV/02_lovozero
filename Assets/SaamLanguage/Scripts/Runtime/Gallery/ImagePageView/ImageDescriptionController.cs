using DG.Tweening;
using Gallery.ImagePageView;
using TMPro;
using UnityEngine;

namespace Gallery.FolderView
{
    [RequireComponent(typeof(TMP_Text))]
    public class ImageDescriptionController : MonoBehaviour
    {
        private const float DURATION_ANIMATION = 0.35f / 2;
        [SerializeField] private ImageViewController _imageViewController;
        [SerializeField] private TMP_Text _text;
        private int _lastIndex;

        public void InitPanel()
        {
            _imageViewController.OnSwipeImage += OnSwipeImage;
            _imageViewController.OnChangeCurrentIndex += OnChangeCurrentIndex;

            _text.color = new Color(1, 1, 1, 0);
        }

        private void OnEnable()
        {
            _lastIndex = 0;
            OnChangeCurrentIndex(_lastIndex);
        }

        private void OnSwipeImage(int direction)
        {
            OnChangeCurrentIndex(_lastIndex + direction);
        }

        private void OnChangeCurrentIndex(int index)
        {
            _lastIndex = index;
            _text.DOFade(0, DURATION_ANIMATION).OnComplete(() =>
            {
                var imageDescription = _imageViewController.GetImageDescriptionFromIndex(index);
                _text.text = imageDescription;
                _text.DOFade(1, DURATION_ANIMATION);
            });
        }
    }
}
