using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BackgroundLetterAnimation : MonoBehaviour
{
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _oneWayDuration;
    [SerializeField] private Ease _ease;

    private float _initialScale;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        var scale = transform.localScale;
        scale.x = _maxScale;
        transform.localScale = scale;
        Animate();
    }

    private void Animate()
    {
        var imageTransform = _image.transform;
        Sequence animationSequence = DOTween.Sequence(transform).SetLoops(-1);
        animationSequence.Append(imageTransform.DOScale(_minScale, _oneWayDuration));
        animationSequence.Append(imageTransform.DOScale(_maxScale, _oneWayDuration));
        animationSequence.Play();
    }
}
