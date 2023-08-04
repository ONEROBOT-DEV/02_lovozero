using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneCard : MonoBehaviour, IPointerClickHandler
{
    public event Action<SceneCard> RequestOpenCard; 
    public event Action<SceneCard> OnCardOpened; 

    [SerializeField] private Image _backImage;
    [SerializeField] private Image _frontImage;
    public bool IsGuessed { get; set; } = false;
    public bool IsAnimating;
    public CardSO CardSo { get; set; }

    public void Init(CardSO so)
    {
        this.CardSo = so;
        _backImage.sprite = so.Sprite;

        RequestOpenCard = null;
        OnCardOpened = null;
        
        ResetView();
    }

    public void Open()
    {
        StartCoroutine(OpenAnimation());
    }

    public IEnumerator OpenAnimation()
    {
        IsAnimating = true;
        
        var currentRotation = transform.localEulerAngles;
        currentRotation.y -= 90;
        float openDuration = 0.5f;
        transform.DOLocalRotate(currentRotation, openDuration / 2);
        yield return new WaitForSeconds(openDuration / 2);

        _backImage.enabled = true;
        _frontImage.enabled = false;
        
        currentRotation.y -= 90;
        transform.DOLocalRotate(currentRotation, openDuration / 2);
        yield return new WaitForSeconds(openDuration / 2);

        IsAnimating = false;
        OnCardOpened?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!IsGuessed && !IsAnimating)
        {
            RequestOpenCard?.Invoke(this);
        }
    }

    public void FadeAndDisable()
    {
        IsGuessed = true;
        _backImage.color = Color.gray;
        GetComponent<Image>().color = Color.gray;
        _backImage.raycastTarget = false;
    }

    public void Close()
    {
        StartCoroutine(CloseAnimation());
    }

    private IEnumerator CloseAnimation()
    {
        IsAnimating = true;
        
        var currentRotation = transform.localEulerAngles;
        currentRotation.y += 90;
        float openDuration = 0.5f;
        transform.DOLocalRotate(currentRotation, openDuration / 2);
        yield return new WaitForSeconds(openDuration / 2);

        _backImage.enabled = false;
        _frontImage.enabled = true;
        
        currentRotation.y += 90;
        transform.DOLocalRotate(currentRotation, openDuration / 2);
        yield return new WaitForSeconds(openDuration / 2);

        IsAnimating = false;
    }

    public void ResetView()
    {
        transform.localEulerAngles = Vector3.zero;
        _backImage.enabled = false;
        _frontImage.enabled = true;

        _backImage.color = Color.white;
        GetComponent<Image>().color = Color.white;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
