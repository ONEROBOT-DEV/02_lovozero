using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CardSO> _dataCards = new();
    [SerializeField] private List<SceneCard> _sceneCards = new();
    [SerializeField] private SceneCard _bigCard;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _timerCircle;
    [SerializeField] private TextMeshProUGUI _rusTextTranslation;
    [SerializeField] private TextMeshProUGUI _saamTextTranslation;

    [SerializeField] private TextMeshProUGUI _guessedCardsCount;
    
    private int guessedCards = 0;

    public float Timer
    {
        get => _timer;
        private set
        {
            _timer = value;
            _timerText.SetText(((int)_timer).ToString());
            _timerCircle.fillAmount = _timer / _fullTimerValue;
        }
    }

    private float _fullTimerValue = 60;
    private bool someCardIsAnimating;
    [SerializeField] private int needToGuessToWin = 8;
    private float _timer;

    private void OnEnable()
    {
        StartCoroutine(InitRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator InitRoutine()
    {
        _bigCard.ResetView();
        guessedCards = 0;
        _guessedCardsCount.SetText(guessedCards.ToString() + '/' + needToGuessToWin);
        
        Timer = 60;

        SetupCards();

        foreach (var sceneCard in _sceneCards)
            sceneCard.Open();
        yield return new WaitForSeconds(3f);
        
        foreach (var sceneCard in _sceneCards)
            sceneCard.Close();

        foreach (var sceneCard in _sceneCards)
        {
            sceneCard.RequestOpenCard += SceneCardOnRequestOpenCard;
            sceneCard.OnCardOpened += SceneCardOnOnCardOpened;
        }

        yield return new WaitForSeconds(1f);

        GenerateBigCard();

        StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        while (true)
        {
            Timer -= Time.deltaTime;
            yield return null;
        }
    }

    private void SetupCards()
    {
        var dataCardsCopy = new List<CardSO>();

        foreach (var ds in _dataCards)
            dataCardsCopy.Add(ds);

        foreach (var sceneCard in _sceneCards)
        {
            var so = dataCardsCopy[Random.Range(0, dataCardsCopy.Count)];
            dataCardsCopy.Remove(so);
            sceneCard.Init(so);
        }
    }

    private void SceneCardOnRequestOpenCard(SceneCard sceneCard)        
    {
        if (!someCardIsAnimating)
        {
            sceneCard.Open();
            someCardIsAnimating = true;
        }
    }

    private void SceneCardOnOnCardOpened(SceneCard sceneCard)
    {
        someCardIsAnimating = false;

        if (sceneCard.CardSo.Sprite == _bigCard.CardSo.Sprite)
        {
            guessedCards++;
            _guessedCardsCount.SetText(guessedCards.ToString() + '/' + needToGuessToWin);
            sceneCard.FadeAndDisable();
            GenerateBigCard();
        }
        else
        {
            sceneCard.Close();
        }
    }

    private void GenerateBigCard()
    {
        var sceneCard = CalculateUnguessedSceneCard();
        var so = sceneCard.CardSo;
        _bigCard.Init(so);
        _bigCard.Open();

        _rusTextTranslation.SetText(so.RusTranslate);
        _saamTextTranslation.SetText(so.SaamTranlsate);
    }

    private SceneCard CalculateUnguessedSceneCard()
    {
        var unguessed = new List<SceneCard>();
        foreach (var sceneCard in _sceneCards)
        {
            if (!sceneCard.IsGuessed)
                unguessed.Add(sceneCard);
        }

        return unguessed[Random.Range(0, unguessed.Count)];
    }
}
