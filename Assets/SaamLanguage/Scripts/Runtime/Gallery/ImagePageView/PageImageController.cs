using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Gallery.ImagePageView
{
    public class PageImageController : MonoBehaviour
    {
        private const float DURATION_ANIMATION = 0.35f;
        private readonly Color CLEAR_WHITE_COLOR = new(1, 1, 1, 0);

        [SerializeField] private Vector2 _maxImageSize;
        [SerializeField] private ImageViewController _imageViewController;
        [Space, SerializeField] private List<RawImage> _images;
        [SerializeField] private RawImage _swapImage;
        [Space, SerializeField] private float _minImageSize;
        [SerializeField] private float _horizontalOffset;
        [Space, SerializeField] private Color _middlePageColor;
        [SerializeField] private Color _fadePageColor;
        private Vector2 _defaultSize;
        private int _lastIndex;

        public void InitPanel()
        {
            _defaultSize = _swapImage.rectTransform.sizeDelta;
            _imageViewController.OnSwipeImage += OnSwipeImage;
            _imageViewController.OnChangeCurrentIndex += OnChangeCurrentIndex;
            _images.Remove(_swapImage);

            _images[0].color = _fadePageColor;
            _images[^1].color = _fadePageColor;
        }

        private void OnSwipeImage(int direction)
        {
            if (Mathf.Abs(direction) <= 0) return;

            _lastIndex += direction;
            if (direction > 0) MovePageLeftDirection();
            else MovePageRightDirection();
        }

        public void Enable()
        {
            _lastIndex = 0;

            var indexData = -1;
            for (var indexPage = 0; indexPage < _images.Count; indexPage++, indexData++)
            {
                var page = indexPage;
                _images[page].color = CLEAR_WHITE_COLOR;
                _imageViewController.GetImageFromIndex?.Invoke(indexData,
                    (sprite) => SetSizeAndTexture(_images[page], sprite,
                        page % 2 == 0 ? _fadePageColor : _middlePageColor));
            }
        }

        private void OnDisable()
        {
            foreach (var image in _images)
            {
                Destroy(image.texture);
            }

            Destroy(_swapImage.texture);
        }

        private void OnChangeCurrentIndex(int index)
        {
            var direction = _lastIndex - index;

            var seq = DOTween.Sequence();
            for (var i = 0; i < Mathf.Abs(direction); i++)
            {
                seq.AppendCallback(() =>
                {
                    _lastIndex += Mathf.Clamp(direction, -1, 1);
                    OnSwipeImage(direction);
                });
                seq.AppendInterval(DURATION_ANIMATION);
            }
        }

        private void MovePageLeftDirection()
        {
            _swapImage.transform.localPosition =
                _images[^1].transform.localPosition + Vector3.right * _horizontalOffset;
            _swapImage.rectTransform.sizeDelta = _defaultSize;
            Destroy(_swapImage.texture);
            _swapImage.texture = null;
            _swapImage.color = CLEAR_WHITE_COLOR;
            _images.Add(_swapImage);

            var saveSwapImage = _swapImage;
            var saveChangeIndex = _lastIndex + 1;
            DOVirtual.DelayedCall(DURATION_ANIMATION, () =>
                _imageViewController.GetImageFromIndex(saveChangeIndex,
                    (texture2D) => SetSizeAndTexture(saveSwapImage, texture2D, _fadePageColor)));

            _swapImage = _images[0];

            foreach (var page in _images)
            {
                page.transform
                    .DOLocalMoveX(page.transform.localPosition.x - _horizontalOffset, DURATION_ANIMATION);
            }

            _images.Remove(_swapImage);

            _images[0].DOColor(_fadePageColor, DURATION_ANIMATION);
            _images[0].transform.DOScale(_minImageSize, DURATION_ANIMATION);

            _images[1].DOColor(_middlePageColor, DURATION_ANIMATION);
            _images[1].transform.DOScale(1, DURATION_ANIMATION);
            _images[1].transform.SetSiblingIndex(_images[0].transform.parent.childCount);
        }

        private void MovePageRightDirection()
        {
            _swapImage.transform.localPosition = _images[0].transform.localPosition - Vector3.right * _horizontalOffset;
            _swapImage.rectTransform.sizeDelta = _defaultSize;
            Destroy(_swapImage.texture);
            _swapImage.texture = null;
            _swapImage.color = CLEAR_WHITE_COLOR;
            _images.Insert(0, _swapImage);

            var saveSwapImage = _swapImage;
            var saveChangeIndex = _lastIndex - 1;
            DOVirtual.DelayedCall(DURATION_ANIMATION, () =>
                _imageViewController.GetImageFromIndex(saveChangeIndex,
                    (texture2D) => SetSizeAndTexture(saveSwapImage, texture2D, _fadePageColor)));

            _swapImage = _images[^1];

            foreach (var page in _images)
            {
                page.transform
                    .DOLocalMoveX(page.transform.localPosition.x + _horizontalOffset, DURATION_ANIMATION);
            }

            _images.Remove(_swapImage);

            _images[2].DOColor(_fadePageColor, DURATION_ANIMATION);
            _images[2].transform.DOScale(_minImageSize, DURATION_ANIMATION);

            _images[1].DOColor(_middlePageColor, DURATION_ANIMATION);
            _images[1].transform.DOScale(1, DURATION_ANIMATION);
            _images[1].transform.SetSiblingIndex(_images[0].transform.parent.childCount);
        }

        private void SetSizeAndTexture(RawImage image, Texture texture, Color endColor)
        {
            try
            {
                image.DOColor(endColor, DURATION_ANIMATION);
                image.rectTransform.sizeDelta = CalculateNewSize(new Vector2(texture.width, texture.height));
                image.texture = texture;
            }
            catch
            {
                
            }
        }

        private Vector2 CalculateNewSize(Vector2 size)
        {
            Vector2 newTextureSize;
            var textureCoefficient = (float)size.y / size.x;
            if (textureCoefficient > _maxImageSize.y / _maxImageSize.x)
            {
                newTextureSize = new Vector2((float)size.x / size.y * _maxImageSize.y, _maxImageSize.y);
            }
            else
            {
                newTextureSize = new Vector2(_maxImageSize.x, (float)size.y / size.x * _maxImageSize.x);
            }

            return newTextureSize;
        }

        [Button]
        private void FindImage()
        {
            _images = new List<RawImage>(GetComponentsInChildren<RawImage>());
        }
    }
}
