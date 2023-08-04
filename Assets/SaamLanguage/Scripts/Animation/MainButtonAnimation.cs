using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MainButtonAnimation : MonoBehaviour
{
    [SerializeField] private float _maxScale;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Ease _expandEase;
    [SerializeField] private Ease _shrinkEase;
    [SerializeField] private float _shrinkDelay;

    private Vector3 _defaultScale;
    private Sequence _sequence;

    private void Awake()
    {
        _defaultScale = transform.localScale;
        _sequence = DOTween.Sequence();
    }

    private void OnDisable()
    {
        transform.DOComplete();
    }

    public void Animate()
    {
        if (_sequence.IsPlaying())
            return;
        _sequence.Append(transform.DOScale(_maxScale,_animationDuration).SetEase(_expandEase));
        _sequence.Append(transform.DOScale(_defaultScale,_animationDuration).SetEase(_shrinkEase).SetDelay(_shrinkDelay));
        _sequence.Play();
    }
}
