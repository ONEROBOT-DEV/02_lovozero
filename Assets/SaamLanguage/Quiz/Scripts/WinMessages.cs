using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Win Messages", menuName = "ScriptableObjects/Win Message", order = 2)]
public class WinMessages : ScriptableObject
{
    [SerializeField] private WinMessage[] _winMessages;

    public IEnumerable<WinMessage> Messages => _winMessages;

    public WinMessage GetMessageForPoints(int points)
    {
        foreach (var message in _winMessages)
        {
            if (message.IsInRange(points))
            {
                return message;
            }
        }
        return null;
    }
}

[Serializable]
public class WinMessage
{
    [SerializeField] private int _minPointsInclude;
    [SerializeField] private int _maxPointsInclude;
    [SerializeField] private string _text;

    public string Text => _text;

    public bool IsInRange(int points)
    {
        return points <= _maxPointsInclude && points >= _minPointsInclude;
    }
}