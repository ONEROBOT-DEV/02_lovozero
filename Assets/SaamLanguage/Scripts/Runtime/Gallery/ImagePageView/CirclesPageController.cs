using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gallery.ImagePageView
{
    public class CirclesPageController : MonoBehaviour
    {
        private const float DURATION_ANIMATION = 0.35f;
        [SerializeField] private ImageViewController _imageViewController;
        [Space, SerializeField] private Image _circlePrefab;
        [Space, SerializeField] private Color _disableColor;
        [SerializeField] private Color _currentColor;
        private readonly List<Image> _circles = new();

        private int _lastIndex;

        public void InitPanel()
        {
            _imageViewController.OnSwipeImage += OnSwipeImage;
            _imageViewController.OnChangeCurrentIndex += OnChangeCurrentIndex;
        }

        private void OnSwipeImage(int direction)
        {
            OnChangeCurrentIndex(_lastIndex + direction);
        }

        public void Enable()
        {
            _lastIndex = 0;
            var imageCount = _imageViewController.GetImageCount();
            while (_circles.Count > imageCount)
            {
                var circle = _circles[^1];
                _circles.Remove(circle);
                Destroy(circle.gameObject);
            }

            foreach (var circle in _circles)
                circle.color = _disableColor;

            if (_circles.Count < imageCount)
            {
                for (var index = _circles.Count; index < imageCount; index++)
                {
                    _circles.Add(Instantiate(_circlePrefab, transform));
                }
            }

            _circles[_lastIndex].color = _currentColor;
        }

        private void OnChangeCurrentIndex(int index)
        {
            _circles[_lastIndex].DOColor(_disableColor, DURATION_ANIMATION);
            _lastIndex = CalculateIndex(index);
            _circles[_lastIndex].DOColor(_currentColor, DURATION_ANIMATION);
        }

        private int CalculateIndex(int index)
        {
            if (index >= _imageViewController.GetImageCount()) index = index % _imageViewController.GetImageCount();
            else if (index < 0)
                index = (_imageViewController.GetImageCount() + index % _imageViewController.GetImageCount()) %
                        _imageViewController.GetImageCount();

            return index;
        }
    }
}
