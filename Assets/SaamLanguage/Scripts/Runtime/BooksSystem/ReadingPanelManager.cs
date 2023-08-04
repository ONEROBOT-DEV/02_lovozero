using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Gallery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.BooksSystem
{
    public class ReadingPanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject _verticalListsContainer;
        [SerializeField] private GameObject _horizontalListsContainer;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private float _swipeTime = 1f;

        [SerializeField] private int _countOfPreloadedLists = 7;

        [SerializeField] private SwipeHandler _swipeHandler;

        [SerializeField] private List<RawImage> _horizontalLists = new();
        [SerializeField] private List<RawImage> _verticalLists = new();
        [SerializeField] private List<Color> _colorsAtIndices = new();

        private RectTransform _listsContainer;
        private List<RawImage> _lists;

        private List<Texture2D> _textures;

        private ReadingPanelLoader _readingPanelLoader;

        private List<string> _listsPaths;

        private int _currentListIndex;

        private bool _isAnimating;

        private void Awake()
        {
            _readingPanelLoader = new ReadingPanelLoader();

            _swipeHandler.OnVerticalSwipe += isSwipeToUp =>
            {
                if (_isCreatingTexture)
                    return;

                if (isSwipeToUp)
                    MoveToPreviousList();
                else
                    MoveToNextList();
            };
        }

        public void MoveToNextList()
        {
            if (_isAnimating)
                return;

            if (_currentListIndex + 1 < _listsPaths.Count)
            {
                _currentListIndex++;
                StartCoroutine(AnimateNextSwap());


                if (_currentListIndex + 2 >= _textures.Count)
                {
                    _lists[^1].gameObject.SetActive(false);
                }
                else
                {
                    var loadedTexture = _textures[_currentListIndex + 2];
                    _lists[^1].texture = loadedTexture;
                }
            }
        }

        private IEnumerator AnimateNextSwap()
        {
            _isAnimating = true;

            for (int i = _lists.Count - 1; i > 0; i--)
            {
                _lists[i].rectTransform.DOSizeDelta(_lists[i - 1].rectTransform.sizeDelta, _swipeTime);
                _lists[i].rectTransform.DOLocalMove(_lists[i - 1].rectTransform.localPosition, _swipeTime);
                _lists[i].DOColor(_colorsAtIndices[i - 1], _swipeTime);
            }

            StartCoroutine(AnimateShadow(_lists[1].GetComponent<Shadow>(), 0, _swipeTime));
            StartCoroutine(AnimateShadow(_lists[2].GetComponent<Shadow>(), 0.61f, _swipeTime));

            _lists[0].rectTransform.sizeDelta = _lists[^1].rectTransform.sizeDelta;
            _lists[0].rectTransform.localPosition = _lists[^1].rectTransform.localPosition;
            _lists[0].DOColor(_colorsAtIndices.Last(), _swipeTime);

            for (int i = 0; i < _lists.Count - 1; i++)
                (_lists[i], _lists[i + 1]) = (_lists[i + 1], _lists[i]);

            _listsContainer.GetChild(_listsContainer.childCount - 1).SetSiblingIndex(0);

            yield return new WaitForSeconds(_swipeTime + 0.1f);
            _isAnimating = false;
        }

        private IEnumerator AnimateShadow(Shadow shadow, float targetAlpha, float time)
        {
            float tempTime = 0;
            var startColor = shadow.effectColor;
            var targetColor = startColor;
            targetColor.a = targetAlpha;
            while (tempTime < time)
            {
                yield return null;

                float reached = tempTime / time;
                var interColor = Color.Lerp(startColor, targetColor, reached);

                shadow.effectColor = interColor;
                tempTime += Time.deltaTime;
            }
        }

        public void MoveToPreviousList()
        {
            if (_isAnimating)
                return;

            if (_currentListIndex - 1 >= 0)
            {
                _currentListIndex--;
                StartCoroutine(AnimatePreviousSwap());
                var loadedTexture = _textures[_currentListIndex];
                _lists[1].texture = loadedTexture;
            }
        }

        private IEnumerator AnimatePreviousSwap()
        {
            _isAnimating = true;

            for (int i = 0; i < _lists.Count - 1; i++)
            {
                _lists[i].rectTransform.DOSizeDelta(_lists[i + 1].rectTransform.sizeDelta, _swipeTime);
                _lists[i].rectTransform.DOLocalMove(_lists[i + 1].rectTransform.localPosition, _swipeTime);
                _lists[i].DOColor(_colorsAtIndices[i + 1], _swipeTime);
            }

            StartCoroutine(AnimateShadow(_lists[1].GetComponent<Shadow>(), 0, _swipeTime));
            StartCoroutine(AnimateShadow(_lists[0].GetComponent<Shadow>(), 0.61f, _swipeTime));
            _lists.Last().DOColor(_colorsAtIndices[0], _swipeTime);

            var lastList = _lists[^1];
            var l0SizeDelta = _lists[0].rectTransform.sizeDelta;
            var l0LocalPosition = _lists[0].rectTransform.localPosition;

            for (int i = _lists.Count - 1; i > 0; i--)
                (_lists[i], _lists[i - 1]) = (_lists[i - 1], _lists[i]);


            _lists[1].gameObject.SetActive(true);

            yield return new WaitForSeconds(_swipeTime + 0.1f);
            _listsContainer.GetChild(0).SetSiblingIndex(_listsContainer.childCount - 1);

            lastList.rectTransform.sizeDelta = l0SizeDelta;
            lastList.rectTransform.localPosition = l0LocalPosition;

            _isAnimating = false;
        }

        private bool _isCreatingTexture;

        public void Load(string title)
        {
            _isAnimating = false;
            _currentListIndex = 0;
            string orientation = title.Substring(title.IndexOf('-') + 1);
            _verticalListsContainer.SetActive(false);
            _horizontalListsContainer.SetActive(false);
            if (orientation == "ВЕРТИКАЛЬНЫЙ")
            {
                _verticalListsContainer.SetActive(true);
                _lists = _verticalLists;
                _listsContainer = (RectTransform)_verticalListsContainer.transform;
            }
            else
            {
                _horizontalListsContainer.SetActive(true);
                _lists = _horizontalLists;
                _listsContainer = (RectTransform)_horizontalListsContainer.transform;
            }

            GC.Collect();
            StopCoroutine(LoadedImagesManager());

            if (_textures != null)
                for (int i = 0; i < _textures.Count; i++)
                    Destroy(_textures[i]);

            _textures = new List<Texture2D>();

            _titleText.SetText(title.Substring(0, title.LastIndexOf('-')).Normalize());

            _readingPanelLoader.Load(title, out _listsPaths);

            for (int i = 0; i < _lists.Count; i++)
            {
                _textures.Add(_readingPanelLoader.LoadImageReadAllBytes(_listsPaths[i]));
                if (i > 0)
                    _lists[i].texture = _textures[i - 1];
            }
            
            for(int i = _lists.Count; i < _listsPaths.Count; i++)
                _textures.Add(null);

            StartCoroutine(LoadedImagesManager());
        }

        private IEnumerator LoadedImagesManager()
        {
            int lastCurrentListIndex = -25;
            while (true)
            {
                if (lastCurrentListIndex != _currentListIndex)
                {
                    while (_isAnimating)
                        yield return null;

                    for (int i = Math.Max(_currentListIndex - _countOfPreloadedLists, 0);
                         i < Math.Min(_textures.Count, _currentListIndex + _countOfPreloadedLists);
                         i++)
                    {
                        if (_textures[i] == null)
                        {
                            int i1 = i;
                            StartCoroutine(GalleryLoadHelper.LoadImages(_listsPaths[i],
                                texture2D => { _textures[i1] = texture2D; }));
                        }
                    }

                    _lists[1].texture = _textures[_currentListIndex];
                    _lists[2].texture = _textures[_currentListIndex + 1];
                    _lists[3].texture = _textures[_currentListIndex + 2];

                    if (_currentListIndex > lastCurrentListIndex)
                    {
                        if (_currentListIndex - _countOfPreloadedLists >= 0)
                            Destroy(_textures[_currentListIndex - _countOfPreloadedLists]);
                    }
                    else
                    {
                        if (_currentListIndex + _countOfPreloadedLists < _textures.Count)
                            Destroy(_textures[_currentListIndex + _countOfPreloadedLists]);
                    }

                    GC.Collect(0);

                    lastCurrentListIndex = _currentListIndex;
                }

                yield return null;
            }
        }

        public void UnloadData()
        {
            GC.Collect();
            if (_textures != null)
                for (int i = 0; i < _textures.Count; i++)
                    Destroy(_textures[i]);
        }
    }
}
