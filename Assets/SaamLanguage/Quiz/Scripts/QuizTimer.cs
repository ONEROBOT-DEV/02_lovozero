using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class QuizTimer : MonoBehaviour
{
    [SerializeField] private float startTime;
    [Required]
    [SerializeField] private TextMeshProUGUI _timerTextMeshPro;

    private float _remainTime = 0f;
    private bool _isPlaying = false;

    public event Action TimeEnded;

    public float RemainTime => _remainTime;

    private void OnEnable()
    {
        ResetTimer();
        StartCoroutine(TimerCoroutine());
        DisplayTime();
    }

    private void OnDisable()
    {
        StopTimer();
    }

    private void Update()
    {
        if (_isPlaying)
        {
            DisplayTime();
        }
    }

    public void StopTimer()
    {
        _isPlaying = false;
    }

    public void ResetTimer()
    {
        _remainTime = startTime;
    }

    public void StartTimer()
    {
        _isPlaying = true;
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return null;
            if (!_isPlaying)
                continue;
            _remainTime -= Time.deltaTime;
            if (_remainTime <= 0f )
            {
                _remainTime = 0f;
                TimeEnded?.Invoke();
                StopTimer();
            }
        }
    }

    private void DisplayTime()
    {
        _timerTextMeshPro.text = ConvertToMMSS(_remainTime);
    }

    private static string ConvertToMMSS(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60);
        int remainingSeconds = Mathf.FloorToInt(seconds % 60);

        return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
    }
}
