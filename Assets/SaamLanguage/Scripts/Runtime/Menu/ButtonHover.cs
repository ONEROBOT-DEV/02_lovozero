using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    [SerializeField] private float _fadeDuration;
    [SerializeField] private List<GraphicFade> _config;

    public void OnPressed()
    {
        foreach (var config in _config)
        {
            config.Press(_fadeDuration);
        }
    }

    public void OnFreed()
    {
        foreach (var config in _config)
        {
            config.Free(_fadeDuration);
        }
    }

    [Serializable]
    private class GraphicFade
    {
        [SerializeField] private Graphic Graphic;
        [SerializeField] private Color DefaultColor;
        [SerializeField] private Color PressedColor;

        public void Press(float duration)
        {
            Graphic.DOColor(PressedColor, duration);
        }

        public void Free(float duration)
        {
            Graphic.DOColor(DefaultColor, duration);
        }
    }
}
