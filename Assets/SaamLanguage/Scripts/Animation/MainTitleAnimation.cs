using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class MainTitleAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMesh;
    [SerializeField] private float _pauseBetweenCharacterAnimation;
    [SerializeField] private float _animationDuration;

    private string _wholeText;

    [ShowInInspector]
    private float[] _opacities;
    private Tween[] _tweens;

    private StringBuilder _builder = new StringBuilder();

    [ShowInInspector]
    public string BuilderText => _builder?.ToString();

    private void Awake()
    {
        _wholeText = _textMesh.text;
    }

    private void OnEnable()
    {
        _builder.Clear();
        _textMesh.text = "";
        _opacities = new float[_wholeText.Length];
        _tweens = new Tween[_wholeText.Length];
        for (int i = 0; i < _wholeText.Length; i++)
        {
            ChangeLetter(i, 0f, true);
        }
        StartCoroutine(Animate());
    }

    private void Update()
    {
        _textMesh.text = _builder.ToString();
    }

    private void OnDisable()
    {
        foreach (var tween in _tweens)
        {
            tween.Kill();
        }
    }

    private IEnumerator Animate()
    {
        for (int i = 0; i < _wholeText.Length; i++)
        {
            AnimateLetter(i);
            yield return new WaitForSeconds(_pauseBetweenCharacterAnimation);
        }
    }

    private void AnimateLetter(int index)
    {
        _tweens[index] = DOTween.To(() => _opacities[index], x => _opacities[index] = x, 1.0f, _animationDuration).OnUpdate(() => ChangeLetter(index, _opacities[index], false));
    }

    private void ChangeLetter(int index, float opacity, bool init)
    {
        if (init)
        {
            _builder.Append($"<color=#FFFFFF{ConvertOpacityToHex(opacity)}>{_wholeText[index]}</color>");
        } 
        else
        {
            int substringLength = 26;
            string newSubstring = $"<color=#FFFFFF{ConvertOpacityToHex(opacity)}>{_wholeText[index]}</color>";

            int startIndex = index * substringLength;
            int endIndex = startIndex + substringLength;

            if (startIndex >= 0 && endIndex <= _builder.Length)
            {
                _builder.Remove(startIndex, substringLength);
                _builder.Insert(startIndex, newSubstring);
            }
        }
    }

    public string ConvertOpacityToHex(float opacity)
    {
        // Clamp the opacity value between 0.0 and 1.0
        opacity = Mathf.Clamp01(opacity);

        // Scale the opacity from [0.0, 1.0] to [00, FF]
        byte alphaByte = (byte)(opacity * 255);

        // Convert the alpha value to a two-letter hexadecimal string
        string hexAlpha = alphaByte.ToString("X2");

        return hexAlpha;
    }
}
