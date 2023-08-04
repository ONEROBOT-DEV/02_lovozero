using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotController : MonoBehaviour
{
    [SerializeField] private Image _dot;
    [SerializeField] private Color _chosenColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private int _count;

    private List<Image> _dots;

    private void Awake()
    {
        SetDots();
    }

    private void OnDisable()
    {
        MarkDot(0);
    }

    private void SetDots()
    {
        if (_dots != null)
        {
            return;
        }
        _dots = new List<Image>(_count);
        if (transform.childCount != _count)
        {
            _dots.Add(_dot);
            for (int i = 0; i < _count - 1; i++)
            {
                _dots.Add(Instantiate(_dot, _dot.transform.parent));
            }
        }
        else
        {
            for (int i = 0; i < _count; i++)
            {
                _dots.Add(transform.GetChild(i).GetComponent<Image>());
            }
        }
        
    }

    public void MarkDot(int index)
    {
        SetDots();
        for (int i = 0; i < _dots.Count; i++)
        {
            _dots[i].color = _defaultColor;
        }
        _dots[index].color = _chosenColor;
    }
}
